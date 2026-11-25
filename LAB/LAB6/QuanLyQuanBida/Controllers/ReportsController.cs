using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using QuanLyQuanBida.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: QUẢN TRỊ & BÁO CÁO - DOANH THU & PHÂN TÍCH TÀI CHÍNH (100% SQL SERVER)
    // - Tính toán trực tiếp từ Invoices, InvoiceDetails, InvoicePayments, PaymentVouchers
    // - Đồng bộ từng đồng với trang Hóa đơn & Thu ngân (/Invoices)
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class ReportsController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public ReportsController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string range = "all", DateTime? startDate = null, DateTime? endDate = null)
        {
            ViewData["Title"] = "Doanh thu & Báo cáo - BOLT BILLIARDS";

            var today = DateTime.Today;
            DateTime? filterStart = startDate;
            DateTime? filterEnd = endDate?.Date.AddDays(1).AddSeconds(-1);

            if (range == "today")
            {
                filterStart = today;
                filterEnd = today.AddDays(1).AddSeconds(-1);
            }
            else if (range == "yesterday")
            {
                filterStart = today.AddDays(-1);
                filterEnd = today.AddSeconds(-1);
            }
            else if (range == "7days")
            {
                filterStart = today.AddDays(-6);
                filterEnd = today.AddDays(1).AddSeconds(-1);
            }

            var allInvoices = await _context.Invoices.AsNoTracking().OrderByDescending(i => i.StartTime).ToListAsync();
            var allDetails = await _context.InvoiceDetails.AsNoTracking().ToListAsync();
            var allPayments = await _context.InvoicePayments.AsNoTracking().ToListAsync();
            var payMethods = await _context.PaymentMethods.AsNoTracking().ToDictionaryAsync(p => p.Id, p => p.Name);
            var tables = await _context.BilliardTables.AsNoTracking().ToListAsync();
            var tableDict = tables.ToDictionary(t => t.Id, t => t);
            var customers = await _context.Customers.AsNoTracking().ToDictionaryAsync(c => c.Id, c => c.FullName);
            var users = await _context.Users.AsNoTracking().ToDictionaryAsync(u => u.Id, u => u.FullName);
            var items = await _context.ServiceItems.AsNoTracking().ToDictionaryAsync(s => s.Id, s => s);

            var filteredInvoices = allInvoices.AsEnumerable();
            if (filterStart.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.StartTime >= filterStart.Value);
            if (filterEnd.HasValue)
                filteredInvoices = filteredInvoices.Where(i => i.StartTime <= filterEnd.Value);

            var paidInvoices = filteredInvoices.Where(i => i.Status == "Đã thanh toán").ToList();
            var paidIds = paidInvoices.Select(i => i.Id).ToHashSet();
            var payByInvoice = allPayments.GroupBy(p => p.InvoiceId).ToDictionary(g => g.Key, g => g.First());

            var invoiceRows = paidInvoices.Select(inv =>
            {
                var tbl = tableDict.ContainsKey(inv.BilliardTableId) ? tableDict[inv.BilliardTableId] : null;
                string pmName = "Khác";
                string pmKey = "other";
                if (payByInvoice.TryGetValue(inv.Id, out var p) && payMethods.TryGetValue(p.PaymentMethodId, out var name))
                {
                    pmName = name;
                    pmKey = name.Contains("Tiền mặt", StringComparison.OrdinalIgnoreCase) ? "cash"
                          : name.Contains("QR", StringComparison.OrdinalIgnoreCase) ? "qr" : "card";
                }
                string dur = inv.EndTime.HasValue
                    ? $"{(int)(inv.EndTime.Value - inv.StartTime).TotalHours}h {(inv.EndTime.Value - inv.StartTime).Minutes:00}p"
                    : "—";

                return new InvoiceRowViewModel
                {
                    Id = inv.Id,
                    InvoiceCode = inv.InvoiceCode,
                    TableName = tbl?.Name ?? $"Bàn #{inv.BilliardTableId}",
                    IsVipTable = tbl?.Name.Contains("VIP", StringComparison.OrdinalIgnoreCase) ?? false,
                    CustomerName = customers.ContainsKey(inv.CustomerId) ? customers[inv.CustomerId] : "Khách vãng lai",
                    StaffName = users.ContainsKey(inv.UserId) ? users[inv.UserId] : $"NV #{inv.UserId}",
                    StartTime = inv.StartTime,
                    EndTime = inv.EndTime,
                    DurationText = dur,
                    TableAmount = inv.TableAmount,
                    ServiceAmount = inv.ServiceAmount,
                    DiscountAmount = inv.DiscountAmount,
                    TotalPayment = inv.TotalPayment,
                    PaymentMethodName = pmName,
                    PaymentMethodKey = pmKey,
                    Status = inv.Status
                };
            }).ToList();

var categories = await _context.Categories.AsNoTracking()
    .ToDictionaryAsync(c => c.Id, c => c.Code);

var drinkItemIds = items.Values
    .Where(i => categories.TryGetValue(i.CategoryId, out var code)
                && (code == "DRINK" || code == "BEER"))
    .Select(i => i.Id)
    .ToHashSet();

// Top 5 thức uống bán chạy nhất (theo số lượng bán)
var topItems = allDetails
    .Where(d => paidIds.Contains(d.InvoiceId) && drinkItemIds.Contains(d.ServiceItemId))
    .GroupBy(d => d.ServiceItemId)
    .Select(g => new TopItemSalesRow
    {
        Code = items.ContainsKey(g.Key) ? items[g.Key].Code : $"SP#{g.Key}",
        Name = items.ContainsKey(g.Key) ? items[g.Key].Name : $"Món #{g.Key}",
        Unit = items.ContainsKey(g.Key) ? items[g.Key].Unit : "Phần",
        TotalQty = g.Sum(x => x.Quantity),
        TotalRevenue = g.Sum(x => x.TotalAmount)
    })
    .OrderByDescending(x => x.TotalQty)
    .Take(5)
    .ToList();

            // Phiếu chi thực tế từ PaymentVouchers
            var expTypes = await _context.ExpenseTypes.AsNoTracking().OrderBy(e => e.Id).ToListAsync();
            var expTypeDict = expTypes.ToDictionary(e => e.Id, e => e.Name);
            var vouchers = await _context.PaymentVouchers.AsNoTracking().OrderByDescending(v => v.CreatedAt).ToListAsync();
            var filteredVouchers = vouchers.AsEnumerable();
            if (filterStart.HasValue)
                filteredVouchers = filteredVouchers.Where(v => v.CreatedAt >= filterStart.Value);
            if (filterEnd.HasValue)
                filteredVouchers = filteredVouchers.Where(v => v.CreatedAt <= filterEnd.Value);

            var voucherRows = filteredVouchers.Select(v => new ExpenseVoucherRow
            {
                Id = v.Id,
                Code = v.Code,
                ExpenseTypeName = expTypeDict.ContainsKey(v.ExpenseTypeId) ? expTypeDict[v.ExpenseTypeId] : "Chi phí khác",
                StaffName = users.ContainsKey(v.UserId) ? users[v.UserId] : "Quản trị viên",
                Amount = v.Amount,
                CreatedAt = v.CreatedAt,
                Purpose = v.ExpensePurpose,
                Notes = v.Notes
            }).ToList();

            // Tính doanh thu theo 8 khung giờ (08h-24h) cho Hôm nay & Hôm qua từ SQL Server
            var hourlyToday = new decimal[8];
            var hourlyYesterday = new decimal[8];
            foreach (var inv in allInvoices.Where(i => i.Status == "Đã thanh toán"))
            {
                int bucket = Math.Clamp((inv.StartTime.Hour - 8) / 2, 0, 7);
                if (inv.StartTime.Date == today)
                    hourlyToday[bucket] += inv.TotalPayment;
                else if (inv.StartTime.Date == today.AddDays(-1))
                    hourlyYesterday[bucket] += inv.TotalPayment;
            }

            var vm = new ReportsDashboardViewModel
            {
                SelectedRange = range,
                StartDate = filterStart,
                EndDate = filterEnd,
                TotalRevenue = invoiceRows.Sum(r => r.TotalPayment),
                TableRevenue = invoiceRows.Sum(r => r.TableAmount),
                ServiceRevenue = invoiceRows.Sum(r => r.ServiceAmount),
                TotalDiscount = invoiceRows.Sum(r => r.DiscountAmount),
                TotalExpenses = voucherRows.Sum(v => v.Amount),
                CompletedInvoiceCount = invoiceRows.Count,
                PlayingTableCount = tables.Count(t => t.Status == "Đang chơi"),
                TotalTableCount = tables.Count,
                CashRevenue = invoiceRows.Where(r => r.PaymentMethodKey == "cash").Sum(r => r.TotalPayment),
                TransferRevenue = invoiceRows.Where(r => r.PaymentMethodKey == "qr").Sum(r => r.TotalPayment),
                CardRevenue = invoiceRows.Where(r => r.PaymentMethodKey == "card").Sum(r => r.TotalPayment),
                Invoices = invoiceRows,
                TopItems = topItems,
                Vouchers = voucherRows,
                ExpenseTypes = expTypes,
                HourlyToday = hourlyToday,
                HourlyYesterday = hourlyYesterday
            };
 // Doanh thu từng ngày trong khoảng thời gian đã lọc (startDate - endDate)
var revenueByDay = paidInvoices
    .GroupBy(i => i.StartTime.Date)
    .OrderBy(g => g.Key)
    .Select(g => new
    {
        Label = g.Key.ToString("dd/MM/yyyy"),
        Total = g.Sum(x => x.TotalPayment)
    })
    .ToList();
ViewBag.RevenueLabels = revenueByDay.Select(x => x.Label).ToList();
ViewBag.RevenueValues = revenueByDay.Select(x => x.Total).ToList();
ViewBag.TopDrinkLabels = topItems.Select(x => x.Name).ToList();
ViewBag.TopDrinkValues = topItems.Select(x => x.TotalQty).ToList();

            return View(vm);
        }

        // POST: /Reports/CreateVoucher — Lập phiếu chi thật lưu vào SQL Server
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVoucher(int expenseTypeId, decimal amount, string purpose, string? notes)
        {
            if (amount <= 0 || string.IsNullOrWhiteSpace(purpose))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập số tiền hợp lệ và nội dung chi.";
                return RedirectToAction(nameof(Index));
            }

            int userId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 1;
            string code = $"PC-{DateTime.Now:yyMM}-{DateTime.Now:HHmmss}";

            _context.PaymentVouchers.Add(new PaymentVoucher
            {
                ExpenseTypeId = expenseTypeId,
                UserId = userId,
                Code = code,
                Amount = amount,
                CreatedAt = DateTime.Now,
                ExpensePurpose = purpose.Trim(),
                Notes = notes?.Trim()
            });
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã lập phiếu chi {code} ({amount:N0} đ) và cập nhật vào báo cáo tài chính.";
            return RedirectToAction(nameof(Index));
        }
    }
}

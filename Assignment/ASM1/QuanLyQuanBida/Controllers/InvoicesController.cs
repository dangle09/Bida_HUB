using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    // ===================================================================================
    // CONTROLLER: VẬN HÀNH & BÁN HÀNG - HÓA ĐƠN & THU NGÂN POS (ĐỒNG BỘ 100% SQL SERVER)
    // - Dùng chung dữ liệu SQL Server với Báo cáo & Doanh thu (/Reports)
    // ===================================================================================
    [Authorize(Policy = "CashierOrAbove")]
    public class InvoicesController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public InvoicesController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString, string? methodKey, string? statusFilter)
        {
            ViewData["Title"] = "Hóa đơn & Thu ngân - BOLT BILLIARDS";

            var invoices = await _context.Invoices.AsNoTracking().OrderByDescending(i => i.StartTime).ToListAsync();
            var tables = await _context.BilliardTables.AsNoTracking().ToDictionaryAsync(t => t.Id, t => t);
            var customers = await _context.Customers.AsNoTracking().ToDictionaryAsync(c => c.Id, c => c.FullName);
            var users = await _context.Users.AsNoTracking().ToDictionaryAsync(u => u.Id, u => u.FullName);
            var payMethods = await _context.PaymentMethods.AsNoTracking().ToDictionaryAsync(p => p.Id, p => p.Name);
            var payments = await _context.InvoicePayments.AsNoTracking().ToListAsync();
            var payByInvoice = payments.GroupBy(p => p.InvoiceId).ToDictionary(g => g.Key, g => g.First());

            var itemsDict = await _context.ServiceItems.AsNoTracking().ToDictionaryAsync(s => s.Id, s => s);
            var details = await _context.InvoiceDetails.AsNoTracking().ToListAsync();
            var detailsByInvoice = details.GroupBy(d => d.InvoiceId).ToDictionary(g => g.Key, g => g.ToList());

            var rows = invoices.Select(inv =>
            {
                var tbl = tables.ContainsKey(inv.BilliardTableId) ? tables[inv.BilliardTableId] : null;
                string tblName = tbl?.Name ?? $"Bàn #{inv.BilliardTableId}";
                bool isVip = tblName.Contains("VIP", StringComparison.OrdinalIgnoreCase);

                string custName = customers.ContainsKey(inv.CustomerId)
                    ? customers[inv.CustomerId]
                    : "Khách vãng lai";

                string staffName = users.ContainsKey(inv.UserId) ? users[inv.UserId] : $"NV #{inv.UserId}";

                string pmName = "Chưa thanh toán";
                string pmKey = "unpaid";
                if (payByInvoice.TryGetValue(inv.Id, out var pay) && payMethods.TryGetValue(pay.PaymentMethodId, out var mName))
                {
                    pmName = mName;
                    if (mName.Contains("Tiền mặt", StringComparison.OrdinalIgnoreCase) || mName.Contains("Cash", StringComparison.OrdinalIgnoreCase))
                        pmKey = "cash";
                    else if (mName.Contains("QR", StringComparison.OrdinalIgnoreCase) || mName.Contains("Chuyển khoản", StringComparison.OrdinalIgnoreCase))
                        pmKey = "qr";
                    else
                        pmKey = "card";
                }

                string duration = "Đang tính giờ";
                if (inv.EndTime.HasValue)
                {
                    var span = inv.EndTime.Value - inv.StartTime;
                    int h = (int)span.TotalHours;
                    int m = span.Minutes;
                    duration = $"{h}h {m:00}p";
                }

                var invDetails = detailsByInvoice.ContainsKey(inv.Id)
                    ? detailsByInvoice[inv.Id].Select(d => new InvoiceLineDisplay
                    {
                        ItemName = itemsDict.ContainsKey(d.ServiceItemId) ? itemsDict[d.ServiceItemId].Name : $"Món #{d.ServiceItemId}",
                        Unit = itemsDict.ContainsKey(d.ServiceItemId) ? itemsDict[d.ServiceItemId].Unit : "Phần",
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        TotalAmount = d.TotalAmount
                    }).ToList()
                    : new List<InvoiceLineDisplay>();

                return new InvoiceRowViewModel
                {
                    Id = inv.Id,
                    InvoiceCode = inv.InvoiceCode,
                    TableName = tblName,
                    IsVipTable = isVip,
                    CustomerName = custName,
                    StaffName = staffName,
                    StartTime = inv.StartTime,
                    EndTime = inv.EndTime,
                    DurationText = duration,
                    TableAmount = inv.TableAmount,
                    ServiceAmount = inv.ServiceAmount,
                    DiscountAmount = inv.DiscountAmount,
                    TotalPayment = inv.TotalPayment,
                    PaymentMethodName = pmName,
                    PaymentMethodKey = pmKey,
                    Status = inv.Status,
                    Details = invDetails
                };
            }).ToList();

            var paidRows = rows.Where(r => r.Status == "Đã thanh toán").ToList();
            var vm = new InvoicesPageViewModel
            {
                TotalRevenue = paidRows.Sum(r => r.TotalPayment),
                PaidCount = paidRows.Count,
                PlayingCount = rows.Count(r => r.Status != "Đã thanh toán"),
                CashTotal = paidRows.Where(r => r.PaymentMethodKey == "cash").Sum(r => r.TotalPayment),
                TransferTotal = paidRows.Where(r => r.PaymentMethodKey == "qr").Sum(r => r.TotalPayment),
                CardTotal = paidRows.Where(r => r.PaymentMethodKey == "card").Sum(r => r.TotalPayment)
            };

            var filtered = rows.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var kw = searchString.Trim().ToLower();
                filtered = filtered.Where(r =>
                    r.InvoiceCode.ToLower().Contains(kw) ||
                    r.CustomerName.ToLower().Contains(kw) ||
                    r.TableName.ToLower().Contains(kw) ||
                    r.StaffName.ToLower().Contains(kw));
            }
            if (!string.IsNullOrWhiteSpace(methodKey))
            {
                filtered = filtered.Where(r => r.PaymentMethodKey == methodKey);
            }
            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                filtered = filtered.Where(r => r.Status == statusFilter);
            }

            vm.Invoices = filtered.ToList();
            ViewBag.SearchString = searchString;
            ViewBag.MethodKey = methodKey;
            ViewBag.StatusFilter = statusFilter;

            return View(vm);
        }
    }
}

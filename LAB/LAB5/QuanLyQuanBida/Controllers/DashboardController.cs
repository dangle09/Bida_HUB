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
    // CONTROLLER: TRANG CHỦ TỔNG QUAN HỆ THỐNG QUẢN TRỊ (DASHBOARD)
    // - Route: /Dashboard hoặc /Dashboard/Index
    // - Tổng hợp & đồng bộ 100% số liệu trực tiếp từ SQL Server qua EF Core
    // - Phân quyền: CashierOrAbove (Admin: full quản trị, Manager: vận hành trừ nhật ký bảo mật, Cashier: chỉ tác vụ thu ngân ca trực; Waiter: 403)
    // ===================================================================================
    [Authorize(Policy = "CashierOrAbove")]
    public class DashboardController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public DashboardController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = User.IsInRole("Admin");
            bool isAdminOrManager = isAdmin || User.IsInRole("Manager");
            ViewData["Title"] = isAdminOrManager
                ? "Trang chủ Tổng quan - BOLT BILLIARDS"
                : "Bàn làm việc Thu ngân - BOLT BILLIARDS";

            var today = DateTime.Today;

            var invoices = await _context.Invoices.AsNoTracking().OrderByDescending(i => i.StartTime).ToListAsync();
            var invoiceDetails = await _context.InvoiceDetails.AsNoTracking().ToListAsync();
            var vouchers = await _context.PaymentVouchers.AsNoTracking().ToListAsync();
            var tables = await _context.BilliardTables.AsNoTracking().ToListAsync();
            var areas = await _context.TableAreas.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
            var categories = await _context.Categories.AsNoTracking().ToListAsync();
            var items = await _context.ServiceItems.AsNoTracking().ToListAsync();
            var inventories = await _context.Inventories.AsNoTracking().ToListAsync();
            var receiptDetails = await _context.GoodsReceiptDetails.AsNoTracking().ToListAsync();
            var users = await _context.Users.AsNoTracking().ToListAsync();
            var customers = await _context.Customers.AsNoTracking().OrderByDescending(c => c.AccumulatedPoints).ToListAsync();
            var tiers = await _context.MembershipTiers.AsNoTracking().ToDictionaryAsync(t => t.Id, t => t);
            var recentLogs = await _context.ActivityLogs.AsNoTracking().OrderByDescending(l => l.CreatedAt).Take(7).ToListAsync();

            var tableDict = tables.ToDictionary(t => t.Id, t => t);
            var customerDict = customers.ToDictionary(c => c.Id, c => c.FullName);
            var userDict = users.ToDictionary(u => u.Id, u => u.FullName);
            var catDict = categories.ToDictionary(c => c.Id, c => c.Name);
            var itemDict = items.ToDictionary(i => i.Id, i => i);

            // Tính toán tồn kho & giá trị kho đồng bộ 100% với /Inventory & /Menu
            var invDict = inventories.GroupBy(i => i.ServiceItemId)
                .ToDictionary(g => g.Key, g => new { Stock = g.Sum(x => x.StockQuantity), Reorder = g.First().ReorderLevel });
            var avgCostDict = receiptDetails.GroupBy(d => d.ServiceItemId)
                .ToDictionary(g => g.Key, g => g.Average(x => x.ImportPrice));

            var inventoryRows = items.Select(i =>
            {
                int stock = invDict.ContainsKey(i.Id) ? invDict[i.Id].Stock : 0;
                int reorder = invDict.ContainsKey(i.Id) ? invDict[i.Id].Reorder : 10;
                decimal cost = avgCostDict.ContainsKey(i.Id) ? avgCostDict[i.Id] : Math.Round(i.UnitPrice * 0.58m, 0);
                return new InventoryItemRow
                {
                    ServiceItemId = i.Id,
                    Code = i.Code,
                    Name = i.Name,
                    CategoryName = catDict.ContainsKey(i.CategoryId) ? catDict[i.CategoryId] : "Khác",
                    Unit = i.Unit,
                    Image = i.Image,
                    StockQuantity = stock,
                    ReorderLevel = reorder,
                    AvgImportPrice = cost,
                    UnitPrice = i.UnitPrice,
                    IsActive = i.IsActive
                };
            }).ToList();

            var paidInvoices = invoices.Where(i => i.Status == "Đã thanh toán").ToList();
            var paidIds = paidInvoices.Select(i => i.Id).ToHashSet();

            // Công suất bàn theo khu vực
            var areaRows = areas.Select(a =>
            {
                var areaTables = tables.Where(t => t.TableAreaId == a.Id).ToList();
                return new AreaOccupancyRow
                {
                    AreaName = a.Name ?? $"Khu vực #{a.Id}",
                    TotalCount = areaTables.Count,
                    PlayingCount = areaTables.Count(t => string.Equals(t.Status, "Đang chơi", StringComparison.OrdinalIgnoreCase)),
                    ReservedCount = areaTables.Count(t => (t.Status ?? string.Empty).Contains("đặt", StringComparison.OrdinalIgnoreCase)),
                    AvailableCount = areaTables.Count(t => string.Equals(t.Status, "Trống", StringComparison.OrdinalIgnoreCase))
                };
            }).ToList();

            // 6 hóa đơn gần nhất
            var recentInvoices = invoices.Take(6).Select(inv => new InvoiceRowViewModel
            {
                Id = inv.Id,
                InvoiceCode = inv.InvoiceCode,
                TableName = tableDict.ContainsKey(inv.BilliardTableId) ? tableDict[inv.BilliardTableId].Name : $"Bàn #{inv.BilliardTableId}",
                CustomerName = customerDict.ContainsKey(inv.CustomerId) ? customerDict[inv.CustomerId] : "Khách vãng lai",
                StaffName = userDict.ContainsKey(inv.UserId) ? userDict[inv.UserId] : $"NV #{inv.UserId}",
                StartTime = inv.StartTime,
                TotalPayment = inv.TotalPayment,
                Status = inv.Status
            }).ToList();

            // Top món bán chạy
            var topItems = invoiceDetails
                .Where(d => paidIds.Contains(d.InvoiceId))
                .GroupBy(d => d.ServiceItemId)
                .Select(g => new TopItemSalesRow
                {
                    Code = itemDict.ContainsKey(g.Key) ? itemDict[g.Key].Code : $"SP#{g.Key}",
                    Name = itemDict.ContainsKey(g.Key) ? itemDict[g.Key].Name : $"Món #{g.Key}",
                    Unit = itemDict.ContainsKey(g.Key) ? itemDict[g.Key].Unit : "Phần",
                    TotalQty = g.Sum(x => x.Quantity),
                    TotalRevenue = g.Sum(x => x.TotalAmount)
                })
                .OrderByDescending(x => x.TotalRevenue)
                .Take(5)
                .ToList();

            // Top 7 hội viên VIP tích điểm
            var spendByCust = paidInvoices.GroupBy(i => i.CustomerId)
                .ToDictionary(g => g.Key, g => new { Total = g.Sum(x => x.TotalPayment), Count = g.Count() });
            var topCustomers = customers.Take(7).Select(c => new CustomerRowViewModel
            {
                Id = c.Id,
                Code = c.Code,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                TierName = tiers.ContainsKey(c.MembershipTierId) ? tiers[c.MembershipTierId].Name : "Hội viên",
                AccumulatedPoints = c.AccumulatedPoints,
                TotalSpent = spendByCust.ContainsKey(c.Id) ? spendByCust[c.Id].Total : 0m,
                InvoiceCount = spendByCust.ContainsKey(c.Id) ? spendByCust[c.Id].Count : 0
            }).ToList();

            var vm = new DashboardOverviewViewModel
            {
                // Phân quyền dữ liệu ngay từ Server: Cashier không nhận số liệu tài chính tổng CLB, chi phí, giá trị kho, nhân sự, VIP hay nhật ký bảo mật
                TotalRevenue = isAdminOrManager ? paidInvoices.Sum(i => i.TotalPayment) : 0m,
                TodayRevenue = paidInvoices.Where(i => i.StartTime.Date == today).Sum(i => i.TotalPayment),
                TotalExpenses = isAdminOrManager ? vouchers.Sum(v => v.Amount) : 0m,
                PlayingTables = tables.Count(t => t.Status == "Đang chơi"),
                TotalTables = tables.Count,
                PaidInvoicesCount = paidInvoices.Count,
                PlayingInvoicesCount = invoices.Count(i => i.Status != "Đã thanh toán"),
                TotalCategories = categories.Count,
                TotalMenuItems = items.Count,
                ActiveMenuItems = items.Count(i => i.IsActive),
                LowStockCount = isAdminOrManager ? inventoryRows.Count(r => r.StockQuantity <= r.ReorderLevel) : 0,
                TotalInventoryValue = isAdminOrManager ? inventoryRows.Sum(r => r.StockQuantity * r.AvgImportPrice) : 0m,
                TotalStaff = isAdmin ? users.Count : 0,
                ActiveStaff = isAdmin ? users.Count(u => u.IsActive) : 0,
                TotalCustomers = isAdminOrManager ? customers.Count : 0,
                VipCustomers = isAdminOrManager ? customers.Count(c => tiers.ContainsKey(c.MembershipTierId) && ((tiers[c.MembershipTierId].Name ?? string.Empty).Contains("Kim Cương") || (tiers[c.MembershipTierId].Name ?? string.Empty).Contains("Vàng"))) : 0,
                AreaOccupancies = areaRows,
                RecentInvoices = recentInvoices,
                LowStockItems = isAdminOrManager ? inventoryRows.Where(r => r.StockQuantity <= r.ReorderLevel).OrderBy(r => r.StockQuantity).Take(6).ToList() : new List<InventoryItemRow>(),
                TopSellingItems = isAdminOrManager
                    ? topItems
                    : topItems.Select(t => new TopItemSalesRow { Code = t.Code, Name = t.Name, Unit = t.Unit, TotalQty = t.TotalQty, TotalRevenue = 0m }).ToList(),
                TopCustomers = isAdminOrManager ? topCustomers : new List<CustomerRowViewModel>(),
                RecentLogs = isAdmin ? recentLogs : new List<Models.ActivityLog>(),
                UserNameDict = isAdmin ? userDict : new Dictionary<int, string>()
            };

            return View(vm);
        }
    }
}

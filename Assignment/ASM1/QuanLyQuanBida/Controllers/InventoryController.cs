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
    // CONTROLLER: QUẢN LÝ HÀNG HÓA - KHO & NHẬP HÀNG (ĐỒNG BỘ 100% SQL SERVER QUA EF CORE)
    // - Đồng bộ trực tiếp với Thực đơn (/Menu): chung bảng ServiceItems & Inventories
    // - Khi lập phiếu nhập hàng hoặc kiểm kê kho -> tự động cập nhật ngay sang /Menu
    // ===================================================================================
    [Authorize(Policy = "AdminOrManager")]
    public class InventoryController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public InventoryController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString, string? status)
        {
            ViewData["Title"] = "Kho & Nhập hàng - BOLT BILLIARDS";

            var items = await _context.ServiceItems.AsNoTracking().OrderBy(s => s.CategoryId).ThenBy(s => s.Id).ToListAsync();
            var categories = await _context.Categories.AsNoTracking().ToDictionaryAsync(c => c.Id, c => c.Name);
            var inventories = await _context.Inventories.AsNoTracking()
                .GroupBy(i => i.ServiceItemId)
                .ToDictionaryAsync(g => g.Key, g => new
                {
                    Stock = g.Sum(x => x.StockQuantity),
                    Reorder = g.First().ReorderLevel
                });

            var receiptDetails = await _context.GoodsReceiptDetails.AsNoTracking().ToListAsync();
            var avgPriceByItem = receiptDetails
                .GroupBy(d => d.ServiceItemId)
                .ToDictionary(g => g.Key, g => g.Average(x => x.ImportPrice));

            var rows = items.Select(item =>
            {
                int stock = inventories.ContainsKey(item.Id) ? inventories[item.Id].Stock : 0;
                int reorder = inventories.ContainsKey(item.Id) ? inventories[item.Id].Reorder : 10;
                decimal avgCost = avgPriceByItem.ContainsKey(item.Id) ? avgPriceByItem[item.Id] : Math.Round(item.UnitPrice * 0.58m, 0);

                return new InventoryItemRow
                {
                    ServiceItemId = item.Id,
                    Code = item.Code,
                    Name = item.Name,
                    CategoryName = categories.ContainsKey(item.CategoryId) ? categories[item.CategoryId] : "Khác",
                    Unit = item.Unit,
                    Image = item.Image,
                    StockQuantity = stock,
                    ReorderLevel = reorder,
                    AvgImportPrice = avgCost,
                    UnitPrice = item.UnitPrice,
                    IsActive = item.IsActive
                };
            }).ToList();

            var vm = new InventoryDashboardViewModel
            {
                TotalInventoryValue = rows.Sum(r => r.StockQuantity * r.AvgImportPrice),
                LowStockCount = rows.Count(r => r.StockQuantity <= r.ReorderLevel)
            };

            // Lọc tìm kiếm nếu có
            var filteredRows = rows.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var kw = searchString.Trim().ToLower();
                filteredRows = filteredRows.Where(r => r.Name.ToLower().Contains(kw) || r.Code.ToLower().Contains(kw) || r.CategoryName.ToLower().Contains(kw));
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                filteredRows = filteredRows.Where(r => r.StatusKey == status);
            }
            vm.Items = filteredRows.OrderBy(r => r.StockQuantity <= r.ReorderLevel ? 0 : 1).ThenBy(r => r.ServiceItemId).ToList();

            // Lịch sử phiếu nhập thật từ SQL Server
            var suppliers = await _context.Suppliers.AsNoTracking().OrderBy(s => s.Id).ToListAsync();
            var supDict = suppliers.ToDictionary(s => s.Id, s => s.Name);
            var userDict = await _context.Users.AsNoTracking().ToDictionaryAsync(u => u.Id, u => u.FullName);
            var itemDict = items.ToDictionary(i => i.Id, i => i.Name);

            var receipts = await _context.GoodsReceipts.AsNoTracking().OrderByDescending(g => g.ImportDate).ToListAsync();
            var detailsByReceipt = receiptDetails.GroupBy(d => d.GoodsReceiptId).ToDictionary(g => g.Key, g => g.ToList());

            vm.Receipts = receipts.Select(r =>
            {
                var lines = detailsByReceipt.ContainsKey(r.Id) ? detailsByReceipt[r.Id] : new List<GoodsReceiptDetail>();
                var summary = lines.Any()
                    ? string.Join(", ", lines.Take(3).Select(l => $"{(itemDict.ContainsKey(l.ServiceItemId) ? itemDict[l.ServiceItemId] : $"SP #{l.ServiceItemId}")} (x{l.Quantity})"))
                    : "Phiếu nhập hàng tổng hợp";

                return new GoodsReceiptRow
                {
                    Id = r.Id,
                    Code = r.Code,
                    SupplierName = supDict.ContainsKey(r.SupplierId) ? supDict[r.SupplierId] : "Nhà cung cấp",
                    StaffName = userDict.ContainsKey(r.UserId) ? userDict[r.UserId] : "Quản trị viên",
                    ImportDate = r.ImportDate,
                    TotalImportAmount = r.TotalImportAmount,
                    ItemsSummary = summary,
                    LineCount = lines.Count
                };
            }).ToList();

            vm.TotalImportedAmount = receipts.Sum(r => r.TotalImportAmount);
            vm.Suppliers = suppliers;

            ViewBag.AllItems = rows;
            ViewBag.SearchString = searchString;
            ViewBag.SelectedStatus = status;

            return View(vm);
        }

        // POST: /Inventory/ImportStock — Lập phiếu nhập hàng thật, tự động cộng tồn kho đồng bộ sang /Menu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImportStock(int supplierId, int serviceItemId, int quantity, decimal importPrice)
        {
            if (quantity <= 0 || importPrice < 0)
            {
                TempData["ErrorMessage"] = "Số lượng nhập phải lớn hơn 0 và đơn giá không âm.";
                return RedirectToAction(nameof(Index));
            }

            var item = await _context.ServiceItems.FindAsync(serviceItemId);
            if (item == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy mặt hàng cần nhập.";
                return RedirectToAction(nameof(Index));
            }

            int userId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 1;
            string code = $"PN-{DateTime.Now:yyMM}-{DateTime.Now:HHmmss}";
            decimal total = quantity * importPrice;

            var receipt = new GoodsReceipt
            {
                SupplierId = supplierId,
                UserId = userId,
                Code = code,
                ImportDate = DateTime.Now,
                TotalImportAmount = total
            };
            _context.GoodsReceipts.Add(receipt);
            await _context.SaveChangesAsync();

            _context.GoodsReceiptDetails.Add(new GoodsReceiptDetail
            {
                GoodsReceiptId = receipt.Id,
                ServiceItemId = serviceItemId,
                Quantity = quantity,
                ImportPrice = importPrice
            });

            var inv = await _context.Inventories.FirstOrDefaultAsync(i => i.ServiceItemId == serviceItemId);
            if (inv == null)
            {
                inv = new Inventory { ServiceItemId = serviceItemId, StockQuantity = quantity, ReorderLevel = 10 };
                _context.Inventories.Add(inv);
            }
            else
            {
                inv.StockQuantity += quantity;
            }

            _context.ActivityLogs.Add(new ActivityLog
            {
                UserId = userId,
                Action = "IMPORT_STOCK",
                TableName = "GoodsReceipts",
                RecordId = receipt.Id,
                Description = $"Nhập kho +{quantity} {item.Unit} '{item.Name}' (Phiếu {code} - {total:N0}đ)",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã nhập kho +{quantity} {item.Unit} '{item.Name}' (Mã phiếu {code}). Tồn kho mới: {inv.StockQuantity} {item.Unit} (đã đồng bộ sang Thực đơn).";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Inventory/UpdateAudit — Kiểm kê điều chỉnh tồn kho thực tế
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAudit(int serviceItemId, int actualQuantity, int reorderLevel, string? notes)
        {
            var item = await _context.ServiceItems.FindAsync(serviceItemId);
            if (item == null) return RedirectToAction(nameof(Index));

            var inv = await _context.Inventories.FirstOrDefaultAsync(i => i.ServiceItemId == serviceItemId);
            int oldQty = inv?.StockQuantity ?? 0;

            if (inv == null)
            {
                _context.Inventories.Add(new Inventory
                {
                    ServiceItemId = serviceItemId,
                    StockQuantity = Math.Max(0, actualQuantity),
                    ReorderLevel = Math.Max(1, reorderLevel)
                });
            }
            else
            {
                inv.StockQuantity = Math.Max(0, actualQuantity);
                inv.ReorderLevel = Math.Max(1, reorderLevel);
            }

            int userId = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var uid) ? uid : 1;
            _context.InventoryAudits.Add(new InventoryAudit
            {
                UserId = userId,
                Code = $"KK_{DateTime.Now:yyyyMMddHHmm}",
                AuditDate = DateTime.Now,
                Notes = string.IsNullOrWhiteSpace(notes)
                    ? $"Kiểm kê điều chỉnh '{item.Name}': {oldQty} -> {actualQuantity} {item.Unit}"
                    : notes.Trim()
            });

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã kiểm kê cập nhật tồn kho '{item.Name}': {oldQty} → {actualQuantity} {item.Unit} (Mức an toàn: {reorderLevel}).";
            return RedirectToAction(nameof(Index));
        }
    }
}

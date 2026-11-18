using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// ===================================================================================
// CONTROLLER LAB 5 (BÀI 1A + BÀI 3): QUẢN LÝ THỰC ĐƠN ĐỒ UỐNG, MÓN ĂN & DỊCH VỤ F&B
// - Kiến trúc: 100% ASP.NET Core MVC + Entity Framework Core (KHÔNG dùng DAL)
// - Route chính: /Menu (Index, Details, Create, Edit, Delete, ToggleStatus)
// - Phân quyền:
//     + ManagementAccess (Admin, Manager, Cashier): Xem danh sách, Chi tiết, Lọc & Tìm kiếm
//     + AdminOrManager (Admin, Manager): Thêm, Sửa, Xóa, Đổi trạng thái bán
// ===================================================================================
namespace QuanLyQuanBida.Controllers
{
    public class MenuController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;
        private readonly IWebHostEnvironment _env;

        public MenuController(QuanLyQuanBidaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // =========================================================================
        // 1. GET: /Menu/Index?categoryId=1&searchString=... (BÀI 1A + BÀI 3)
        // =========================================================================
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Index(int? categoryId, string? searchString, int page = 1)
        {
            ViewData["Title"] = "Thực đơn & Dịch vụ - BOLT BILLIARDS";
            const int pageSize = 30;

            var categories = await _context.Categories.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
            var categoryDict = categories.ToDictionary(c => c.Id, c => c);

            // Đọc toàn bộ thực đơn để tính KPI tổng quan chuẩn xác từ SQL Server
            var allItems = await _context.ServiceItems.AsNoTracking().ToListAsync();
            ViewBag.TotalAllItems = allItems.Count;
            ViewBag.TotalActiveItems = allItems.Count(x => x.IsActive);
            ViewBag.DrinkCount = allItems.Count(x => categoryDict.ContainsKey(x.CategoryId) && (categoryDict[x.CategoryId].Code == "DRINK" || categoryDict[x.CategoryId].Code == "BEER"));
            ViewBag.FoodCount = allItems.Count(x => categoryDict.ContainsKey(x.CategoryId) && categoryDict[x.CategoryId].Code == "FOOD");
            ViewBag.AccessoryCount = allItems.Count(x => categoryDict.ContainsKey(x.CategoryId) && categoryDict[x.CategoryId].Code == "ACCESSORIES");

            // BÀI 3: Truy vấn Lọc theo Loại (CategoryId) và Tìm kiếm theo Tên / Mã món trên SQL Server
            var query = _context.ServiceItems.AsNoTracking().AsQueryable();

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(s => s.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var kw = searchString.Trim();
                query = query.Where(s => s.Name.Contains(kw) || s.Code.Contains(kw));
            }

            var allFiltered = await query.OrderBy(s => s.CategoryId).ThenBy(s => s.Id).ToListAsync();
            int totalFiltered = allFiltered.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(totalFiltered / (double)pageSize));
            page = Math.Clamp(page, 1, totalPages);
            var items = allFiltered.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Đọc số lượng tồn kho và định mức an toàn thực tế từ bảng Inventories (đồng bộ 100% với /Inventory)
            var invRows = await _context.Inventories.AsNoTracking().ToListAsync();
            var stockDict = invRows
                .GroupBy(i => i.ServiceItemId)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.StockQuantity));
            var reorderDict = invRows
                .GroupBy(i => i.ServiceItemId)
                .ToDictionary(g => g.Key, g => g.First().ReorderLevel);

            ViewBag.Categories = categories;
            ViewBag.CategoryDict = categoryDict;
            ViewBag.StockDict = stockDict;
            ViewBag.ReorderDict = reorderDict;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalFiltered = totalFiltered;

            return View(items);
        }

        // =========================================================================
        // 2. GET: /Menu/Details/5
        // =========================================================================
        [Authorize(Policy = "ManagementAccess")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.ServiceItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == item.CategoryId);
            var inv = await _context.Inventories.AsNoTracking().FirstOrDefaultAsync(i => i.ServiceItemId == item.Id);

            bool isAdminOrManager = User.IsInRole("Admin") || User.IsInRole("Manager");
            var salesLines = isAdminOrManager
                ? await _context.InvoiceDetails.AsNoTracking().Where(d => d.ServiceItemId == item.Id).ToListAsync()
                : new List<InvoiceDetail>();
            var importLines = isAdminOrManager
                ? await _context.GoodsReceiptDetails.AsNoTracking().Where(r => r.ServiceItemId == item.Id).OrderByDescending(r => r.Id).ToListAsync()
                : new List<GoodsReceiptDetail>();

            var receiptIds = importLines.Select(l => l.GoodsReceiptId).Distinct().ToList();
            var receiptDict = isAdminOrManager && receiptIds.Any()
                ? await _context.GoodsReceipts.AsNoTracking().Where(gr => receiptIds.Contains(gr.Id)).ToDictionaryAsync(gr => gr.Id, gr => gr)
                : new Dictionary<int, GoodsReceipt>();

            ViewBag.CategoryName = category != null ? $"{category.Name} ({category.Code})" : "Chưa phân loại";
            ViewBag.CategoryCode = category?.Code ?? "N/A";
            ViewBag.StockQty = inv?.StockQuantity ?? 0;
            ViewBag.SafetyStock = inv?.ReorderLevel ?? 10;
            ViewBag.TotalSoldQty = isAdminOrManager ? salesLines.Sum(s => s.Quantity) : 0;
            ViewBag.TotalSalesRevenue = isAdminOrManager ? salesLines.Sum(s => s.TotalAmount) : 0m;
            ViewBag.TotalImportedQty = isAdminOrManager ? importLines.Sum(r => r.Quantity) : 0;
            ViewBag.AvgImportPrice = isAdminOrManager
                ? (importLines.Any() ? importLines.Average(r => r.ImportPrice) : Math.Round(item.UnitPrice * 0.55m, 0))
                : 0m;
            ViewBag.ImportLines = isAdminOrManager ? importLines.Take(6).ToList() : new List<GoodsReceiptDetail>();
            ViewBag.ReceiptDict = receiptDict;
            ViewData["Title"] = $"Chi tiết món: {item.Name}";

            return View(item);
        }

        // =========================================================================
        // 3. GET: /Menu/Create
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Thêm món mới vào thực đơn";
            await PopulateCategoriesDropDown();
            return View(new ServiceItem { IsActive = true, Unit = "Ly", UnitPrice = 25000 });
        }

        // POST: /Menu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Create([Bind("CategoryId,Code,Name,Unit,UnitPrice,Image,IsActive")] ServiceItem item, IFormFile? ImageFile, int InitialStock = 50)
        {
            if (string.IsNullOrWhiteSpace(item.Code))
                ModelState.AddModelError("Code", "Mã món không được để trống.");
            if (string.IsNullOrWhiteSpace(item.Name))
                ModelState.AddModelError("Name", "Tên món ăn / đồ uống không được để trống.");
            if (string.IsNullOrWhiteSpace(item.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống.");
            if (item.UnitPrice < 0)
                ModelState.AddModelError("UnitPrice", "Đơn giá phải lớn hơn hoặc bằng 0.");

            if (!string.IsNullOrWhiteSpace(item.Code))
            {
                var normalizedCode = item.Code.Trim().ToUpper();
                bool exists = await _context.ServiceItems.AnyAsync(s => s.Code == normalizedCode);
                if (exists)
                {
                    ModelState.AddModelError("Code", $"Mã món '{normalizedCode}' đã tồn tại trong hệ thống.");
                }
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "fnb");
                    Directory.CreateDirectory(uploadDir);
                    var ext = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                    var fileName = $"menu_{Guid.NewGuid():N}{ext}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    item.Image = $"images/fnb/{fileName}";
                }

                item.Code = item.Code.Trim().ToUpper();
                item.Name = item.Name.Trim();
                item.Unit = item.Unit.Trim();

                _context.ServiceItems.Add(item);
                await _context.SaveChangesAsync();

                // Khởi tạo tồn kho ban đầu đồng bộ trong SQL Server
                _context.Inventories.Add(new Inventory
                {
                    ServiceItemId = item.Id,
                    StockQuantity = Math.Max(0, InitialStock),
                    ReorderLevel = 10
                });
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã thêm món mới thành công: {item.Name} ({item.Code}) — {item.UnitPrice:N0} đ";
                return RedirectToAction(nameof(Index));
            }

            await PopulateCategoriesDropDown(item.CategoryId);
            return View(item);
        }

        // =========================================================================
        // 4. GET: /Menu/Edit/5
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null) return NotFound();

            ViewData["Title"] = $"Chỉnh sửa món: {item.Name}";
            await PopulateCategoriesDropDown(item.CategoryId);
            return View(item);
        }

        // POST: /Menu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Code,Name,Unit,UnitPrice,Image,IsActive")] ServiceItem item, IFormFile? ImageFile, string? ExistingImage)
        {
            if (id != item.Id) return NotFound();

            if (string.IsNullOrWhiteSpace(item.Code))
                ModelState.AddModelError("Code", "Mã món không được để trống.");
            if (string.IsNullOrWhiteSpace(item.Name))
                ModelState.AddModelError("Name", "Tên món không được để trống.");
            if (item.UnitPrice < 0)
                ModelState.AddModelError("UnitPrice", "Đơn giá phải lớn hơn hoặc bằng 0.");

            if (!string.IsNullOrWhiteSpace(item.Code))
            {
                var normalizedCode = item.Code.Trim().ToUpper();
                bool duplicate = await _context.ServiceItems.AnyAsync(s => s.Code == normalizedCode && s.Id != item.Id);
                if (duplicate)
                {
                    ModelState.AddModelError("Code", $"Mã món '{normalizedCode}' đã được sử dụng cho món khác.");
                }
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "fnb");
                    Directory.CreateDirectory(uploadDir);
                    var ext = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                    var fileName = $"menu_{Guid.NewGuid():N}{ext}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    item.Image = $"images/fnb/{fileName}";
                }
                else if (string.IsNullOrWhiteSpace(item.Image) && !string.IsNullOrWhiteSpace(ExistingImage))
                {
                    item.Image = ExistingImage;
                }

                item.Code = item.Code.Trim().ToUpper();
                item.Name = item.Name.Trim();
                item.Unit = item.Unit.Trim();

                _context.Update(item);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã cập nhật món: {item.Name} ({item.Code})";
                return RedirectToAction(nameof(Index));
            }

            await PopulateCategoriesDropDown(item.CategoryId);
            return View(item);
        }

        // =========================================================================
        // 5. POST: /Menu/ToggleStatus/5 (Bật / Tắt trạng thái Đang bán / Tạm ngưng)
        // =========================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item == null) return NotFound();

            item.IsActive = !item.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = item.IsActive
                ? $"Đã mở bán lại món '{item.Name}'."
                : $"Đã chuyển món '{item.Name}' sang trạng thái Tạm ngưng bán.";

            return RedirectToAction(nameof(Index));
        }

        // =========================================================================
        // 6. GET: /Menu/Delete/5
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.ServiceItems.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (item == null) return NotFound();

            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == item.CategoryId);
            ViewBag.CategoryName = category?.Name ?? "Chưa phân loại";
            ViewBag.InvoiceCount = await _context.InvoiceDetails.CountAsync(d => d.ServiceItemId == item.Id);
            ViewData["Title"] = $"Xác nhận xóa món: {item.Name}";

            return View(item);
        }

        // POST: /Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ServiceItems.FindAsync(id);
            if (item != null)
            {
                var inventories = await _context.Inventories.Where(i => i.ServiceItemId == id).ToListAsync();
                if (inventories.Any()) _context.Inventories.RemoveRange(inventories);

                var invoiceDetails = await _context.InvoiceDetails.Where(d => d.ServiceItemId == id).ToListAsync();
                if (invoiceDetails.Any()) _context.InvoiceDetails.RemoveRange(invoiceDetails);

                var receiptDetails = await _context.GoodsReceiptDetails.Where(r => r.ServiceItemId == id).ToListAsync();
                if (receiptDetails.Any()) _context.GoodsReceiptDetails.RemoveRange(receiptDetails);

                _context.ServiceItems.Remove(item);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã xóa vĩnh viễn món '{item.Name}' ({item.Code}) khỏi SQL Server.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateCategoriesDropDown(int? selectedId = null)
        {
            var cats = await _context.Categories.AsNoTracking().OrderBy(c => c.Id).ToListAsync();
            ViewBag.CategoryList = new SelectList(cats, "Id", "Name", selectedId);
        }
    }
}

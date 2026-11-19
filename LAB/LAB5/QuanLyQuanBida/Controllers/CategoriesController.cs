using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// ===================================================================================
// CONTROLLER CỦA WEB QUẢN LÝ (TAB 1: QUẢN LÝ DANH MỤC HÀNG HÓA & DỊCH VỤ)
// - Route chính: http://localhost:PORT/Categories hoặc /Categories/Index
// - Quản lý các View trong thư mục: Views/Categories/ (Index, Create, Edit, Details, Delete)
// - Sử dụng khung giao diện chung: Views/Shared/_AdminLayout.cshtml
// - Phân quyền:
//     + ManagementAccess (Admin, Manager, Cashier): Được xem Index, Details
//     + AdminOrManager (Admin, Manager): Được Thêm (Create), Sửa (Edit), Xóa (Delete)
// ===================================================================================
namespace QuanLyQuanBida.Controllers
{
    /// <summary>
    /// Controller Quản lý Danh mục Đồ uống & Dịch vụ (Chuẩn kiến trúc ASP.NET Core MVC + Entity Framework Core)
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoriesController(QuanLyQuanBidaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // =========================================================================
        // 1. GET: /Categories/Index (Policy: ManagementAccess - Admin, Manager, Cashier)
        // =========================================================================
        [Authorize(Policy = "CashierOrAbove")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Quản lý danh mục đồ uống & dịch vụ";
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            // Đếm số lượng sản phẩm/dịch vụ thuộc từng danh mục
            var counts = await _context.ServiceItems
                .GroupBy(s => s.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CategoryId, x => x.Count);

            ViewBag.ItemCounts = counts;
            return View(categories);
        }

        // =========================================================================
        // 2. GET: /Categories/Details/5 (Policy: ManagementAccess)
        // =========================================================================
        [Authorize(Policy = "CashierOrAbove")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            // Danh sách các món ăn / đồ uống / dụng cụ thuộc danh mục (khớp 100% web vãng lai)
            var items = await _context.ServiceItems.AsNoTracking()
                .Where(s => s.CategoryId == id)
                .OrderBy(s => s.UnitPrice)
                .ToListAsync();

            var itemIds = items.Select(i => i.Id).ToList();
            var stockDict = await _context.Inventories.AsNoTracking()
                .Where(inv => itemIds.Contains(inv.ServiceItemId))
                .GroupBy(inv => inv.ServiceItemId)
                .ToDictionaryAsync(g => g.Key, g => g.Sum(x => x.StockQuantity));

            ViewBag.Items = items;
            ViewBag.StockDict = stockDict;
            ViewBag.TotalStock = stockDict.Values.Sum();
            ViewBag.ActiveCount = items.Count(x => x.IsActive);
            return View(category);
        }

        // =========================================================================
        // 3. GET: /Categories/Create (Policy: AdminOrManager - Cả Admin và Manager)
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Thêm mới danh mục";
            return View();
        }

        // POST: /Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Create([Bind("Code,Name,Image")] Category category, IFormFile? ImageFile)
        {
            if (string.IsNullOrWhiteSpace(category.Code))
            {
                ModelState.AddModelError("Code", "Mã danh mục không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "Tên danh mục không được để trống.");
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh đại diện sản phẩm / danh mục
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "categories");
                    Directory.CreateDirectory(uploadDir);
                    var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                    var fileName = $"cat_{Guid.NewGuid():N}{extension}";
                    var filePath = Path.Combine(uploadDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    category.Image = $"images/categories/{fileName}";
                }

                category.Code = category.Code.Trim().ToUpper();
                category.Name = category.Name.Trim();

                _context.Add(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã thêm thành công danh mục: {category.Name} ({category.Code})";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // =========================================================================
        // 4. GET: /Categories/Edit/5 (Policy: AdminOrManager - Cả Admin và Manager)
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Chỉnh sửa danh mục";
            return View(category);
        }

        // POST: /Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Image")] Category category, IFormFile? ImageFile, string? ExistingImage)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (string.IsNullOrWhiteSpace(category.Code))
            {
                ModelState.AddModelError("Code", "Mã danh mục không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "Tên danh mục không được để trống.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Xử lý cập nhật ảnh mới hoặc giữ ảnh cũ
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var uploadDir = Path.Combine(_env.WebRootPath, "images", "categories");
                        Directory.CreateDirectory(uploadDir);
                        var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                        var fileName = $"cat_{Guid.NewGuid():N}{extension}";
                        var filePath = Path.Combine(uploadDir, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }
                        category.Image = $"images/categories/{fileName}";
                    }
                    else if (string.IsNullOrWhiteSpace(category.Image) && !string.IsNullOrWhiteSpace(ExistingImage))
                    {
                        category.Image = ExistingImage;
                    }

                    category.Code = category.Code.Trim().ToUpper();
                    category.Name = category.Name.Trim();

                    _context.Update(category);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"Đã cập nhật danh mục: {category.Name} ({category.Code})";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(category);
        }

        // =========================================================================
        // 5. GET: /Categories/Delete/5 (Policy: AdminOrManager - Cả Admin và Manager)
        // =========================================================================
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            var itemCount = await _context.ServiceItems.CountAsync(s => s.CategoryId == id);
            ViewBag.ItemCount = itemCount;

            ViewData["Title"] = "Xác nhận xóa danh mục";
            return View(category);
        }

        // POST: /Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOrManager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                // Xóa tuần tự các bản ghi liên kết khóa ngoại qua EF Core DbContext (chuẩn MVC thuần)
                var itemIds = await _context.ServiceItems
                    .Where(s => s.CategoryId == id)
                    .Select(s => s.Id)
                    .ToListAsync();

                if (itemIds.Any())
                {
                    var inventories = await _context.Inventories
                        .Where(i => itemIds.Contains(i.ServiceItemId))
                        .ToListAsync();
                    if (inventories.Any()) _context.Inventories.RemoveRange(inventories);

                    var invoiceDetails = await _context.InvoiceDetails
                        .Where(d => itemIds.Contains(d.ServiceItemId))
                        .ToListAsync();
                    if (invoiceDetails.Any()) _context.InvoiceDetails.RemoveRange(invoiceDetails);

                    var receiptDetails = await _context.GoodsReceiptDetails
                        .Where(r => itemIds.Contains(r.ServiceItemId))
                        .ToListAsync();
                    if (receiptDetails.Any()) _context.GoodsReceiptDetails.RemoveRange(receiptDetails);

                    var items = await _context.ServiceItems
                        .Where(s => s.CategoryId == id)
                        .ToListAsync();
                    _context.ServiceItems.RemoveRange(items);
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = itemIds.Any()
                    ? $"Đã xóa thành công danh mục '{category.Name}' cùng {itemIds.Count} món/dụng cụ liên kết."
                    : $"Đã xóa thành công danh mục '{category.Name}'.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
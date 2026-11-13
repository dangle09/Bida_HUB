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

namespace QuanLyQuanBida.Controllers
{
    /// <summary>
    /// Controller Quản lý Danh mục Đồ uống & Dịch vụ (Chuẩn kiến trúc ASP.NET Core MVC + Entity Framework Core)
    /// Áp dụng Phân quyền:
    /// - ManagementAccess: Admin, Manager, Cashier đều có quyền xem (Index, Details).
    /// - AdminOrManager: Cả Admin và Manager đều có toàn quyền Thêm (Create), Sửa (Edit), Xóa (Delete). Cashier bị chặn 403.
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
        [Authorize(Policy = "ManagementAccess")]
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
        [Authorize(Policy = "ManagementAccess")]
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
            var items = await _context.ServiceItems
                .Where(s => s.CategoryId == id)
                .OrderBy(s => s.UnitPrice)
                .ToListAsync();

            ViewBag.Items = items;
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
                var itemCount = await _context.ServiceItems.CountAsync(s => s.CategoryId == id);
                if (itemCount > 0)
                {
                    TempData["ErrorMessage"] = $"Không thể xóa danh mục '{category.Name}' vì đang có {itemCount} món/dụng cụ liên kết.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã xóa thành công danh mục '{category.Name}'.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}
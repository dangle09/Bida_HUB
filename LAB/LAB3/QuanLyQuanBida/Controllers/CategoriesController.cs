using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public CategoriesController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string? search)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(term) || c.Code.ToLower().Contains(term));
                ViewData["Search"] = search.Trim();
            }

            var categories = await query.OrderBy(c => c.Id).ToListAsync();

            // Đếm số lượng sản phẩm/dịch vụ theo từng danh mục
            var itemCounts = await _context.ServiceItems
                .GroupBy(s => s.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CategoryId, x => x.Count);

            ViewBag.ItemCounts = itemCounts;

            return View(categories);
        }

        // GET: Categories/Details/5
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

            // Lấy danh sách sản phẩm/dịch vụ thuộc danh mục này
            var serviceItems = await _context.ServiceItems
                .Where(s => s.CategoryId == id)
                .OrderBy(s => s.Name)
                .ToListAsync();

            ViewBag.ServiceItems = serviceItems;

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name")] Category category)
        {
            if (await _context.Categories.AnyAsync(c => c.Code.ToLower() == category.Code.Trim().ToLower()))
            {
                ModelState.AddModelError("Code", "Mã danh mục này đã tồn tại trong hệ thống.");
            }

            if (ModelState.IsValid)
            {
                category.Code = category.Code.Trim().ToUpper();
                category.Name = category.Name.Trim();
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã thêm mới danh mục \"{category.Name}\" thành công!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
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
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (await _context.Categories.AnyAsync(c => c.Code.ToLower() == category.Code.Trim().ToLower() && c.Id != id))
            {
                ModelState.AddModelError("Code", "Mã danh mục này đã thuộc về danh mục khác.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.Code = category.Code.Trim().ToUpper();
                    category.Name = category.Name.Trim();
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Đã cập nhật danh mục \"{category.Name}\" thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
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

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                // Kiểm tra ràng buộc khóa ngoại với ServiceItems
                var itemCount = await _context.ServiceItems.CountAsync(s => s.CategoryId == id);
                if (itemCount > 0)
                {
                    TempData["ErrorMessage"] = $"Không thể xóa danh mục \"{category.Name}\" vì đang có {itemCount} sản phẩm/dịch vụ liên kết!";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã xóa danh mục \"{category.Name}\" thành công!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

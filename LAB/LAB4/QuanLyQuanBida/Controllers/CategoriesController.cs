using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    /// <summary>
    /// Controller Quản lý Danh mục Đồ uống & Dịch vụ (Chuẩn kiến trúc ASP.NET Core MVC + Entity Framework Core)
    /// Áp dụng Phân quyền Policy: EmployeeOrManager và ManagerOnly
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public CategoriesController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        // =========================================================================
        // 1. GET: /Categories/Index (Policy: EmployeeOrManager - Cả nhân viên & quản lý đều xem được)
        // =========================================================================
        [Authorize(Policy = "EmployeeOrManager")]
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Quản lý danh mục đồ uống & dịch vụ";
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            // Đếm số lượng sản phẩm thuộc từng danh mục để hiển thị trực quan
            var counts = await _context.ServiceItems
                .GroupBy(s => s.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.CategoryId, x => x.Count);

            ViewBag.ItemCounts = counts;
            return View(categories);
        }

        // =========================================================================
        // 2. GET: /Categories/Details/5 (Policy: EmployeeOrManager)
        // =========================================================================
        [Authorize(Policy = "EmployeeOrManager")]
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

            // Danh sách các món ăn / đồ uống thuộc danh mục
            var items = await _context.ServiceItems
                .Where(s => s.CategoryId == id)
                .OrderBy(s => s.Name)
                .ToListAsync();

            ViewBag.Items = items;
            return View(category);
        }

        // =========================================================================
        // 3. GET: /Categories/Create (Policy: ManagerOnly - Chỉ Quản lý)
        // =========================================================================
        [Authorize(Policy = "ManagerOnly")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Thêm mới danh mục";
            return View();
        }

        // POST: /Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> Create([Bind("Code,Name")] Category category)
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
        // 4. GET: /Categories/Edit/5 (Policy: ManagerOnly - Chỉ Quản lý)
        // =========================================================================
        [Authorize(Policy = "ManagerOnly")]
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
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name")] Category category)
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
        // 5. GET: /Categories/Delete/5 (Policy: ManagerOnly - Chỉ Quản lý)
        // =========================================================================
        [Authorize(Policy = "ManagerOnly")]
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
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                var itemCount = await _context.ServiceItems.CountAsync(s => s.CategoryId == id);
                if (itemCount > 0)
                {
                    TempData["ErrorMessage"] = $"Không thể xóa danh mục '{category.Name}' vì đang có {itemCount} món dịch vụ liên kết.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Đã xóa danh mục '{category.Name}' thành công.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _context.Categories.AnyAsync(e => e.Id == id);
        }
    }
}

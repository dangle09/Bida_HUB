using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.Models;
using QuanLyQuanBida.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// ===================================================================================
// CONTROLLER LAB 5 (BÀI 1B): QUẢN LÝ NHÂN VIÊN & PHÂN QUYỀN TÀI KHOẢN
// - Kiến trúc: 100% ASP.NET Core MVC + Entity Framework Core (KHÔNG dùng DAL)
// - Route chính: /Staff (Index, Details, Create, Edit, Delete, ToggleActive)
// - Phân quyền: AdminOnly (Chỉ Admin mới có quyền quản trị nhân sự & vai trò)
// ===================================================================================
namespace QuanLyQuanBida.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class StaffController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;
        private readonly IWebHostEnvironment _env;

        public StaffController(QuanLyQuanBidaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // =========================================================================
        // 1. GET: /Staff/Index?roleId=...&searchString=...
        // =========================================================================
        public async Task<IActionResult> Index(int? roleId, string? searchString)
        {
            ViewData["Title"] = "Nhân sự & Phân ca - BOLT BILLIARDS";

            var roles = await _context.Roles.AsNoTracking().OrderBy(r => r.Id).ToListAsync();
            var roleDict = roles.ToDictionary(r => r.Id, r => r.Name);
            var userRoles = await _context.UserRoles.AsNoTracking().ToListAsync();
            var userRoleMap = userRoles
                .GroupBy(ur => ur.UserId)
                .ToDictionary(g => g.Key, g => g.First().RoleId);

            var allUsers = await _context.Users.AsNoTracking().OrderBy(u => u.Id).ToListAsync();

            var staffList = allUsers.Select(u =>
            {
                int rId = userRoleMap.ContainsKey(u.Id) ? userRoleMap[u.Id] : 0;
                return new StaffViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email ?? "",
                    PhoneNumber = u.PhoneNumber,
                    Avatar = u.Avatar,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    RoleId = rId,
                    RoleName = roleDict.ContainsKey(rId) ? roleDict[rId] : "Chưa gán"
                };
            }).ToList();

            // KPI thực tế từ SQL Server
            ViewBag.TotalStaff = staffList.Count;
            ViewBag.ActiveStaff = staffList.Count(x => x.IsActive);
            ViewBag.LockedStaff = staffList.Count(x => !x.IsActive);
            ViewBag.Roles = roles;
            ViewBag.SelectedRoleId = roleId;
            ViewBag.SearchString = searchString;

            // Nhật ký hệ thống thực tế (Audit Trail) từ bảng ActivityLogs
            var userDict = allUsers.ToDictionary(u => u.Id, u => u.FullName);
            var logs = await _context.ActivityLogs.AsNoTracking()
                .OrderByDescending(l => l.CreatedAt)
                .Take(20)
                .ToListAsync();
            ViewBag.ActivityLogs = logs;
            ViewBag.UserDict = userDict;

            if (roleId.HasValue && roleId.Value > 0)
            {
                staffList = staffList.Where(s => s.RoleId == roleId.Value).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var kw = searchString.Trim().ToLower();
                staffList = staffList.Where(s =>
                    s.FullName.ToLower().Contains(kw) ||
                    s.Username.ToLower().Contains(kw) ||
                    s.Email.ToLower().Contains(kw) ||
                    (s.PhoneNumber ?? "").Contains(kw)).ToList();
            }

            return View(staffList);
        }

        // =========================================================================
        // 2. GET: /Staff/Details/5
        // =========================================================================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vm = await BuildStaffViewModel(id.Value);
            if (vm == null) return NotFound();

            // Dữ liệu thống kê & hoạt động đồng bộ từ SQL Server cho trang Hồ sơ chi tiết
            var staffInvoices = await _context.Invoices.AsNoTracking()
                .Where(i => i.UserId == id.Value)
                .OrderByDescending(i => i.StartTime)
                .ToListAsync();

            var staffLogs = await _context.ActivityLogs.AsNoTracking()
                .Where(l => l.UserId == id.Value)
                .OrderByDescending(l => l.CreatedAt)
                .Take(8)
                .ToListAsync();

            var receiptsCount = await _context.GoodsReceipts.AsNoTracking()
                .CountAsync(g => g.UserId == id.Value);

            var roleEntity = await _context.Roles.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == vm.RoleId);

            var rolePermIds = await _context.RolePermissions.AsNoTracking()
                .Where(rp => rp.RoleId == vm.RoleId)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            var permissions = await _context.Permissions.AsNoTracking()
                .Where(p => rolePermIds.Contains(p.Id))
                .ToListAsync();

            ViewBag.InvoiceCount = staffInvoices.Count;
            ViewBag.TotalCollected = staffInvoices.Where(i => i.Status == "Đã thanh toán").Sum(i => i.TotalPayment);
            ViewBag.RecentInvoices = staffInvoices.Take(6).ToList();
            ViewBag.StaffLogs = staffLogs;
            ViewBag.ReceiptsCount = receiptsCount;
            ViewBag.RoleDescription = roleEntity?.Description ?? "Nhân sự vận hành CLB Bida";
            ViewBag.Permissions = permissions;

            ViewData["Title"] = $"Hồ sơ nhân viên: {vm.FullName}";
            return View(vm);
        }

        // =========================================================================
        // 3. GET: /Staff/Create
        // =========================================================================
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Thêm nhân viên mới";
            await PopulateRolesDropDown();
            return View(new StaffViewModel { IsActive = true });
        }

        // POST: /Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StaffViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", "Vui lòng nhập mật khẩu khởi tạo cho nhân viên mới (tối thiểu 6 ký tự).");
            }

            var normUser = (model.Username ?? "").Trim().ToLower();
            var normEmail = (model.Email ?? "").Trim().ToLower();

            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == normUser))
            {
                ModelState.AddModelError("Username", $"Tên đăng nhập '{model.Username}' đã tồn tại.");
            }
            if (!string.IsNullOrEmpty(normEmail) && await _context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower() == normEmail))
            {
                ModelState.AddModelError("Email", $"Email '{model.Email}' đã được sử dụng cho tài khoản khác.");
            }

            if (ModelState.IsValid)
            {
                string? avatarPath = null;
                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "staff");
                    Directory.CreateDirectory(uploadDir);
                    var ext = Path.GetExtension(model.AvatarFile.FileName).ToLowerInvariant();
                    var fileName = $"staff_{Guid.NewGuid():N}{ext}";
                    var fullPath = Path.Combine(uploadDir, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.AvatarFile.CopyToAsync(stream);
                    }
                    avatarPath = $"images/staff/{fileName}";
                }

                var newUser = new User
                {
                    Username = (model.Username ?? "").Trim(),
                    PasswordHash = HashPassword(model.Password!),
                    FullName = (model.FullName ?? "").Trim(),
                    Email = (model.Email ?? "").Trim(),
                    PhoneNumber = model.PhoneNumber?.Trim(),
                    Avatar = avatarPath,
                    IsActive = model.IsActive,
                    CreatedAt = DateTime.Now
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Gán vai trò vào bảng UserRoles
                _context.UserRoles.Add(new UserRole
                {
                    UserId = newUser.Id,
                    RoleId = model.RoleId,
                    CreatedAt = DateTime.Now
                });
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã tạo tài khoản nhân viên '{newUser.FullName}' (Username: {newUser.Username}) thành công! Có thể đăng nhập ngay.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateRolesDropDown(model.RoleId);
            return View(model);
        }

        // =========================================================================
        // 4. GET: /Staff/Edit/5
        // =========================================================================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vm = await BuildStaffViewModel(id.Value);
            if (vm == null) return NotFound();

            ViewData["Title"] = $"Chỉnh sửa nhân viên: {vm.FullName}";
            await PopulateRolesDropDown(vm.RoleId);
            return View(vm);
        }

        // POST: /Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StaffViewModel model)
        {
            if (id != model.Id) return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // Mật khẩu là tùy chọn khi Sửa (để trống nếu giữ nguyên mật khẩu cũ)
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.Remove("Password");
            }

            var normUser = (model.Username ?? "").Trim().ToLower();
            var normEmail = (model.Email ?? "").Trim().ToLower();

            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == normUser && u.Id != id))
            {
                ModelState.AddModelError("Username", $"Tên đăng nhập '{model.Username}' đã thuộc về tài khoản khác.");
            }
            if (!string.IsNullOrEmpty(normEmail) && await _context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower() == normEmail && u.Id != id))
            {
                ModelState.AddModelError("Email", $"Email '{model.Email}' đã được đăng ký cho nhân viên khác.");
            }

            if (ModelState.IsValid)
            {
                if (model.AvatarFile != null && model.AvatarFile.Length > 0)
                {
                    var uploadDir = Path.Combine(_env.WebRootPath, "images", "staff");
                    Directory.CreateDirectory(uploadDir);
                    var ext = Path.GetExtension(model.AvatarFile.FileName).ToLowerInvariant();
                    var fileName = $"staff_{Guid.NewGuid():N}{ext}";
                    var fullPath = Path.Combine(uploadDir, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await model.AvatarFile.CopyToAsync(stream);
                    }
                    user.Avatar = $"images/staff/{fileName}";
                }

                user.Username = (model.Username ?? "").Trim();
                user.FullName = (model.FullName ?? "").Trim();
                user.Email = (model.Email ?? "").Trim();
                user.PhoneNumber = model.PhoneNumber?.Trim();
                user.IsActive = model.IsActive;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    user.PasswordHash = HashPassword(model.Password);
                }

                // Cập nhật vai trò trong bảng UserRoles
                var existingRoles = await _context.UserRoles.Where(ur => ur.UserId == user.Id).ToListAsync();
                if (existingRoles.Any()) _context.UserRoles.RemoveRange(existingRoles);

                _context.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = model.RoleId,
                    CreatedAt = DateTime.Now
                });

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã cập nhật hồ sơ nhân viên '{user.FullName}' thành công.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateRolesDropDown(model.RoleId);
            return View(model);
        }

        // =========================================================================
        // 5. POST: /Staff/ToggleActive/5 (Khóa / Mở khóa tài khoản nhân viên)
        // =========================================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == id.ToString())
            {
                TempData["ErrorMessage"] = "Không thể tự khóa tài khoản Admin đang đăng nhập!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = user.IsActive
                ? $"Đã mở khóa tài khoản nhân viên '{user.FullName}'."
                : $"Đã khóa tài khoản nhân viên '{user.FullName}' (không thể đăng nhập).";

            return RedirectToAction(nameof(Index));
        }

        // =========================================================================
        // 6. GET: /Staff/Delete/5
        // =========================================================================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vm = await BuildStaffViewModel(id.Value);
            if (vm == null) return NotFound();

            ViewBag.IsCurrentUser = User.FindFirstValue(ClaimTypes.NameIdentifier) == id.Value.ToString();
            ViewData["Title"] = $"Xác nhận xóa nhân viên: {vm.FullName}";
            return View(vm);
        }

        // POST: /Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
            {
                TempData["ErrorMessage"] = "Không thể xóa chính tài khoản Admin đang đăng nhập!";
                return RedirectToAction(nameof(Index));
            }

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                bool hasBusinessRecords =
                    await _context.Invoices.AnyAsync(i => i.UserId == id) ||
                    await _context.GoodsReceipts.AnyAsync(g => g.UserId == id) ||
                    await _context.PaymentVouchers.AnyAsync(p => p.UserId == id);

                if (hasBusinessRecords)
                {
                    // Bảo toàn lịch sử tài chính: tự động chuyển sang Khóa tài khoản nếu nhân viên đã lập hóa đơn/phiếu thu chi
                    user.IsActive = false;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Nhân viên '{user.FullName}' đã phát sinh chứng từ hóa đơn nên hệ thống đã chuyển sang trạng thái KHÓA TÀI KHOẢN để bảo toàn dữ liệu kế toán.";
                    return RedirectToAction(nameof(Index));
                }

                // Dọn sạch các bảng phụ thuộc trước khi xóa cứng User
                var uRoles = await _context.UserRoles.Where(x => x.UserId == id).ToListAsync();
                if (uRoles.Any()) _context.UserRoles.RemoveRange(uRoles);

                var logs = await _context.ActivityLogs.Where(x => x.UserId == id).ToListAsync();
                if (logs.Any()) _context.ActivityLogs.RemoveRange(logs);

                var schedules = await _context.WorkSchedules.Where(x => x.UserId == id).ToListAsync();
                if (schedules.Any()) _context.WorkSchedules.RemoveRange(schedules);

                var tks = await _context.Timekeepings.Where(x => x.UserId == id).ToListAsync();
                if (tks.Any()) _context.Timekeepings.RemoveRange(tks);

                var payrolls = await _context.Payrolls.Where(x => x.UserId == id).ToListAsync();
                if (payrolls.Any()) _context.Payrolls.RemoveRange(payrolls);

                var audits = await _context.InventoryAudits.Where(x => x.UserId == id).ToListAsync();
                if (audits.Any()) _context.InventoryAudits.RemoveRange(audits);

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = $"Đã xóa thành công nhân viên '{user.FullName}' ({user.Username}) khỏi SQL Server.";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<StaffViewModel?> BuildStaffViewModel(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            var userRole = await _context.UserRoles.AsNoTracking().FirstOrDefaultAsync(ur => ur.UserId == id);
            var role = userRole != null ? await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Id == userRole.RoleId) : null;

            return new StaffViewModel
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email ?? "",
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                RoleId = role?.Id ?? 0,
                RoleName = role?.Name ?? "Chưa gán"
            };
        }

        private async Task PopulateRolesDropDown(int? selectedId = null)
        {
            var roles = await _context.Roles.AsNoTracking().OrderBy(r => r.Id).ToListAsync();
            ViewBag.RoleList = new SelectList(roles, "Id", "Name", selectedId);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}

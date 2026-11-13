using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanBida.Controllers
{
    /// <summary>
    /// Controller Quản lý Xác thực Đăng nhập & Đăng xuất (Chuẩn MVC + Entity Framework Core)
    /// </summary>
    public class LoginController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;

        public LoginController(QuanLyQuanBidaContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. GET: /Login/Login
        // ==========================================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Categories");
            }

            ViewData["Title"] = "Đăng nhập hệ thống";
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl ?? Url.Content("~/Categories")
            };

            return View(model);
        }

        // ==========================================
        // 2. POST: /Login/Login
        // ==========================================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            model.ReturnUrl = returnUrl ?? model.ReturnUrl ?? Url.Content("~/Categories");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var input = (model.Email ?? string.Empty).Trim().ToLower();
            var hashedPassword = HashPassword(model.Password ?? string.Empty);

            // Truy vấn trực tiếp từ DbSet<User> qua Entity Framework Core
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.IsActive &&
                (u.Username.ToLower() == input || (u.Email != null && u.Email.ToLower() == input)));

            if (user != null)
            {
                // Kiểm tra mật khẩu (băm SHA256 chuẩn bảo mật)
                bool isPasswordValid = string.Equals(user.PasswordHash, hashedPassword, StringComparison.OrdinalIgnoreCase)
                                    || string.Equals(user.PasswordHash, model.Password, StringComparison.Ordinal);

                if (isPasswordValid)
                {
                    // Lấy vai trò của người dùng từ Database qua EF Core
                    var role = await (from ur in _context.UserRoles
                                      join r in _context.Roles on ur.RoleId equals r.Id
                                      where ur.UserId == user.Id
                                      select r.Name).FirstOrDefaultAsync();

                    // Chuẩn hóa Policy: Admin/Manager -> "Manager", Cashier/Waiter/Khác -> "Employee"
                    var roleName = (!string.IsNullOrEmpty(role) &&
                                   (role.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                                    role.Equals("Manager", StringComparison.OrdinalIgnoreCase)))
                                   ? "Manager"
                                   : "Employee";

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName),
                        new Claim(ClaimTypes.Email, user.Email ?? $"{user.Username}@boltbilliards.vn"),
                        new Claim(ClaimTypes.Role, roleName),
                        new Claim("Username", user.Username)
                    };

                    // Nếu vai trò là Admin, thêm claim Admin để hỗ trợ IsInRole("Admin")
                    if (!string.IsNullOrEmpty(role) && !claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals(role, StringComparison.OrdinalIgnoreCase)))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Categories");
                }
            }

            ModelState.AddModelError(string.Empty, "Tài khoản hoặc mật khẩu không chính xác.");
            return View(model);
        }

        // ==========================================
        // 3. POST: /Login/Logout
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // ==========================================
        // TIỆN ÍCH BĂM MẬT KHẨU SHA-256
        // ==========================================
        private static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLowerInvariant();
        }
    }
}

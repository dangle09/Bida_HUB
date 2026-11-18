using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyQuanBida.Data;
using QuanLyQuanBida.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// ===================================================================================
// CONTROLLER CỔNG ĐĂNG NHẬP NỘI BỘ & QUÊN MẬT KHẨU (LAB 4 + LAB 5 BÀI 2)
// - Route chính: /Login, /Login/Login, /Login/ForgotPassword, /Login/Logout
// - Kiến trúc: 100% ASP.NET Core MVC + Entity Framework Core (KHÔNG dùng DAL)
// ===================================================================================
namespace QuanLyQuanBida.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuanLyQuanBidaContext _context;
        private readonly IConfiguration _configuration;

        public LoginController(QuanLyQuanBidaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ==========================================
        // 1. GET: /Login hoặc /Login/Login
        // ==========================================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index(string? returnUrl = null)
        {
            return Login(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Waiter"))
                    return RedirectToAction("Index", "Pos");
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["Title"] = "Đăng nhập hệ thống";
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View("Login", model);
        }

        // ==========================================
        // 2. POST: /Login/Login
        // ==========================================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            model.ReturnUrl = returnUrl ?? model.ReturnUrl ?? Url.Content("~/Dashboard");

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

                    // Thiết lập danh sách Claim Roles chuẩn:
                    // Admin: nhận cả Role "Admin" và "Manager"
                    // Manager: nhận Role "Manager"
                    // Cashier: nhận Role "Cashier" và "Employee"
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, string.IsNullOrWhiteSpace(user.FullName) ? user.Username : user.FullName),
                        new Claim(ClaimTypes.Email, user.Email ?? $"{user.Username}@boltbilliards.vn"),
                        new Claim("Username", user.Username)
                    };

                    if (!string.IsNullOrEmpty(role))
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                        if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Manager"));
                        }
                        else if (role.Equals("Cashier", StringComparison.OrdinalIgnoreCase))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, "Employee"));
                        }
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    bool isWaiter = string.Equals(role, "Waiter", StringComparison.OrdinalIgnoreCase);
                    if (isWaiter)
                    {
                        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                            && !model.ReturnUrl.StartsWith("/Dashboard", StringComparison.OrdinalIgnoreCase))
                        {
                            return LocalRedirect(model.ReturnUrl);
                        }
                        return RedirectToAction("Index", "Pos");
                    }

                    if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return LocalRedirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Dashboard");
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
        // 4. LAB 5 (BÀI 2): QUÊN MẬT KHẨU & GỬI MẬT KHẨU NGẪU NHIÊN QUA EMAIL
        //    GET/POST: /Login/ForgotPassword
        // ==========================================
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            ViewData["Title"] = "Quên mật khẩu - Cấp lại mật khẩu qua Email";
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            ViewData["Title"] = "Quên mật khẩu - Cấp lại mật khẩu qua Email";
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var inputEmail = model.Email.Trim().ToLower();
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.IsActive && u.Email != null && u.Email.ToLower() == inputEmail);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Không tìm thấy tài khoản nhân viên đang hoạt động nào khớp với Email này.");
                return View(model);
            }

            // 1. Sinh mật khẩu ngẫu nhiên an toàn (8 ký tự chữ hoa + số)
            var newPassword = GenerateRandomPassword(8);

            // 2. Băm SHA-256 và cập nhật vào SQL Server qua EF Core
            user.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            // 3. Gửi mật khẩu mới qua SMTP Email (cấu hình trong appsettings.json)
            var smtpServer = _configuration["EmailSettings:SmtpServer"] ?? "smtp.gmail.com";
            var smtpPort = int.TryParse(_configuration["EmailSettings:SmtpPort"], out var p) ? p : 587;
            var senderName = _configuration["EmailSettings:SenderName"] ?? "BOLT BILLIARDS POS";
            var senderEmail = _configuration["EmailSettings:SenderEmail"] ?? "";
            var senderPassword = _configuration["EmailSettings:SenderPassword"] ?? "";

            bool isSmtpConfigured = !string.IsNullOrWhiteSpace(senderEmail)
                && !senderEmail.Contains("YOUR_GMAIL", StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(senderPassword)
                && !senderPassword.Contains("YOUR_GMAIL_APP_PASSWORD", StringComparison.OrdinalIgnoreCase);

            if (isSmtpConfigured)
            {
                try
                {
                    using var message = new MailMessage
                    {
                        From = new MailAddress(senderEmail, senderName),
                        Subject = "[BOLT BILLIARDS] Cấp lại mật khẩu đăng nhập hệ thống POS",
                        IsBodyHtml = true,
                        Body = $@"
                            <div style='font-family: Arial, sans-serif; padding: 20px; background: #0d121c; color: #f8fafc;'>
                                <h2 style='color: #a6ff00;'>BOLT BILLIARDS — CẤP LẠI MẬT KHẨU</h2>
                                <p>Xin chào <strong>{WebUtility.HtmlEncode(user.FullName)}</strong>,</p>
                                <p>Hệ thống vừa tiếp nhận yêu cầu cấp lại mật khẩu cho tài khoản <strong>{WebUtility.HtmlEncode(user.Username)}</strong>.</p>
                                <p>Mật khẩu đăng nhập mới của bạn là:</p>
                                <div style='padding: 12px 18px; background: #07090e; border: 1px solid #a6ff00; color: #a6ff00; font-size: 20px; font-weight: bold; letter-spacing: 2px; display: inline-block;'>
                                    {newPassword}
                                </div>
                                <p style='margin-top: 16px; color: #94a3b8; font-size: 13px;'>Vui lòng đăng nhập và đổi lại mật khẩu cá nhân ngay sau khi vào hệ thống.</p>
                            </div>"
                    };
                    message.To.Add(user.Email!);

                    using var client = new SmtpClient(smtpServer, smtpPort)
                    {
                        Credentials = new NetworkCredential(senderEmail, senderPassword),
                        EnableSsl = true
                    };
                    await client.SendMailAsync(message);

                    ViewBag.SuccessMessage = $"Đã đặt lại mật khẩu mới trong CSDL và gửi thành công tới email {user.Email}. Vui lòng kiểm tra hộp thư đến (hoặc Spam) để đăng nhập!";
                }
                catch (Exception ex)
                {
                    ViewBag.SuccessMessage = $"Đã cập nhật mật khẩu mới vào CSDL SQL Server cho tài khoản '{user.Username}'. (Gửi SMTP chưa thành công: {ex.Message})";
                    ViewBag.DemoPassword = newPassword;
                    ViewBag.DemoUsername = user.Username;
                }
            }
            else
            {
                // Chế độ thử nghiệm nội bộ khi chưa điền Gmail App Password vào appsettings.json
                ViewBag.SuccessMessage = $"Đã cập nhật mật khẩu ngẫu nhiên mới vào CSDL SQL Server cho tài khoản '{user.Username}' ({user.Email}).";
                ViewBag.DemoNote = "Chưa cấu hình Gmail App Password trong appsettings.json (EmailSettings) nên hệ thống hiển thị mật khẩu vừa cấp để bạn test đăng nhập ngay:";
                ViewBag.DemoPassword = newPassword;
                ViewBag.DemoUsername = user.Username;
            }

            return View(new ForgotPasswordViewModel());
        }

        // ==========================================
        // 5. POST: /Login/UpdateProfile (Cập nhật thông tin cá nhân thật vào SQL Server)
        // ==========================================
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(string fullName, string email, string? phoneNumber, string? returnUrl = null)
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out int userId)) return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return RedirectToAction("Login");

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email))
            {
                TempData["ErrorMessage"] = "Họ tên và Email không được để trống.";
                return Redirect(returnUrl ?? "/Dashboard");
            }

            user.FullName = fullName.Trim();
            user.Email = email.Trim();
            user.PhoneNumber = phoneNumber?.Trim();
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã cập nhật thông tin cá nhân thành công!";
            return Redirect(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) ? returnUrl : "/Dashboard");
        }

        // ==========================================
        // 6. POST: /Login/ChangePassword (Đổi mật khẩu thật với xác thực SHA-256)
        // ==========================================
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword, string? returnUrl = null)
        {
            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out int userId)) return RedirectToAction("Login");

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return RedirectToAction("Login");

            string backUrl = !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) ? returnUrl : "/Dashboard";

            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword))
            {
                TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ mật khẩu hiện tại và mật khẩu mới.";
                return Redirect(backUrl);
            }

            var currentHash = HashPassword(currentPassword);
            bool currentValid = string.Equals(user.PasswordHash, currentHash, StringComparison.OrdinalIgnoreCase)
                             || string.Equals(user.PasswordHash, currentPassword, StringComparison.Ordinal);

            if (!currentValid)
            {
                TempData["ErrorMessage"] = "Mật khẩu hiện tại không chính xác! Nếu bạn quên, hãy bấm 'Quên mật khẩu?' để nhận mật khẩu mới qua Email.";
                return Redirect(backUrl);
            }

            if (newPassword.Length < 6)
            {
                TempData["ErrorMessage"] = "Mật khẩu mới phải có ít nhất 6 ký tự.";
                return Redirect(backUrl);
            }

            if (newPassword != confirmPassword)
            {
                TempData["ErrorMessage"] = "Mật khẩu xác nhận không khớp với mật khẩu mới.";
                return Redirect(backUrl);
            }

            user.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đã thay đổi mật khẩu thành công! Lần đăng nhập tới hãy dùng mật khẩu mới.";
            return Redirect(backUrl);
        }

        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var result = new char[length];
            var randomBytes = RandomNumberGenerator.GetBytes(length);
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[randomBytes[i] % chars.Length];
            }
            return new string(result);
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

using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanBida.ViewModels
{
    /// <summary>
    /// ViewModel phục vụ chức năng đăng nhập tài khoản hệ thống (Lab 4 - Bài 2)
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email hoặc Tên tài khoản.")]
        [Display(Name = "Email hoặc Tên đăng nhập")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Ghi nhớ phiên đăng nhập (24 giờ)")]
        public bool RememberMe { get; set; } = false;

        public string? ReturnUrl { get; set; }
    }
}

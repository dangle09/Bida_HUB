using System.ComponentModel.DataAnnotations;

namespace QuanLyQuanBida.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Email tài khoản nhân viên.")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng định dạng.")]
        [Display(Name = "Email nhân viên")]
        public string Email { get; set; } = string.Empty;
    }
}

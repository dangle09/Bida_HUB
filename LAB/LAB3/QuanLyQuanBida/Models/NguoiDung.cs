using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanBida.Models
{
    [Table("nguoi_dung")]
    public class NguoiDung
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ten_dang_nhap")]
        public string TenDangNhap { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("mat_khau_hash")]
        public string MatKhauHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("ho_ten")]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(100)]
        [EmailAddress]
        [Column("email")]
        public string? Email { get; set; }

        [StringLength(15)]
        [Phone]
        [Column("so_dien_thoai")]
        public string? SoDienThoai { get; set; }

        [StringLength(255)]
        [Column("anh_dai_dien")]
        public string? AnhDaiDien { get; set; }

        [Required]
        [Column("dang_hoat_dong")]
        public bool DangHoatDong { get; set; }

        [Required]
        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public NguoiDung() { }
    }
}

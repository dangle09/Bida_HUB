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

        [Column("ten_dang_nhap")]
        public string TenDangNhap { get; set; } = string.Empty;

        [Column("mat_khau_hash")]
        public string MatKhauHash { get; set; } = string.Empty;

        [Column("ho_ten")]
        public string HoTen { get; set; } = string.Empty;

        [Column("email")]
        public string? Email { get; set; }

        [Column("so_dien_thoai")]
        public string? SoDienThoai { get; set; }

        [Column("anh_dai_dien")]
        public string? AnhDaiDien { get; set; }

        [Column("dang_hoat_dong")]
        public bool DangHoatDong { get; set; }

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public NguoiDung() { }
    }
}

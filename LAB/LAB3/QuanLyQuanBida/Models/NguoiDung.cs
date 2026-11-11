using System;

namespace QuanLyQuanBida.Models
{
    public class NguoiDung
    {
        public int Id { get; set; }
        public string TenDangNhap { get; set; } = string.Empty;
        public string MatKhauHash { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? SoDienThoai { get; set; }
        public string? AnhDaiDien { get; set; }
        public bool DangHoatDong { get; set; }
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public NguoiDung() { }
    }
}

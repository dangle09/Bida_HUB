using System;

namespace QuanLyQuanBida.Models
{
    public class PhuongThucThanhToan
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public PhuongThucThanhToan() { }
    }
}

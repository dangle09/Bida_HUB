using System;

namespace QuanLyQuanBida.Models
{
    public class NhaCungCap
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string? DiaChi { get; set; }

        // HÀM KHỞI TẠO
        public NhaCungCap() { }
    }
}

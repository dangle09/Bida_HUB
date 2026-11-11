using System;

namespace QuanLyQuanBida.Models
{
    public class TuDo
    {
        public int Id { get; set; }
        public string SoTu { get; set; } = string.Empty;
        public string? KhuVuc { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public decimal TienThueThang { get; set; }

        // HÀM KHỞI TẠO
        public TuDo() { }
    }
}

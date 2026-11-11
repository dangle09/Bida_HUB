using System;

namespace QuanLyQuanBida.Models
{
    public class ThietBiBan
    {
        public int Id { get; set; }
        public int BanBidaId { get; set; }
        public string Ten { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThietBiBan() { }
    }
}

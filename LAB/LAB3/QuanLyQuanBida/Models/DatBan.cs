using System;

namespace QuanLyQuanBida.Models
{
    public class DatBan
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public int BanBidaId { get; set; }
        public DateTime ThoiGianDen { get; set; }
        public int SoNguoi { get; set; }
        public decimal TienCoc { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public DatBan() { }
    }
}

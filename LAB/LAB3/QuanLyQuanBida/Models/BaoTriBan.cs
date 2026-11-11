using System;

namespace QuanLyQuanBida.Models
{
    public class BaoTriBan
    {
        public int Id { get; set; }
        public int BanBidaId { get; set; }
        public string LoaiBaoTri { get; set; } = string.Empty;
        public decimal ChiPhi { get; set; }
        public DateTime NgayBaoTri { get; set; }
        public string? NguoiThucHien { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public BaoTriBan() { }
    }
}

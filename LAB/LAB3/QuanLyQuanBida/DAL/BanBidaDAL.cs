using System;

namespace QuanLyQuanBida.DAL
{
    public class BanBidaDAL
    {
        public int Id { get; set; }
        public int KhuVucBanId { get; set; }
        public int LoaiBanId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public BanBidaDAL() { }
    }
}

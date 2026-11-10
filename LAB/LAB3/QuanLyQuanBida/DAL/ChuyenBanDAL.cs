using System;

namespace QuanLyQuanBida.DAL
{
    public class ChuyenBanDAL
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public int TuBanId { get; set; }
        public int DenBanId { get; set; }
        public DateTime GioChuyenBan { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public ChuyenBanDAL() { }
    }
}

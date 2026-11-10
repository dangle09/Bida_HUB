using System;

namespace QuanLyQuanBida.DAL
{
    public class ThietBiBanDAL
    {
        public int Id { get; set; }
        public int BanBidaId { get; set; }
        public string Ten { get; set; } = string.Empty;
        public int SoLuong { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThietBiBanDAL() { }
    }
}

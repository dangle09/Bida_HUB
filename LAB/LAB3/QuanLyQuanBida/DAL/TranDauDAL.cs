using System;

namespace QuanLyQuanBida.DAL
{
    public class TranDauDAL
    {
        public int Id { get; set; }
        public int GiaiDauId { get; set; }
        public int BanBidaId { get; set; }
        public string TenVongDau { get; set; } = string.Empty;
        public int CoThu1Id { get; set; }
        public int CoThu2Id { get; set; }
        public int? TiSoCoThu1 { get; set; }
        public int? TiSoCoThu2 { get; set; }
        public int? NguoiThangId { get; set; }
        public DateTime ThoiGianDau { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public TranDauDAL() { }
    }
}

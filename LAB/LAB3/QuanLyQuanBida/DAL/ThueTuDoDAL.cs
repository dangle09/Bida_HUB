using System;

namespace QuanLyQuanBida.DAL
{
    public class ThueTuDoDAL
    {
        public int Id { get; set; }
        public int TuDoId { get; set; }
        public int KhachHangId { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayHetHan { get; set; }
        public decimal TienThueThang { get; set; }
        public decimal TienCoc { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public ThueTuDoDAL() { }
    }
}

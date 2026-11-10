using System;

namespace QuanLyQuanBida.DAL
{
    public class TuDoDAL
    {
        public int Id { get; set; }
        public string SoTu { get; set; } = string.Empty;
        public string? KhuVuc { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public decimal TienThueThang { get; set; }

        // HÀM KHỞI TẠO
        public TuDoDAL() { }
    }
}

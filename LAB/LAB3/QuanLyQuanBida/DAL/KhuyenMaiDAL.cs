using System;

namespace QuanLyQuanBida.DAL
{
    public class KhuyenMaiDAL
    {
        public int Id { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public decimal? PhanTramGiam { get; set; }
        public decimal? SoTienGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayHetHan { get; set; }

        // HÀM KHỞI TẠO
        public KhuyenMaiDAL() { }
    }
}

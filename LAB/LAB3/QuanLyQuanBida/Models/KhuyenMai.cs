using System;

namespace QuanLyQuanBida.Models
{
    public class KhuyenMai
    {
        public int Id { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public decimal? PhanTramGiam { get; set; }
        public decimal? SoTienGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayHetHan { get; set; }

        // HÀM KHỞI TẠO
        public KhuyenMai() { }
    }
}

using System;

namespace QuanLyQuanBida.Models
{
    public class QuyenHan
    {
        public int Id { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string PhanHe { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public QuyenHan() { }
    }
}

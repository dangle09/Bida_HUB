using System;

namespace QuanLyQuanBida.DAL
{
    public class QuyenHanDAL
    {
        public int Id { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string PhanHe { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public QuyenHanDAL() { }
    }
}

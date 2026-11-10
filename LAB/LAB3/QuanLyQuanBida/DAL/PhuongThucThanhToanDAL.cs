using System;

namespace QuanLyQuanBida.DAL
{
    public class PhuongThucThanhToanDAL
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public PhuongThucThanhToanDAL() { }
    }
}

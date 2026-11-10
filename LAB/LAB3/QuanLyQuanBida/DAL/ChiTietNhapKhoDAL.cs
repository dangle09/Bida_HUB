using System;

namespace QuanLyQuanBida.DAL
{
    public class ChiTietNhapKhoDAL
    {
        public int Id { get; set; }
        public int PhieuNhapId { get; set; }
        public int MonDichVuId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietNhapKhoDAL() { }
    }
}

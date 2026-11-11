using System;

namespace QuanLyQuanBida.Models
{
    public class ChiTietNhapKho
    {
        public int Id { get; set; }
        public int PhieuNhapId { get; set; }
        public int MonDichVuId { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaNhap { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietNhapKho() { }
    }
}

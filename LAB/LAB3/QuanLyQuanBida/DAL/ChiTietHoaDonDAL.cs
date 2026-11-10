using System;

namespace QuanLyQuanBida.DAL
{
    public class ChiTietHoaDonDAL
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public int MonDichVuId { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }

        // HÀM KHỞI TẠO
        public ChiTietHoaDonDAL() { }
    }
}

using System;

namespace QuanLyQuanBida.DAL
{
    public class ThanhToanHoaDonDAL
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public int PhuongThucId { get; set; }
        public decimal SoTien { get; set; }
        public DateTime ThoiGianTt { get; set; }

        // HÀM KHỞI TẠO
        public ThanhToanHoaDonDAL() { }
    }
}

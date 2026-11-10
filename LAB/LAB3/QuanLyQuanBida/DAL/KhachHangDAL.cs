using System;

namespace QuanLyQuanBida.DAL
{
    public class KhachHangDAL
    {
        public int Id { get; set; }
        public int HangThanhVienId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public int DiemTichLuy { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime NgayTao { get; set; }

        // HÀM KHỞI TẠO
        public KhachHangDAL() { }
    }
}

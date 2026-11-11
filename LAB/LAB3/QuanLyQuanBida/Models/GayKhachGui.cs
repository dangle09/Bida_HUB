using System;

namespace QuanLyQuanBida.Models
{
    public class GayKhachGui
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string? ThuongHieuGay { get; set; }
        public string? LoaiNgonCo { get; set; }
        public DateTime NgayGui { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GayKhachGui() { }
    }
}

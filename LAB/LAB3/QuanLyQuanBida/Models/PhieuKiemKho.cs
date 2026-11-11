using System;

namespace QuanLyQuanBida.Models
{
    public class PhieuKiemKho
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public DateTime NgayKiemKe { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuKiemKho() { }
    }
}

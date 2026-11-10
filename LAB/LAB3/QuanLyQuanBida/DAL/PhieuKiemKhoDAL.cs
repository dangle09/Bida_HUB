using System;

namespace QuanLyQuanBida.DAL
{
    public class PhieuKiemKhoDAL
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public DateTime NgayKiemKe { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuKiemKhoDAL() { }
    }
}

using System;

namespace QuanLyQuanBida.DAL
{
    public class PhieuChiDAL
    {
        public int Id { get; set; }
        public int LoaiChiPhiId { get; set; }
        public int NguoiDungId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public decimal SoTien { get; set; }
        public DateTime NgayTao { get; set; }
        public string MucDichChi { get; set; } = string.Empty;
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public PhieuChiDAL() { }
    }
}

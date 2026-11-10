using System;

namespace QuanLyQuanBida.DAL
{
    public class GiaoDichDiemDAL
    {
        public int Id { get; set; }
        public int KhachHangId { get; set; }
        public string LoaiGiaoDich { get; set; } = string.Empty;
        public int SoDiem { get; set; }
        public DateTime NgayTao { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public GiaoDichDiemDAL() { }
    }
}

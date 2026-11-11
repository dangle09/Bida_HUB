using System;

namespace QuanLyQuanBida.Models
{
    public class NhatKyHoatDong
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public string HanhDong { get; set; } = string.Empty;
        public string? BangDuLieu { get; set; }
        public int? BanGhiId { get; set; }
        public string? MoTa { get; set; }
        public string? DiaChiIp { get; set; }
        public DateTime ThoiGian { get; set; }

        // HÀM KHỞI TẠO
        public NhatKyHoatDong() { }
    }
}

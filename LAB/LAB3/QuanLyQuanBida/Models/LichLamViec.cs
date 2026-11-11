using System;

namespace QuanLyQuanBida.Models
{
    public class LichLamViec
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int CaLamId { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string? GhiChu { get; set; }

        // HÀM KHỞI TẠO
        public LichLamViec() { }
    }
}

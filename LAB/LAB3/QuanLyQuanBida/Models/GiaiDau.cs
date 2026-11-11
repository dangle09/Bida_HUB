using System;

namespace QuanLyQuanBida.Models
{
    public class GiaiDau
    {
        public int Id { get; set; }
        public string TenGiai { get; set; } = string.Empty;
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal LePhiThamGia { get; set; }
        public decimal TongGiaiThuong { get; set; }
        public string TrangThai { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public GiaiDau() { }
    }
}

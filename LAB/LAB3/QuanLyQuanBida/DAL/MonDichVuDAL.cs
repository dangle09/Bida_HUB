using System;

namespace QuanLyQuanBida.DAL
{
    public class MonDichVuDAL
    {
        public int Id { get; set; }
        public int DanhMucId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public string Ten { get; set; } = string.Empty;
        public string DonViTinh { get; set; } = string.Empty;
        public decimal DonGia { get; set; }
        public string? HinhAnh { get; set; }
        public bool DangHoatDong { get; set; }

        // HÀM KHỞI TẠO
        public MonDichVuDAL() { }
    }
}

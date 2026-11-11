using System;

namespace QuanLyQuanBida.Models
{
    public class PhieuNhapKho
    {
        public int Id { get; set; }
        public int NhaCungCapId { get; set; }
        public int NguoiDungId { get; set; }
        public string MaCode { get; set; } = string.Empty;
        public DateTime NgayNhap { get; set; }
        public decimal TongTienNhap { get; set; }

        // HÀM KHỞI TẠO
        public PhieuNhapKho() { }
    }
}

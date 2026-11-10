using System;

namespace QuanLyQuanBida.DAL
{
    public class HangThanhVienDAL
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public int DiemToiThieu { get; set; }
        public decimal TiLeGiamGia { get; set; }
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public HangThanhVienDAL() { }
    }
}

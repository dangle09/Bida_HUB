using System;

namespace QuanLyQuanBida.Models
{
    public class VaiTro
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public VaiTro() { }
    }
}

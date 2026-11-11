using System;

namespace QuanLyQuanBida.Models
{
    public class LoaiChiPhi
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public string? MoTa { get; set; }

        // HÀM KHỞI TẠO
        public LoaiChiPhi() { }
    }
}

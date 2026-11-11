using System;

namespace QuanLyQuanBida.Models
{
    public class LoaiBan
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public decimal GiaCoBan { get; set; }

        // HÀM KHỞI TẠO
        public LoaiBan() { }
    }
}

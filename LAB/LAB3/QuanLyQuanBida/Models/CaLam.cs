using System;

namespace QuanLyQuanBida.Models
{
    public class CaLam
    {
        public int Id { get; set; }
        public string Ten { get; set; } = string.Empty;
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public decimal LuongTheoGio { get; set; }

        // HÀM KHỞI TẠO
        public CaLam() { }
    }
}

using System;

namespace QuanLyQuanBida.Models
{
    public class GiaBanKhungGio
    {
        public int Id { get; set; }
        public int LoaiBanId { get; set; }
        public TimeSpan GioBatDauKg { get; set; }
        public TimeSpan GioKetThucKg { get; set; }
        public decimal GiaKhungGio { get; set; }

        // HÀM KHỞI TẠO
        public GiaBanKhungGio() { }
    }
}

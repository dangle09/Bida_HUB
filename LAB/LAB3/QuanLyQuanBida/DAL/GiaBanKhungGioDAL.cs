using System;

namespace QuanLyQuanBida.DAL
{
    public class GiaBanKhungGioDAL
    {
        public int Id { get; set; }
        public int LoaiBanId { get; set; }
        public TimeSpan GioBatDauKg { get; set; }
        public TimeSpan GioKetThucKg { get; set; }
        public decimal GiaKhungGio { get; set; }

        // HÀM KHỞI TẠO
        public GiaBanKhungGioDAL() { }
    }
}

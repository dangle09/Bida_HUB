using System;

namespace QuanLyQuanBida.Models
{
    public class Locker
    {
        public int Id { get; set; }
        public string LockerNumber { get; set; } = string.Empty;
        public string? Area { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal MonthlyRent { get; set; }

        // HÀM KHỞI TẠO
        public Locker() { }
    }
}

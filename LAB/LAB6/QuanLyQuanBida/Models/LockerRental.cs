using System;

namespace QuanLyQuanBida.Models
{
    public class LockerRental
    {
        public int Id { get; set; }
        public int LockerId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal MonthlyRent { get; set; }
        public decimal Deposit { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public LockerRental() { }
    }
}

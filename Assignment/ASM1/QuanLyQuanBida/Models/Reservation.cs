using System;

namespace QuanLyQuanBida.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int BilliardTableId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int GuestCount { get; set; }
        public decimal Deposit { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public Reservation() { }
    }
}

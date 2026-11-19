using System;

namespace QuanLyQuanBida.Models
{
    public class Timekeeping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime WorkDate { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public decimal? WorkHours { get; set; }
        public int? LateMinutes { get; set; }
        public int? EarlyLeaveMinutes { get; set; }

        // HÀM KHỞI TẠO
        public Timekeeping() { }
    }
}

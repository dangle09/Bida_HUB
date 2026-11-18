using System;

namespace QuanLyQuanBida.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ShiftId { get; set; }
        public DateTime WorkDate { get; set; }
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public WorkSchedule() { }
    }
}

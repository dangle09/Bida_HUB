using System;

namespace QuanLyQuanBida.Models
{
    public class TimeSlotPrice
    {
        public int Id { get; set; }
        public int TableTypeId { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
        public decimal SlotPrice { get; set; }

        // HÀM KHỞI TẠO
        public TimeSlotPrice() { }
    }
}

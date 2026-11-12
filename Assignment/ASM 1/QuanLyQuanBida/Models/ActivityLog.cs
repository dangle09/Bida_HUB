using System;

namespace QuanLyQuanBida.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string? TableName { get; set; }
        public int? RecordId { get; set; }
        public string? Description { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public ActivityLog() { }
    }
}

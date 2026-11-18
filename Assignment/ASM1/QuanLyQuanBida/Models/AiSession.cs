using System;

namespace QuanLyQuanBida.Models
{
    public class AiSession
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CustomerId { get; set; }
        public string SessionKey { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime StartedAt { get; set; }
        public DateTime LastActiveAt { get; set; }
        public bool IsActive { get; set; }

        // HÀM KHỞI TẠO
        public AiSession() { }
    }
}

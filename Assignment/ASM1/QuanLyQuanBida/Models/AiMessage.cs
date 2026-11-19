using System;

namespace QuanLyQuanBida.Models
{
    public class AiMessage
    {
        public int Id { get; set; }
        public int AiSessionId { get; set; }
        public string Role { get; set; } = string.Empty; // "user", "assistant", "system"
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public AiMessage() { }
    }
}

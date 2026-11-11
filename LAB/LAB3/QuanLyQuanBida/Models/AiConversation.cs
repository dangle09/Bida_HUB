using System;

namespace QuanLyQuanBida.Models
{
    public class AiConversation
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public string UserPrompt { get; set; } = string.Empty;
        public string AiResponse { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public AiConversation() { }
    }
}

using System;

namespace QuanLyQuanBida.Models
{
    public class AiState
    {
        public int Id { get; set; }
        public int? AiSessionId { get; set; }
        public string StateKey { get; set; } = string.Empty;
        public string StateValue { get; set; } = string.Empty; // JSON/Text lưu ngữ cảnh gợi ý
        public DateTime UpdatedAt { get; set; }

        // HÀM KHỞI TẠO
        public AiState() { }
    }
}

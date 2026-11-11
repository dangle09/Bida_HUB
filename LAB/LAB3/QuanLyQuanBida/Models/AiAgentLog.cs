using System;

namespace QuanLyQuanBida.Models
{
    public class AiAgentLog
    {
        public int Id { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public int ExecutionTimeMs { get; set; }
        public string? LogDetails { get; set; }
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public AiAgentLog() { }
    }
}

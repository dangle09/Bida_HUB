using System;

namespace QuanLyQuanBida.Models
{
    public class AiPrediction
    {
        public int Id { get; set; }
        public string PredictionType { get; set; } = string.Empty;
        public string TargetEntity { get; set; } = string.Empty;
        public int? TargetId { get; set; }
        public decimal ConfidenceScore { get; set; }
        public string PredictionDetails { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public AiPrediction() { }
    }
}

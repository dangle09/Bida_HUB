using System;

namespace QuanLyQuanBida.Models
{
    public class PointTransaction
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public PointTransaction() { }
    }
}

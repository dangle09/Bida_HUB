using System;

namespace QuanLyQuanBida.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int BilliardTableId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int? PromotionId { get; set; }
        public string InvoiceCode { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TableAmount { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public Invoice() { }
    }
}

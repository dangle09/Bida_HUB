using System;

namespace QuanLyQuanBida.Models
{
    public class PaymentVoucher
    {
        public int Id { get; set; }
        public int ExpenseTypeId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ExpensePurpose { get; set; } = string.Empty;
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public PaymentVoucher() { }
    }
}

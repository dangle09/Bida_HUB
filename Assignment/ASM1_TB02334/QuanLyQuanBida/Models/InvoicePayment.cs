using System;

namespace QuanLyQuanBida.Models
{
    public class InvoicePayment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentTime { get; set; }

        // HÀM KHỞI TẠO
        public InvoicePayment() { }
    }
}

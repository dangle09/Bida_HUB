using System;

namespace QuanLyQuanBida.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ServiceItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }

        // HÀM KHỞI TẠO
        public InvoiceDetail() { }
    }
}

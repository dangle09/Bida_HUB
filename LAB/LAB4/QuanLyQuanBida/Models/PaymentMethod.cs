using System;

namespace QuanLyQuanBida.Models
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // HÀM KHỞI TẠO
        public PaymentMethod() { }
    }
}

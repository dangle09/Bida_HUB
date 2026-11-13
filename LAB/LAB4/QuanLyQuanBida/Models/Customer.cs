using System;

namespace QuanLyQuanBida.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int MembershipTierId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int AccumulatedPoints { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime CreatedAt { get; set; }

        // HÀM KHỞI TẠO
        public Customer() { }
    }
}

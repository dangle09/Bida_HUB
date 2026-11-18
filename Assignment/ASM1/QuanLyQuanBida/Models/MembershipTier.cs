using System;

namespace QuanLyQuanBida.Models
{
    public class MembershipTier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MinPoints { get; set; }
        public decimal DiscountRate { get; set; }
        public string? Description { get; set; }

        // HÀM KHỞI TẠO
        public MembershipTier() { }
    }
}

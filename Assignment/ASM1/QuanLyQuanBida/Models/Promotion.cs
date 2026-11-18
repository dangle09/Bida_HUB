using System;

namespace QuanLyQuanBida.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // HÀM KHỞI TẠO
        public Promotion() { }
    }
}

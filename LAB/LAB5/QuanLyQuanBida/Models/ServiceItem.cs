using System;

namespace QuanLyQuanBida.Models
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }

        // HÀM KHỞI TẠO
        public ServiceItem() { }
    }
}

using System;

namespace QuanLyQuanBida.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }

        // HÀM KHỞI TẠO
        public Supplier() { }
    }
}

using System;

namespace QuanLyQuanBida.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Image { get; set; }

        // HÀM KHỞI TẠO
        public Category() { }
    }
}

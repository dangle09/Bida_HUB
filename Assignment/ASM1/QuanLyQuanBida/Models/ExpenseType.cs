using System;

namespace QuanLyQuanBida.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // HÀM KHỞI TẠO
        public ExpenseType() { }
    }
}

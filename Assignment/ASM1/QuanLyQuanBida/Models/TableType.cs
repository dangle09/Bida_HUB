using System;

namespace QuanLyQuanBida.Models
{
    public class TableType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }

        // HÀM KHỞI TẠO
        public TableType() { }
    }
}

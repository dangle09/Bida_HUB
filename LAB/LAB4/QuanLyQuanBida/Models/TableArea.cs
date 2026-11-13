using System;

namespace QuanLyQuanBida.Models
{
    public class TableArea
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public TableArea() { }
    }
}

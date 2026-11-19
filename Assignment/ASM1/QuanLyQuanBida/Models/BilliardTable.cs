using System;

namespace QuanLyQuanBida.Models
{
    public class BilliardTable
    {
        public int Id { get; set; }
        public int TableAreaId { get; set; }
        public int TableTypeId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public BilliardTable() { }
    }
}

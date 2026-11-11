using System;

namespace QuanLyQuanBida.Models
{
    public class TableEquipment
    {
        public int Id { get; set; }
        public int BilliardTableId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Status { get; set; } = string.Empty;

        // HÀM KHỞI TẠO
        public TableEquipment() { }
    }
}

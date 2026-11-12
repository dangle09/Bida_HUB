using System;

namespace QuanLyQuanBida.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ServiceItemId { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }

        // HÀM KHỞI TẠO
        public Inventory() { }
    }
}

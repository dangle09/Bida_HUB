using System;

namespace QuanLyQuanBida.Models
{
    public class GoodsReceiptDetail
    {
        public int Id { get; set; }
        public int GoodsReceiptId { get; set; }
        public int ServiceItemId { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }

        // HÀM KHỞI TẠO
        public GoodsReceiptDetail() { }
    }
}

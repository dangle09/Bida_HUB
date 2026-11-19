using System;

namespace QuanLyQuanBida.Models
{
    public class GoodsReceipt
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ImportDate { get; set; }
        public decimal TotalImportAmount { get; set; }

        // HÀM KHỞI TẠO
        public GoodsReceipt() { }
    }
}

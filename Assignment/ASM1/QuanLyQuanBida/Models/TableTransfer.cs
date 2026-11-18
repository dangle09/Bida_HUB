using System;

namespace QuanLyQuanBida.Models
{
    public class TableTransfer
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int FromTableId { get; set; }
        public int ToTableId { get; set; }
        public DateTime TransferTime { get; set; }
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public TableTransfer() { }
    }
}

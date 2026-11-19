using System;

namespace QuanLyQuanBida.Models
{
    public class InventoryAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime AuditDate { get; set; }
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public InventoryAudit() { }
    }
}

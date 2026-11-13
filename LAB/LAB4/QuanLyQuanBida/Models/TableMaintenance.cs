using System;

namespace QuanLyQuanBida.Models
{
    public class TableMaintenance
    {
        public int Id { get; set; }
        public int BilliardTableId { get; set; }
        public string MaintenanceType { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? PerformedBy { get; set; }
        public string? Notes { get; set; }

        // HÀM KHỞI TẠO
        public TableMaintenance() { }
    }
}

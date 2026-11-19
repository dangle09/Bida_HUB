using System;
using System.Collections.Generic;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.ViewModels
{
    public class AreaOccupancyRow
    {
        public string AreaName { get; set; } = "";
        public int PlayingCount { get; set; }
        public int ReservedCount { get; set; }
        public int AvailableCount { get; set; }
        public int TotalCount { get; set; }
        public int OccupancyPercent => TotalCount > 0 ? (int)Math.Round(PlayingCount * 100.0 / TotalCount) : 0;
    }

    public class DashboardOverviewViewModel
    {
        // Doanh thu & Tài chính (đồng bộ 100% /Invoices & /Reports)
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalRevenue - TotalExpenses;

        // Vận hành bàn & Hóa đơn
        public int PlayingTables { get; set; }
        public int TotalTables { get; set; }
        public int PaidInvoicesCount { get; set; }
        public int PlayingInvoicesCount { get; set; }

        // Hàng hóa, Thực đơn & Kho (đồng bộ 100% /Categories, /Menu & /Inventory)
        public int TotalCategories { get; set; }
        public int TotalMenuItems { get; set; }
        public int ActiveMenuItems { get; set; }
        public int LowStockCount { get; set; }
        public decimal TotalInventoryValue { get; set; }

        // Nhân sự & Hội viên (đồng bộ 100% /Staff & /Customers)
        public int TotalStaff { get; set; }
        public int ActiveStaff { get; set; }
        public int TotalCustomers { get; set; }
        public int VipCustomers { get; set; }

        // Bảng tổng hợp chi tiết
        public List<AreaOccupancyRow> AreaOccupancies { get; set; } = new();
        public List<InvoiceRowViewModel> RecentInvoices { get; set; } = new();
        public List<InventoryItemRow> LowStockItems { get; set; } = new();
        public List<TopItemSalesRow> TopSellingItems { get; set; } = new();
        public List<CustomerRowViewModel> TopCustomers { get; set; } = new();
        public List<ActivityLog> RecentLogs { get; set; } = new();
        public Dictionary<int, string> UserNameDict { get; set; } = new();
    }
}

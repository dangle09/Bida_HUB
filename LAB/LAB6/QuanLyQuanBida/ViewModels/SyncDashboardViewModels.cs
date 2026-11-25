using System;
using System.Collections.Generic;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.ViewModels
{
    // =========================================================================
    // VIEWMODEL KHO & NHẬP HÀNG (ĐỒNG BỘ 100% VỚI THỰC ĐƠN ServiceItems + Inventories)
    // =========================================================================
    public class InventoryItemRow
    {
        public int ServiceItemId { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string Unit { get; set; } = "";
        public string? Image { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }
        public decimal AvgImportPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsActive { get; set; }

        public string StatusKey => StockQuantity == 0 ? "danger" : StockQuantity <= ReorderLevel ? "warning" : "success";
        public string StatusText => StockQuantity == 0 ? "HẾT HÀNG" : StockQuantity <= ReorderLevel ? "SẮP HẾT HÀNG" : "ĐỦ HÀNG";
    }

    public class GoodsReceiptRow
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string SupplierName { get; set; } = "";
        public string StaffName { get; set; } = "";
        public DateTime ImportDate { get; set; }
        public decimal TotalImportAmount { get; set; }
        public string ItemsSummary { get; set; } = "";
        public int LineCount { get; set; }
    }

    public class InventoryDashboardViewModel
    {
        public List<InventoryItemRow> Items { get; set; } = new();
        public List<GoodsReceiptRow> Receipts { get; set; } = new();
        public List<Supplier> Suppliers { get; set; } = new();
        public decimal TotalInventoryValue { get; set; }
        public int LowStockCount { get; set; }
        public decimal TotalImportedAmount { get; set; }
    }

    // =========================================================================
    // VIEWMODEL HÓA ĐƠN & THU NGÂN (ĐỒNG BỘ 100% VỚI DOANH THU & BÁO CÁO)
    // =========================================================================
    public class InvoiceLineDisplay
    {
        public string ItemName { get; set; } = "";
        public string Unit { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class InvoiceRowViewModel
    {
        public int Id { get; set; }
        public string InvoiceCode { get; set; } = "";
        public string TableName { get; set; } = "";
        public bool IsVipTable { get; set; }
        public string CustomerName { get; set; } = "";
        public string StaffName { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DurationText { get; set; } = "";
        public decimal TableAmount { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalPayment { get; set; }
        public string PaymentMethodName { get; set; } = "";
        public string PaymentMethodKey { get; set; } = "";
        public string Status { get; set; } = "";
        public List<InvoiceLineDisplay> Details { get; set; } = new();
    }

    public class InvoicesPageViewModel
    {
        public List<InvoiceRowViewModel> Invoices { get; set; } = new();
        public decimal TotalRevenue { get; set; }
        public int PaidCount { get; set; }
        public int PlayingCount { get; set; }
        public decimal CashTotal { get; set; }
        public decimal TransferTotal { get; set; }
        public decimal CardTotal { get; set; }
    }

    // =========================================================================
    // VIEWMODEL DOANH THU & BÁO CÁO (ĐỒNG BỘ 100% VỚI HÓA ĐƠN & PHIẾU CHI)
    // =========================================================================
    public class TopItemSalesRow
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Unit { get; set; } = "";
        public int TotalQty { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class ExpenseVoucherRow
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string ExpenseTypeName { get; set; } = "";
        public string StaffName { get; set; } = "";
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Purpose { get; set; } = "";
        public string? Notes { get; set; }
    }

    public class ReportsDashboardViewModel
    {
        public string SelectedRange { get; set; } = "all";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TableRevenue { get; set; }
        public decimal ServiceRevenue { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetProfit => TotalRevenue - TotalExpenses;
        public int CompletedInvoiceCount { get; set; }
        public int PlayingTableCount { get; set; }
        public int TotalTableCount { get; set; }
        public decimal CashRevenue { get; set; }
        public decimal TransferRevenue { get; set; }
        public decimal CardRevenue { get; set; }
        public List<InvoiceRowViewModel> Invoices { get; set; } = new();
        public List<TopItemSalesRow> TopItems { get; set; } = new();
        public List<ExpenseVoucherRow> Vouchers { get; set; } = new();
        public List<ExpenseType> ExpenseTypes { get; set; } = new();
        public decimal[] HourlyToday { get; set; } = new decimal[8]; // 08h,10h,12h,14h,16h,18h,20h,22h
        public decimal[] HourlyYesterday { get; set; } = new decimal[8];
    }

    // =========================================================================
    // VIEWMODEL KHÁCH HÀNG & VIP (ĐỒNG BỘ SQL SERVER)
    // =========================================================================
    public class CustomerRowViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string TierName { get; set; } = "";
        public decimal DiscountRate { get; set; }
        public int AccumulatedPoints { get; set; }
        public decimal AccountBalance { get; set; }
        public decimal TotalSpent { get; set; }
        public int InvoiceCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomersPageViewModel
    {
        public List<CustomerRowViewModel> Customers { get; set; } = new();
        public List<CustomerRowViewModel> AllCustomerDropdown { get; set; } = new();
        public List<MembershipTier> Tiers { get; set; } = new();
        public List<CustomerCue> Cues { get; set; } = new();
        public Dictionary<int, string> CustomerNameDict { get; set; } = new();
        public List<Locker> Lockers { get; set; } = new();
        public List<Locker> AvailableLockers { get; set; } = new();
        public int DiamondCount { get; set; }
        public int GoldCount { get; set; }
        public int TotalPoints { get; set; }

        // Phân trang chuẩn 30 dòng / trang
        public int PageSize { get; set; } = 30;
        public int MemberPage { get; set; } = 1;
        public int MemberTotalCount { get; set; }
        public int MemberTotalPages => Math.Max(1, (int)Math.Ceiling(MemberTotalCount / (double)PageSize));

        public int LockerPage { get; set; } = 1;
        public int LockerTotalCount { get; set; }
        public int LockerRentedCount { get; set; }
        public int LockerTotalPages => Math.Max(1, (int)Math.Ceiling(LockerTotalCount / (double)PageSize));
        public string ActiveTab { get; set; } = "hoivien";
    }

    // =========================================================================
    // VIEWMODEL SƠ ĐỒ BÀN POS & QUẢN LÝ BÀN GIÁ (ĐỒNG BỘ 100% SQL SERVER)
    // =========================================================================
    public class PosTableCardViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int AreaId { get; set; }
        public string AreaCode { get; set; } = "";
        public string AreaName { get; set; } = "";
        public string TableTypeName { get; set; } = "";
        public decimal HourlyRate { get; set; }
        public string Status { get; set; } = "";
        public int? ActiveInvoiceId { get; set; }
        public string? ActiveInvoiceCode { get; set; }
        public string? ActiveCustomerName { get; set; }
        public DateTime? StartTime { get; set; }
        public decimal CurrentTotal { get; set; }
    }

    public class PosPageViewModel
    {
        public List<PosTableCardViewModel> Tables { get; set; } = new();
        public List<TableArea> Areas { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public List<PaymentMethod> PaymentMethods { get; set; } = new();
        public int TotalTables { get; set; }
        public int PlayingTables { get; set; }
        public int AvailableTables { get; set; }
        public int ReservedTables { get; set; }
        public decimal TodayRevenue { get; set; }
    }

    public class TimeSlotRowViewModel
    {
        public int Id { get; set; }
        public string TableTypeName { get; set; } = "";
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }
        public decimal SlotPrice { get; set; }
    }

    public class TablesManageViewModel
    {
        public List<PosTableCardViewModel> Tables { get; set; } = new();
        public List<TableArea> Areas { get; set; } = new();
        public List<TableType> TableTypes { get; set; } = new();
        public List<TimeSlotRowViewModel> TimeSlots { get; set; } = new();
        public List<TableEquipment> Equipments { get; set; } = new();
        public Dictionary<int, string> TableNameDict { get; set; } = new();
    }
}

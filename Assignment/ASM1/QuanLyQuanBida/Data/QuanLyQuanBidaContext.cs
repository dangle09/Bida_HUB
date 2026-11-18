using System;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data
{
    public class QuanLyQuanBidaContext : DbContext
    {
        public QuanLyQuanBidaContext(DbContextOptions<QuanLyQuanBidaContext> options) : base(options)
        {
        }

        // 1. Phân hệ Cơ bản & Danh mục (Catalog & Lookups)
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<TableArea> TableAreas { get; set; }
        public DbSet<TableType> TableTypes { get; set; }
        public DbSet<MembershipTier> MembershipTiers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Locker> Lockers { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        // 2. Phân hệ Phụ thuộc cấp 1 (Primary Entities)
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeSlotPrice> TimeSlotPrices { get; set; }
        public DbSet<BilliardTable> BilliardTables { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        // 3. Phân hệ Nghiệp vụ & Vận hành (Operations & Business)
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
        public DbSet<Timekeeping> Timekeepings { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<InventoryAudit> InventoryAudits { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TableEquipment> TableEquipments { get; set; }
        public DbSet<TableMaintenance> TableMaintenances { get; set; }
        public DbSet<PointTransaction> PointTransactions { get; set; }
        public DbSet<LockerRental> LockerRentals { get; set; }
        public DbSet<CustomerCue> CustomerCues { get; set; }
        public DbSet<TournamentParticipant> TournamentParticipants { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PaymentVoucher> PaymentVouchers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        // 4. Phân hệ Chi tiết & Giao dịch (Transaction Details)
        public DbSet<GoodsReceiptDetail> GoodsReceiptDetails { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<TableTransfer> TableTransfers { get; set; }
        public DbSet<InvoicePayment> InvoicePayments { get; set; }

        // 5. Phân hệ AI-Native (3 Bảng bắt buộc môn NET108 theo nhận xét của thầy)
        public DbSet<AiSession> AiSessions { get; set; }
        public DbSet<AiMessage> AiMessages { get; set; }
        public DbSet<AiState> AiStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tự động quét và nạp toàn bộ 44 file cấu hình trong Data/Configurations/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuanLyQuanBidaContext).Assembly);
        }
    }
}

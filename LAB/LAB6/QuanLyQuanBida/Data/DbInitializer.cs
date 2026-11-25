using System;
using System.Collections.Generic;
using System.Linq;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuanLyQuanBidaContext context)
        {
            context.Database.EnsureCreated();

            // Nếu database đã có bảng nền, vẫn chạy đồng bộ & bổ sung dữ liệu mẫu phong phú (idempotent)
            if (context.Roles.Any())
            {
                EnsureRichSynchronizedData(context);
                return;
            }

            // ==========================================
            // GIAI ĐOẠN 1: BẢNG DANH MỤC ĐỘC LẬP (LOOKUP TABLES)
            // ==========================================

            // 1. Roles
            var roles = new Role[]
            {
                new() { Name = "Admin", Description = "Quản trị viên toàn quyền hệ thống" },
                new() { Name = "Manager", Description = "Quản lý điều hành cơ sở CLB Bida" },
                new() { Name = "Cashier", Description = "Nhân viên thu ngân bán hàng POS" },
                new() { Name = "Waiter", Description = "Nhân viên phục vụ xếp bi, chạy bàn" }
            };
            context.Roles.AddRange(roles);

            // 2. Permissions
            var permissions = new Permission[]
            {
                new() { Code = "VIEW_REPORTS", Name = "Xem báo cáo thống kê doanh thu", Module = "REPORTS" },
                new() { Code = "POS_SALES", Name = "Mở bàn, order món và thanh toán", Module = "POS" },
                new() { Code = "CANCEL_INVOICE", Name = "Hủy hóa đơn đã xuất bill", Module = "POS" },
                new() { Code = "STOCK_IMPORT", Name = "Lập và phê duyệt phiếu nhập hàng", Module = "INVENTORY" },
                new() { Code = "HR_MANAGEMENT", Name = "Xếp ca và tính lương nhân viên", Module = "HR" }
            };
            context.Permissions.AddRange(permissions);

            // 3. Shifts
            var shifts = new Shift[]
            {
                new() { Name = "Ca Sáng", StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(16, 0, 0), HourlyRate = 25000 },
                new() { Name = "Ca Chiều Tối", StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(23, 59, 59), HourlyRate = 28000 },
                new() { Name = "Ca Đêm", StartTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(8, 0, 0), HourlyRate = 32000 }
            };
            context.Shifts.AddRange(shifts);

            // 4. TableAreas
            var tableAreas = new TableArea[]
            {
                new() { Code = "FLOOR_1", Name = "Tầng 1 - Khu Bàn Lỗ Tiêu Chuẩn" },
                new() { Code = "FLOOR_2", Name = "Tầng 2 - Khu Bida Phăng Carom" },
                new() { Code = "VIP_ROOM", Name = "Phòng VIP 3 Băng Máy Lạnh Cao Cấp" }
            };
            context.TableAreas.AddRange(tableAreas);

            // 5. TableTypes
            var tableTypes = new TableType[]
            {
                new() { Name = "Bida Lỗ (Pool 9 Feet)", BasePrice = 60000 },
                new() { Name = "Bida Phăng (Carom Libre)", BasePrice = 50000 },
                new() { Name = "Bida 3 Băng (3-Cushion)", BasePrice = 80000 },
                new() { Name = "Snooker Quốc Tế", BasePrice = 100000 }
            };
            context.TableTypes.AddRange(tableTypes);

            // 6. MembershipTiers
            var membershipTiers = new MembershipTier[]
            {
                new() { Name = "Hội viên Đồng", MinPoints = 0, DiscountRate = 0, Description = "Hạng tiêu chuẩn khi mới đăng ký" },
                new() { Name = "Hội viên Bạc", MinPoints = 100, DiscountRate = 5, Description = "Tích lũy từ 100 điểm, giảm 5% giờ chơi" },
                new() { Name = "Hội viên Vàng", MinPoints = 500, DiscountRate = 10, Description = "Tích lũy từ 500 điểm, giảm 10% giờ chơi" },
                new() { Name = "Hội viên Kim Cương", MinPoints = 1000, DiscountRate = 15, Description = "VIP đặc quyền, giảm 15% tổng bill" }
            };
            context.MembershipTiers.AddRange(membershipTiers);

            // 7. Categories (Đồng bộ 100% với Web vãng lai - Gộp Cơ bida & Phụ kiện vào 1 danh mục)
            var categories = new Category[]
            {
                new() { Code = "DRINK", Name = "Nước Giải Khát & Cà Phê", Image = "images/fnb/fnb-1-caphe-muoi.png" },
                new() { Code = "FOOD", Name = "Ẩm Thực F&B & Đồ Ăn Nhanh", Image = "images/fnb/fnb-4-mi-xuc-xich.jpg" },
                new() { Code = "BEER", Name = "Bia Các Loại Ướp Lạnh", Image = "images/fnb/fnb-9-heineken.jpg" },
                new() { Code = "ACCESSORIES", Name = "Dụng Cụ, Cơ Bida & Phụ Kiện Cơ Thủ", Image = "images/shop/shop-1-co-fury.jpg" }
            };
            context.Categories.AddRange(categories);

            // 8. Suppliers
            var suppliers = new Supplier[]
            {
                new() { Name = "Công ty TNHH Nước Giải Khát Suntory PepsiCo", PhoneNumber = "02838234567", Address = "Quận 1, TP. Hồ Chí Minh" },
                new() { Name = "Đại lý Phân Phối Bia Nước Ngọt Minh Châu", PhoneNumber = "0903112233", Address = "Đống Đa, Hà Nội" },
                new() { Name = "Xưởng Phụ Kiện & Thiết Bị Bida Thịnh Kent", PhoneNumber = "0988776655", Address = "Bình Thạnh, TP. Hồ Chí Minh" }
            };
            context.Suppliers.AddRange(suppliers);

            // 9. Promotions
            var promotions = new Promotion[]
            {
                new() { Code = "OPENING_20", Name = "Khuyến mãi Khai trương Giảm 20% Giờ chơi", DiscountPercent = 20, DiscountAmount = 0, StartDate = new DateTime(2026, 11, 1), EndDate = new DateTime(2026, 11, 30) },
                new() { Code = "HAPPY_HOUR", Name = "Giờ Vàng Giảm Giá Buổi Sáng", DiscountPercent = 10, DiscountAmount = 0, StartDate = new DateTime(2026, 11, 1), EndDate = new DateTime(2026, 12, 31) },
                new() { Code = "VOUCHER_50K", Name = "Giảm 50K cho hóa đơn trên 300K", DiscountPercent = 0, DiscountAmount = 50000, StartDate = new DateTime(2026, 11, 1), EndDate = new DateTime(2026, 11, 15) }
            };
            context.Promotions.AddRange(promotions);

            // 10. PaymentMethods
            var paymentMethods = new PaymentMethod[]
            {
                new() { Name = "Tiền mặt (Cash)", IsActive = true },
                new() { Name = "Chuyển khoản VietQR", IsActive = true },
                new() { Name = "Quẹt thẻ POS", IsActive = true }
            };
            context.PaymentMethods.AddRange(paymentMethods);

            // 11. Lockers
            var lockers = new Locker[]
            {
                new() { LockerNumber = "TU_01", Area = "Khu Tủ Tầng 1", Status = "Đang thuê", MonthlyRent = 150000 },
                new() { LockerNumber = "TU_02", Area = "Khu Tủ Tầng 1", Status = "Trống", MonthlyRent = 150000 },
                new() { LockerNumber = "TU_VIP_01", Area = "Phòng VIP Tầng 2", Status = "Đang thuê", MonthlyRent = 200000 }
            };
            context.Lockers.AddRange(lockers);

            // 12. Tournaments
            var tournaments = new Tournament[]
            {
                new() { TournamentName = "Giải Bida Lỗ Open CLB Mùa Đông 2026", StartDate = new DateTime(2026, 12, 1), EndDate = new DateTime(2026, 12, 5), EntryFee = 200000, TotalPrize = 10000000, Status = "Đang mở đăng ký" },
                new() { TournamentName = "Giải Bida Phăng Giao Lưu Nội Bộ", StartDate = new DateTime(2026, 11, 20), EndDate = new DateTime(2026, 11, 21), EntryFee = 100000, TotalPrize = 3000000, Status = "Sắp diễn ra" }
            };
            context.Tournaments.AddRange(tournaments);

            // 13. ExpenseTypes
            var expenseTypes = new ExpenseType[]
            {
                new() { Name = "Tiền điện sinh hoạt và Máy lạnh", Description = "Hóa đơn điện lực hàng tháng" },
                new() { Name = "Tiền nước sinh hoạt", Description = "Hóa đơn nước máy" },
                new() { Name = "Chi phí bảo trì, căng nỉ bàn bida", Description = "Căng nỉ mới định kỳ" }
            };
            context.ExpenseTypes.AddRange(expenseTypes);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 2: BẢNG PHỤ THUỘC CẤP 1
            // ==========================================

            // 14. RolePermissions
            var rolePermissions = new RolePermission[]
            {
                new() { RoleId = roles[0].Id, PermissionId = permissions[0].Id },
                new() { RoleId = roles[0].Id, PermissionId = permissions[1].Id },
                new() { RoleId = roles[0].Id, PermissionId = permissions[2].Id },
                new() { RoleId = roles[0].Id, PermissionId = permissions[3].Id },
                new() { RoleId = roles[0].Id, PermissionId = permissions[4].Id },
                new() { RoleId = roles[1].Id, PermissionId = permissions[0].Id },
                new() { RoleId = roles[1].Id, PermissionId = permissions[1].Id },
                new() { RoleId = roles[1].Id, PermissionId = permissions[3].Id },
                new() { RoleId = roles[2].Id, PermissionId = permissions[1].Id }
            };
            context.RolePermissions.AddRange(rolePermissions);

            // 15. Users: 3 Tài khoản quản lý (Admin, Manager, Cashier - Không tạo tài khoản Waiter)
            var users = new User[]
            {
                new() { Username = "admin", PasswordHash = HashPassword("giangcc111"), FullName = "Đào Ngọc Giang (Admin)", Email = "giangadmin@bms.com", PhoneNumber = "0347987360", IsActive = true, CreatedAt = DateTime.Now },
                new() { Username = "manager", PasswordHash = HashPassword("dangcc111"), FullName = "Lê Nhật Đăng (Quản lý Cơ sở)", Email = "manager@boltbilliards.vn", PhoneNumber = "0912334455", IsActive = true, CreatedAt = DateTime.Now },
                new() { Username = "cashier", PasswordHash = HashPassword("khoacc111"), FullName = "Trần Thị Mai (Thu ngân POS)", Email = "cashier@boltbilliards.vn", PhoneNumber = "0977889900", IsActive = true, CreatedAt = DateTime.Now }
            };
            context.Users.AddRange(users);

            // 16. TimeSlotPrices
            var timeSlotPrices = new TimeSlotPrice[]
            {
                new() { TableTypeId = tableTypes[0].Id, SlotStartTime = new TimeSpan(8, 0, 0), SlotEndTime = new TimeSpan(14, 0, 0), SlotPrice = 45000 },
                new() { TableTypeId = tableTypes[0].Id, SlotStartTime = new TimeSpan(18, 0, 0), SlotEndTime = new TimeSpan(23, 0, 0), SlotPrice = 60000 },
                new() { TableTypeId = tableTypes[2].Id, SlotStartTime = new TimeSpan(8, 0, 0), SlotEndTime = new TimeSpan(14, 0, 0), SlotPrice = 65000 }
            };
            context.TimeSlotPrices.AddRange(timeSlotPrices);

            // 17. BilliardTables
            var billiardTables = new BilliardTable[]
            {
                new() { TableAreaId = tableAreas[0].Id, TableTypeId = tableTypes[0].Id, Code = "BAN_01", Name = "Bàn 01 (Lỗ)", Status = "Đang chơi" },
                new() { TableAreaId = tableAreas[0].Id, TableTypeId = tableTypes[0].Id, Code = "BAN_02", Name = "Bàn 02 (Lỗ)", Status = "Trống" },
                new() { TableAreaId = tableAreas[0].Id, TableTypeId = tableTypes[0].Id, Code = "BAN_03", Name = "Bàn 03 (Lỗ)", Status = "Đã đặt trước" },
                new() { TableAreaId = tableAreas[1].Id, TableTypeId = tableTypes[1].Id, Code = "BAN_04", Name = "Bàn 04 (Phăng)", Status = "Trống" },
                new() { TableAreaId = tableAreas[2].Id, TableTypeId = tableTypes[2].Id, Code = "BAN_VIP_01", Name = "Bàn VIP 01 (3 Băng)", Status = "Đang chơi" }
            };
            context.BilliardTables.AddRange(billiardTables);

            // 18. Customers
            var customers = new Customer[]
            {
                new() { MembershipTierId = membershipTiers[2].Id, Code = "KH_001", FullName = "Đào Ngọc Giang", PhoneNumber = "0347987360", AccumulatedPoints = 550, AccountBalance = 500000, CreatedAt = DateTime.Now },
                new() { MembershipTierId = membershipTiers[1].Id, Code = "KH_002", FullName = "Nguyễn Hoàng Nam", PhoneNumber = "0912345678", AccumulatedPoints = 180, AccountBalance = 200000, CreatedAt = DateTime.Now },
                new() { MembershipTierId = membershipTiers[3].Id, Code = "KH_003", FullName = "Trần Quốc Bảo", PhoneNumber = "0909998877", AccumulatedPoints = 1250, AccountBalance = 1500000, CreatedAt = DateTime.Now }
            };
            context.Customers.AddRange(customers);

            // 19. ServiceItems (Đồng bộ 100% khớp danh sách món ăn & dụng cụ bida trên Web Vãng Lai)
            var serviceItems = new ServiceItem[]
            {
                // Nhóm 1: Nước Giải Khát & Cà Phê
                new() { CategoryId = categories[0].Id, Code = "SP_CF_MUOI", Name = "Cà Phê Muối Đặc Sản", Unit = "Ly", UnitPrice = 28000, Image = "images/fnb/fnb-1-caphe-muoi.png", IsActive = true },
                new() { CategoryId = categories[0].Id, Code = "SP_TRA_DAO", Name = "Trà Đào Cam Sả", Unit = "Ly", UnitPrice = 32000, Image = "images/fnb/fnb-2-tra-dao.jpg", IsActive = true },
                new() { CategoryId = categories[0].Id, Code = "SP_REDBULL", Name = "RedBull Thái Ướp Lạnh", Unit = "Lon", UnitPrice = 22000, Image = "images/fnb/fnb-3-redbull.jpg", IsActive = true },
                new() { CategoryId = categories[0].Id, Code = "SP_TRA_MC", Name = "Trà Mãng Cầu Tươi", Unit = "Ly", UnitPrice = 32000, Image = "images/fnb/fnb-8-tra-mang-cau.jpg", IsActive = true },
                new() { CategoryId = categories[0].Id, Code = "SP_C2", Name = "Trà xanh C2 hương chanh", Unit = "Chai", UnitPrice = 15000, Image = "images/fnb/fnb-2-tra-dao.jpg", IsActive = true },
                new() { CategoryId = categories[0].Id, Code = "SP_SUOI", Name = "Nước khoáng Aquafina 500ml", Unit = "Chai", UnitPrice = 10000, Image = "images/fnb/fnb-3-redbull.jpg", IsActive = true },

                // Nhóm 2: Ẩm Thực F&B & Đồ Ăn Nhanh
                new() { CategoryId = categories[1].Id, Code = "SP_MI_TRUNG", Name = "Mì Trứng Xúc Xích Bò", Unit = "Tô", UnitPrice = 35000, Image = "images/fnb/fnb-4-mi-xuc-xich.jpg", IsActive = true },
                new() { CategoryId = categories[1].Id, Code = "SP_BANH_MI", Name = "Bánh Mì Chảo Đặc Biệt", Unit = "Phần", UnitPrice = 38000, Image = "images/fnb/fnb-5-banh-mi-chao.jpg", IsActive = true },
                new() { CategoryId = categories[1].Id, Code = "SP_COM_CHIEN", Name = "Cơm Chiên Bò Dưa Chua", Unit = "Dĩa", UnitPrice = 45000, Image = "images/fnb/fnb-6-com-chien.jpg", IsActive = true },
                new() { CategoryId = categories[1].Id, Code = "SP_KHOAI_TAY", Name = "Khoai Tây Lắc Phô Mai", Unit = "Dĩa", UnitPrice = 30000, Image = "images/fnb/fnb-7-khoai-tay-lac.jpg", IsActive = true },
                new() { CategoryId = categories[1].Id, Code = "SP_KHO_BO", Name = "Khô Bò Vắt Chanh Ớt", Unit = "Dĩa", UnitPrice = 40000, Image = "images/fnb/fnb-10-kho-bo.jpg", IsActive = true },

                // Nhóm 3: Bia Các Loại Ướp Lạnh
                new() { CategoryId = categories[2].Id, Code = "SP_HEINEKEN", Name = "Heineken Silver Ướp Lạnh", Unit = "Lon", UnitPrice = 28000, Image = "images/fnb/fnb-9-heineken.jpg", IsActive = true },
                new() { CategoryId = categories[2].Id, Code = "SP_TIGER", Name = "Bia Tiger Bạc 330ml", Unit = "Lon", UnitPrice = 25000, Image = "images/fnb/fnb-9-heineken.jpg", IsActive = true },
                new() { CategoryId = categories[2].Id, Code = "SP_CORONA", Name = "Bia Corona Extra Chai", Unit = "Chai", UnitPrice = 38000, Image = "images/fnb/fnb-9-heineken.jpg", IsActive = true },

                // Nhóm 4: Dụng Cụ, Cơ Bida & Phụ Kiện Cơ Thủ (gộp cơ bida, ngọn carbon & phụ kiện)
                new() { CategoryId = categories[3].Id, Code = "SP_CO_FURY", Name = "Cơ Bida Fury Carbon Pro", Unit = "Cây", UnitPrice = 2850000, Image = "images/shop/shop-1-co-fury.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_CO_AILEEX", Name = "Cơ Bida Lỗ Aileex Carbon", Unit = "Cây", UnitPrice = 1650000, Image = "images/shop/shop-1-co-fury.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_KHUC_NOI", Name = "Khúc Nối Cơ Carbon Extension", Unit = "Cây", UnitPrice = 220000, Image = "images/shop/shop-5-khuc-noi.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_GANG_TAY", Name = "Găng Tay Bolt Co Giãn", Unit = "Cái", UnitPrice = 65000, Image = "images/shop/shop-2-gang-tay.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_LO_MASTER", Name = "Lơ Bida Master USA (Hộp)", Unit = "Hộp", UnitPrice = 40000, Image = "images/shop/shop-3-lo-master.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_BAO_DA", Name = "Bao Da Đựng Cơ 1 Gốc 2 Ngọn", Unit = "Bao", UnitPrice = 350000, Image = "images/shop/shop-4-bao-da.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_DAU_TAY", Name = "Đầu Tẩy Kamui Nhật Bản", Unit = "Hộp", UnitPrice = 180000, Image = "images/shop/shop-6-dau-tay-kamui.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_KHAN_LAU", Name = "Khăn Lau Cơ Chuyên Dụng", Unit = "Cái", UnitPrice = 30000, Image = "images/shop/shop-7-khan-lau.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_BIT_REN", Name = "Nắp Bịt Ren Bảo Vệ Khớp Nối", Unit = "Bộ", UnitPrice = 80000, Image = "images/shop/shop-8-bit-ren.jpg", IsActive = true }
            };
            context.ServiceItems.AddRange(serviceItems);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 3: BẢNG PHỤ THUỘC CẤP 2
            // ==========================================

            // 20. UserRoles (Admin -> Role 1, Manager -> Role 2, Cashier -> Role 3)
            var userRoles = new UserRole[]
            {
                new() { UserId = users[0].Id, RoleId = roles[0].Id, CreatedAt = DateTime.Now }, // admin -> Admin
                new() { UserId = users[1].Id, RoleId = roles[1].Id, CreatedAt = DateTime.Now }, // manager -> Manager
                new() { UserId = users[2].Id, RoleId = roles[2].Id, CreatedAt = DateTime.Now }  // cashier -> Cashier
            };
            context.UserRoles.AddRange(userRoles);

            // 21. ActivityLogs
            var activityLogs = new ActivityLog[]
            {
                new() { UserId = users[0].Id, Action = "LOGIN", TableName = "Users", RecordId = users[0].Id, Description = "Quản trị viên đăng nhập vào hệ thống", IpAddress = "192.168.1.10", CreatedAt = DateTime.Now },
                new() { UserId = users[1].Id, Action = "OPEN_TABLE", TableName = "Invoices", RecordId = 1, Description = "Mở Bàn 01 tính giờ cho khách Đào Ngọc Giang", IpAddress = "192.168.1.15", CreatedAt = DateTime.Now }
            };
            context.ActivityLogs.AddRange(activityLogs);

            // 22. WorkSchedules
            var workSchedules = new WorkSchedule[]
            {
                new() { UserId = users[1].Id, ShiftId = shifts[0].Id, WorkDate = new DateTime(2026, 11, 10), Notes = "Trực thu ngân quầy chính" },
                new() { UserId = users[2].Id, ShiftId = shifts[0].Id, WorkDate = new DateTime(2026, 11, 10), Notes = "Phục vụ tầng 1" }
            };
            context.WorkSchedules.AddRange(workSchedules);

            // 23. Timekeepings
            var timekeepings = new Timekeeping[]
            {
                new() { UserId = users[1].Id, WorkDate = new DateTime(2026, 11, 10), CheckInTime = new DateTime(2026, 11, 10, 7, 55, 0), CheckOutTime = new DateTime(2026, 11, 10, 16, 5, 0), WorkHours = 8.0m, LateMinutes = 0, EarlyLeaveMinutes = 0 },
                new() { UserId = users[2].Id, WorkDate = new DateTime(2026, 11, 10), CheckInTime = new DateTime(2026, 11, 10, 8, 5, 0), CheckOutTime = new DateTime(2026, 11, 10, 16, 0, 0), WorkHours = 7.9m, LateMinutes = 5, EarlyLeaveMinutes = 0 }
            };
            context.Timekeepings.AddRange(timekeepings);

            // 24. Payrolls
            var payrolls = new Payroll[]
            {
                new() { UserId = users[1].Id, SalaryMonth = 10, SalaryYear = 2026, TotalHours = 208, BaseSalary = 5824000, Allowance = 500000, Bonus = 300000, Penalty = 0, NetSalary = 6624000 },
                new() { UserId = users[2].Id, SalaryMonth = 10, SalaryYear = 2026, TotalHours = 200, BaseSalary = 5000000, Allowance = 400000, Bonus = 200000, Penalty = 50000, NetSalary = 5550000 }
            };
            context.Payrolls.AddRange(payrolls);

            // 25. InventoryAudits
            var inventoryAudits = new InventoryAudit[]
            {
                new() { UserId = users[0].Id, Code = "KK_20261101", AuditDate = new DateTime(2026, 11, 1), Notes = "Kiểm kê đầu tháng 11 toàn bộ đồ uống F&B" }
            };
            context.InventoryAudits.AddRange(inventoryAudits);

            // 26. Reservations
            var reservations = new Reservation[]
            {
                new() { CustomerId = customers[1].Id, BilliardTableId = billiardTables[2].Id, ArrivalTime = new DateTime(2026, 11, 10, 19, 30, 0), GuestCount = 4, Deposit = 100000, Status = "Đã đặt cọc" }
            };
            context.Reservations.AddRange(reservations);

            // 27. TableEquipments
            var tableEquipments = new TableEquipment[]
            {
                new() { BilliardTableId = billiardTables[0].Id, Name = "Bộ bi bida lỗ Aramith Pro Cup", Quantity = 1, Status = "Rất tốt" },
                new() { BilliardTableId = billiardTables[0].Id, Name = "Đèn chiếu sáng 3 chao LED", Quantity = 1, Status = "Hoạt động tốt" },
                new() { BilliardTableId = billiardTables[0].Id, Name = "Tam giác xếp bi gỗ sồi", Quantity = 1, Status = "Tốt" }
            };
            context.TableEquipments.AddRange(tableEquipments);

            // 28. TableMaintenances
            var tableMaintenances = new TableMaintenance[]
            {
                new() { BilliardTableId = billiardTables[0].Id, MaintenanceType = "Thay vải nỉ mặt bàn Simonis 860", Cost = 1800000, MaintenanceDate = new DateTime(2026, 10, 15), PerformedBy = "Thợ Hùng Bida", Notes = "Nỉ cao cấp thi đấu" }
            };
            context.TableMaintenances.AddRange(tableMaintenances);

            // 29. PointTransactions
            var pointTransactions = new PointTransaction[]
            {
                new() { CustomerId = customers[0].Id, TransactionType = "EARN", Points = 50, CreatedAt = DateTime.Now, Notes = "Tích điểm từ hóa đơn HD-0001" },
                new() { CustomerId = customers[0].Id, TransactionType = "REDEEM", Points = 100, CreatedAt = DateTime.Now, Notes = "Đổi 1 giờ chơi bida miễn phí" }
            };
            context.PointTransactions.AddRange(pointTransactions);

            // 30. LockerRentals
            var lockerRentals = new LockerRental[]
            {
                new() { LockerId = lockers[0].Id, CustomerId = customers[0].Id, StartDate = new DateTime(2026, 11, 1), EndDate = new DateTime(2026, 12, 1), MonthlyRent = 150000, Deposit = 100000, Status = "Đang thuê" }
            };
            context.LockerRentals.AddRange(lockerRentals);

            // 31. CustomerCues
            var customerCues = new CustomerCue[]
            {
                new() { CustomerId = customers[0].Id, Code = "GAY_001", CueBrand = "Predator P3 Cobalt", ShaftType = "Ngọn Revo 12.4 Carbon", ReceivedDate = new DateTime(2026, 11, 1), Status = "Đang lưu kho" }
            };
            context.CustomerCues.AddRange(customerCues);

            // 32. TournamentParticipants
            var tournamentParticipants = new TournamentParticipant[]
            {
                new() { TournamentId = tournaments[0].Id, CustomerId = customers[0].Id, SeedNumber = 1, RegisteredDate = new DateTime(2026, 11, 5, 10, 0, 0) },
                new() { TournamentId = tournaments[0].Id, CustomerId = customers[1].Id, SeedNumber = 4, RegisteredDate = new DateTime(2026, 11, 6, 14, 30, 0) },
                new() { TournamentId = tournaments[0].Id, CustomerId = customers[2].Id, SeedNumber = 2, RegisteredDate = new DateTime(2026, 11, 7, 9, 15, 0) }
            };
            context.TournamentParticipants.AddRange(tournamentParticipants);

            // 33. Matches
            var matches = new Match[]
            {
                new() { TournamentId = tournaments[0].Id, BilliardTableId = billiardTables[0].Id, RoundName = "Vòng Bán Kết 1", Player1Id = customers[0].Id, Player2Id = customers[1].Id, ScorePlayer1 = 9, ScorePlayer2 = 5, MatchTime = new DateTime(2026, 12, 4, 14, 0, 0), Status = "Đã kết thúc" }
            };
            context.Matches.AddRange(matches);

            // 34. PaymentVouchers
            var paymentVouchers = new PaymentVoucher[]
            {
                new() { ExpenseTypeId = expenseTypes[0].Id, UserId = users[0].Id, Code = "PC_20261101", Amount = 4500000, CreatedAt = DateTime.Now, ExpensePurpose = "Thanh toán tiền điện tháng 10 CLB", Notes = "Chuyển khoản EVN" }
            };
            context.PaymentVouchers.AddRange(paymentVouchers);

            // 35. Inventories
            var inventories = new Inventory[]
            {
                new() { ServiceItemId = serviceItems[0].Id, StockQuantity = 120, ReorderLevel = 24 },
                new() { ServiceItemId = serviceItems[1].Id, StockQuantity = 95, ReorderLevel = 24 },
                new() { ServiceItemId = serviceItems[2].Id, StockQuantity = 200, ReorderLevel = 48 },
                new() { ServiceItemId = serviceItems[3].Id, StockQuantity = 150, ReorderLevel = 48 },
                new() { ServiceItemId = serviceItems[4].Id, StockQuantity = 50, ReorderLevel = 10 },
                new() { ServiceItemId = serviceItems[5].Id, StockQuantity = 40, ReorderLevel = 10 }
            };
            context.Inventories.AddRange(inventories);

            // 36. GoodsReceipts
            var goodsReceipts = new GoodsReceipt[]
            {
                new() { SupplierId = suppliers[0].Id, UserId = users[0].Id, Code = "PN_20261102", ImportDate = new DateTime(2026, 11, 2, 9, 0, 0), TotalImportAmount = 3600000 }
            };
            context.GoodsReceipts.AddRange(goodsReceipts);

            // 37. Invoices
            var invoices = new Invoice[]
            {
                new() { BilliardTableId = billiardTables[0].Id, UserId = users[1].Id, CustomerId = customers[0].Id, PromotionId = promotions[0].Id, InvoiceCode = "HD_20261110_01", StartTime = new DateTime(2026, 11, 10, 13, 0, 0), EndTime = new DateTime(2026, 11, 10, 14, 30, 0), TableAmount = 90000, ServiceAmount = 75000, DiscountAmount = 18000, TotalPayment = 147000, Status = "Đã thanh toán" },
                new() { BilliardTableId = billiardTables[4].Id, UserId = users[1].Id, CustomerId = customers[2].Id, PromotionId = null, InvoiceCode = "HD_20261110_02", StartTime = new DateTime(2026, 11, 10, 14, 0, 0), EndTime = null, TableAmount = 80000, ServiceAmount = 40000, DiscountAmount = 0, TotalPayment = 120000, Status = "Đang chơi" }
            };
            context.Invoices.AddRange(invoices);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 4: BẢNG CHI TIẾT & GIAO DỊCH
            // ==========================================

            // 38. GoodsReceiptDetails
            var goodsReceiptDetails = new GoodsReceiptDetail[]
            {
                new() { GoodsReceiptId = goodsReceipts[0].Id, ServiceItemId = serviceItems[0].Id, Quantity = 100, ImportPrice = 14000 },
                new() { GoodsReceiptId = goodsReceipts[0].Id, ServiceItemId = serviceItems[1].Id, Quantity = 100, ImportPrice = 9000 }
            };
            context.GoodsReceiptDetails.AddRange(goodsReceiptDetails);

            // 39. InvoiceDetails
            var invoiceDetails = new InvoiceDetail[]
            {
                new() { InvoiceId = invoices[0].Id, ServiceItemId = serviceItems[0].Id, Quantity = 2, UnitPrice = 20000, TotalAmount = 40000 },
                new() { InvoiceId = invoices[0].Id, ServiceItemId = serviceItems[4].Id, Quantity = 1, UnitPrice = 35000, TotalAmount = 35000 }
            };
            context.InvoiceDetails.AddRange(invoiceDetails);

            // 40. TableTransfers
            var tableTransfers = new TableTransfer[]
            {
                new() { InvoiceId = invoices[0].Id, FromTableId = billiardTables[0].Id, ToTableId = billiardTables[4].Id, TransferTime = new DateTime(2026, 11, 10, 13, 45, 0), Notes = "Khách đổi từ Bàn Lỗ 01 sang Bàn VIP 3 Băng" }
            };
            context.TableTransfers.AddRange(tableTransfers);

            // 41. InvoicePayments
            var invoicePayments = new InvoicePayment[]
            {
                new() { InvoiceId = invoices[0].Id, PaymentMethodId = paymentMethods[1].Id, Amount = 147000, PaymentTime = new DateTime(2026, 11, 10, 14, 30, 0) }
            };
            context.InvoicePayments.AddRange(invoicePayments);

            // ==========================================
            // GIAI ĐOẠN 5: 3 BẢNG AI-NATIVE BẮT BUỘC (AiSessions, AiMessages, AiStates)
            // ==========================================

            // 42. AiSessions
            var aiSessions = new AiSession[]
            {
                new()
                {
                    UserId = users[1].Id,
                    CustomerId = customers[0].Id,
                    SessionKey = "SESS_20261110_01",
                    Title = "Tư vấn bàn & dịch vụ cho khách Đào Ngọc Giang",
                    StartedAt = DateTime.Now.AddHours(-2),
                    LastActiveAt = DateTime.Now.AddHours(-1),
                    IsActive = true
                },
                new()
                {
                    UserId = users[0].Id,
                    CustomerId = null,
                    SessionKey = "SESS_20261110_02",
                    Title = "Tra cứu doanh thu ca sáng & công suất bàn",
                    StartedAt = DateTime.Now.AddHours(-4),
                    LastActiveAt = DateTime.Now.AddHours(-3),
                    IsActive = false
                }
            };
            context.AiSessions.AddRange(aiSessions);
            context.SaveChanges();

            // 43. AiMessages
            var aiMessages = new AiMessage[]
            {
                new()
                {
                    AiSessionId = aiSessions[0].Id,
                    Role = "user",
                    Content = "Tối nay CLB còn bàn lỗ nào trống lúc 19h30 không bot?",
                    CreatedAt = DateTime.Now.AddHours(-2)
                },
                new()
                {
                    AiSessionId = aiSessions[0].Id,
                    Role = "assistant",
                    Content = "Dạ chào anh Giang! Tối nay lúc 19h30 CLB còn trống Bàn 02 (Lỗ). Em đã gợi ý giữ bàn trước giúp anh trên hệ thống rồi ạ!",
                    CreatedAt = DateTime.Now.AddHours(-2).AddSeconds(5)
                },
                new()
                {
                    AiSessionId = aiSessions[1].Id,
                    Role = "user",
                    Content = "Doanh thu ca sáng hôm nay đạt bao nhiêu?",
                    CreatedAt = DateTime.Now.AddHours(-4)
                },
                new()
                {
                    AiSessionId = aiSessions[1].Id,
                    Role = "assistant",
                    Content = "Dạ báo cáo anh Giang: Doanh thu ca sáng tạm tính là 3.850.000đ, công suất sử dụng bàn đạt 75%.",
                    CreatedAt = DateTime.Now.AddHours(-4).AddSeconds(6)
                }
            };
            context.AiMessages.AddRange(aiMessages);

            // 44. AiStates (AI Context Memory & Recommendation State)
            var aiStates = new AiState[]
            {
                new()
                {
                    AiSessionId = aiSessions[0].Id,
                    StateKey = "RECOMMENDED_COMBO",
                    StateValue = "{\"PreferredTable\": \"BAN_VIP_01\", \"SuggestedDrink\": \"SP_REDBULL\", \"DiscountApplied\": \"OPENING_20\"}",
                    UpdatedAt = DateTime.Now
                },
                new()
                {
                    AiSessionId = aiSessions[0].Id,
                    StateKey = "PEAK_HOUR_WARNING",
                    StateValue = "{\"ForecastFull\": true, \"PeakStart\": \"19:00\", \"PeakEnd\": \"22:30\", \"RecommendExtraStaff\": 2}",
                    UpdatedAt = DateTime.Now
                }
            };
            context.AiStates.AddRange(aiStates);

            context.SaveChanges();

            // Nạp bổ sung đồng bộ đầy đủ cho tất cả phân hệ (Nhân sự, Tồn kho, Phiếu nhập, Hóa đơn, Doanh thu, Hội viên...)
            EnsureRichSynchronizedData(context);
        }

        // =========================================================================
        // ĐỒNG BỘ & BỔ SUNG DỮ LIỆU MẪU PHONG PHÚ CHO TOÀN HỆ THỐNG (IDEMPOTENT)
        // Đảm bảo 100% số liệu khớp giữa: Thực đơn <-> Kho & Nhập hàng, Hóa đơn <-> Doanh thu, Nhân sự <-> Nhật ký
        // =========================================================================
        private static void EnsureRichSynchronizedData(QuanLyQuanBidaContext context)
        {
            var roles = context.Roles.ToList();
            int adminRoleId = roles.FirstOrDefault(r => r.Name == "Admin")?.Id ?? 1;
            int managerRoleId = roles.FirstOrDefault(r => r.Name == "Manager")?.Id ?? 2;
            int cashierRoleId = roles.FirstOrDefault(r => r.Name == "Cashier")?.Id ?? 3;
            int waiterRoleId = roles.FirstOrDefault(r => r.Name == "Waiter")?.Id ?? 4;

            // 1. CHUẨN HÓA ĐỒNG NHẤT USERNAME THEO QUY CHUẨN phânquyền_tên, GỘP TRÙNG TRẦN THỊ MAI & CẬP NHẬT EMAIL ADMIN
            var uAdmin = context.Users.FirstOrDefault(u => u.Username == "admin" || u.Username == "admin_giang");
            if (uAdmin != null)
            {
                uAdmin.Username = "admin_giang";
                uAdmin.Email = "lemonquestiondog112233@gmail.com";
            }
            var uManager = context.Users.FirstOrDefault(u => u.Username == "manager" || u.Username == "manager_dang");
            if (uManager != null)
            {
                uManager.Username = "manager_dang";
                if (uManager.Email == "manager@boltbilliards.vn") uManager.Email = "manager.dang@boltbilliards.vn";
            }
            var uCashier = context.Users.FirstOrDefault(u => u.Username == "cashier" || u.Username == "cashier_mai");
            if (uCashier != null)
            {
                uCashier.Username = "cashier_mai";
                if (uCashier.Email == "cashier@boltbilliards.vn") uCashier.Email = "cashier.mai@boltbilliards.vn";
            }
            context.SaveChanges();

            // Gộp và xóa bản ghi Trần Thị Mai bị trùng (thungan_mai) chuyển toàn bộ khóa ngoại sang cashier_mai
            var dupMai = context.Users.FirstOrDefault(u => u.Username == "thungan_mai");
            if (dupMai != null && uCashier != null && dupMai.Id != uCashier.Id)
            {
                int oldId = dupMai.Id;
                int newId = uCashier.Id;
                foreach (var x in context.Invoices.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.ActivityLogs.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.WorkSchedules.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.Timekeepings.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.Payrolls.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.InventoryAudits.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.GoodsReceipts.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.PaymentVouchers.Where(i => i.UserId == oldId)) x.UserId = newId;
                foreach (var x in context.AiSessions.Where(i => i.UserId == oldId)) x.UserId = newId;
                var oldRoles = context.UserRoles.Where(ur => ur.UserId == oldId).ToList();
                if (oldRoles.Any()) context.UserRoles.RemoveRange(oldRoles);
                context.Users.Remove(dupMai);
                context.SaveChanges();
            }

            // Bổ sung đầy đủ 10 nhân sự chuẩn quy tắc phânquyền_tên
            var extraStaff = new (string Username, string Password, string FullName, string Email, string Phone, int RoleId, bool IsActive, int DaysAgo)[]
            {
                ("manager_hung", "hung1234", "Nguyễn Quốc Hùng (Quản lý Ca Tối)", "manager.hung@boltbilliards.vn", "0908112233", managerRoleId, true, 25),
                ("cashier_linh", "linh1234", "Phạm Thùy Linh (Thu ngân Ca Sáng)", "cashier.linh@boltbilliards.vn", "0932445566", cashierRoleId, true, 22),
                ("cashier_nam", "nam1234", "Võ Minh Nam (Thu ngân Ca Đêm)", "cashier.nam@boltbilliards.vn", "0919556677", cashierRoleId, true, 18),
                ("waiter_tuan", "tuan1234", "Hoàng Anh Tuấn (Phục vụ Tầng 1)", "waiter.tuan@boltbilliards.vn", "0973667788", waiterRoleId, true, 16),
                ("waiter_huy", "huy1234", "Trần Gia Huy (Phục vụ Tầng 2)", "waiter.huy@boltbilliards.vn", "0964778899", waiterRoleId, true, 14),
                ("waiter_vy", "vy1234", "Lê Thảo Vy (Phục vụ Phòng VIP)", "waiter.vy@boltbilliards.vn", "0945889900", waiterRoleId, true, 10),
                ("waiter_khoa", "khoa1234", "Đỗ Đăng Khoa (Phục vụ - Nghỉ phép)", "waiter.khoa@boltbilliards.vn", "0981990011", waiterRoleId, false, 30)
            };

            foreach (var s in extraStaff)
            {
                if (!context.Users.Any(u => u.Username == s.Username))
                {
                    var u = new User
                    {
                        Username = s.Username,
                        PasswordHash = HashPassword(s.Password),
                        FullName = s.FullName,
                        Email = s.Email,
                        PhoneNumber = s.Phone,
                        IsActive = s.IsActive,
                        CreatedAt = DateTime.Now.AddDays(-s.DaysAgo)
                    };
                    context.Users.Add(u);
                    context.SaveChanges();

                    context.UserRoles.Add(new UserRole
                    {
                        UserId = u.Id,
                        RoleId = s.RoleId,
                        CreatedAt = u.CreatedAt
                    });
                }
            }
            context.SaveChanges();

            // 2. ĐỒNG BỘ TỒN KHO 100% CHO TOÀN BỘ SERVICEITEMS (KHÔNG ĐỂ SÓT MÓN TỒN = 0 BẤT THƯỜNG)
            var allItems = context.ServiceItems.OrderBy(i => i.Id).ToList();
            var existingInvItemIds = context.Inventories.Select(i => i.ServiceItemId).ToHashSet();

            // Định mức tồn kho thực tế đồng bộ giữa /Menu và /Inventory
            var presetStock = new Dictionary<string, (int Stock, int Reorder)>(StringComparer.OrdinalIgnoreCase)
            {
                ["SP_CF_MUOI"] = (85, 20),
                ["SP_TRA_DAO"] = (72, 20),
                ["SP_REDBULL"] = (8, 24),      // Sắp hết hàng (cảnh báo đồng bộ cả Menu & Kho)
                ["SP_TRA_MC"] = (64, 15),
                ["SP_C2"] = (96, 24),
                ["SP_SUOI"] = (140, 30),
                ["SP_MI_TRUNG"] = (55, 15),
                ["SP_BANH_MI"] = (42, 12),
                ["SP_COM_CHIEN"] = (38, 10),
                ["SP_KHOAI_TAY"] = (48, 12),
                ["SP_KHO_BO"] = (6, 15),       // Sắp hết hàng
                ["SP_HEINEKEN"] = (10, 24),    // Sắp hết hàng
                ["SP_TIGER"] = (78, 24),
                ["SP_CORONA"] = (36, 12),
                ["SP_CO_FURY"] = (3, 5),       // Sắp hết hàng
                ["SP_CO_AILEEX"] = (7, 4),
                ["SP_KHUC_NOI"] = (14, 5),
                ["SP_GANG_TAY"] = (34, 10),
                ["SP_LO_MASTER"] = (65, 20),
                ["SP_BAO_DA"] = (12, 4),
                ["SP_DAU_TAY"] = (22, 8),
                ["SP_KHAN_LAU"] = (45, 15),
                ["SP_BIT_REN"] = (28, 10)
            };

            bool invChanged = false;
            foreach (var item in allItems)
            {
                if (!existingInvItemIds.Contains(item.Id))
                {
                    var (stock, reorder) = presetStock.TryGetValue(item.Code, out var pair)
                        ? pair
                        : (40, 10);

                    context.Inventories.Add(new Inventory
                    {
                        ServiceItemId = item.Id,
                        StockQuantity = stock,
                        ReorderLevel = reorder
                    });
                    invChanged = true;
                }
            }
            if (invChanged) context.SaveChanges();

            // Đảm bảo một số mặt hàng mẫu có mức cảnh báo thực tế (nếu trước đó seed số quá cao)
            var redbullItem = allItems.FirstOrDefault(i => i.Code == "SP_REDBULL");
            if (redbullItem != null)
            {
                var rbInv = context.Inventories.FirstOrDefault(i => i.ServiceItemId == redbullItem.Id);
                if (rbInv != null && rbInv.StockQuantity == 200)
                {
                    rbInv.StockQuantity = 8;
                    rbInv.ReorderLevel = 24;
                    context.SaveChanges();
                }
            }

            // 3. BỔ SUNG NHÀ CUNG CẤP & PHIẾU NHẬP KHO ĐỒNG BỘ VỚI SERVICEITEMS
            var extraSuppliers = new (string Name, string Phone, string Address)[]
            {
                ("Công ty TNHH Heineken Việt Nam - CN Đồng Nai", "02513889900", "KCN Biên Hòa 2, Đồng Nai"),
                ("Tổng Kho Nguyên Liệu F&B & Thực Phẩm Miền Nam", "0909665544", "TP. Thủ Đức, TP. Hồ Chí Minh")
            };
            foreach (var sup in extraSuppliers)
            {
                if (!context.Suppliers.Any(x => x.Name == sup.Name))
                {
                    context.Suppliers.Add(new Supplier { Name = sup.Name, PhoneNumber = sup.Phone, Address = sup.Address });
                }
            }
            context.SaveChanges();

            var suppliers = context.Suppliers.OrderBy(s => s.Id).ToList();
            var adminUser = context.Users.FirstOrDefault(u => u.Username == "admin") ?? context.Users.First();
            var managerUser = context.Users.FirstOrDefault(u => u.Username == "manager") ?? adminUser;

            if (context.GoodsReceipts.Count() < 5 && suppliers.Count >= 3 && allItems.Count >= 15)
            {
                var today = DateTime.Today;
                var receiptSpecs = new (string Code, int SupIdx, int UserId, DateTime Date, (int ItemIdx, int Qty, decimal Price)[] Lines)[]
                {
                    ("PN_202611_002", 1, adminUser.Id, today.AddDays(-6).AddHours(9), new[] { (11, 72, 18500m), (12, 96, 16500m), (13, 36, 26000m) }),
                    ("PN_202611_003", 2, managerUser.Id, today.AddDays(-4).AddHours(14), new[] { (14, 4, 1950000m), (17, 30, 38000m), (18, 50, 22000m) }),
                    ("PN_202611_004", 0, adminUser.Id, today.AddDays(-2).AddHours(10), new[] { (0, 60, 14000m), (1, 50, 15000m), (4, 80, 9000m), (5, 120, 4500m) }),
                    ("PN_202611_005", suppliers.Count - 1, managerUser.Id, today.AddDays(-1).AddHours(15), new[] { (6, 40, 18000m), (7, 35, 21000m), (8, 30, 24000m), (9, 40, 14000m) })
                };

                foreach (var spec in receiptSpecs)
                {
                    if (!context.GoodsReceipts.Any(gr => gr.Code == spec.Code))
                    {
                        decimal total = spec.Lines.Sum(l => l.Qty * l.Price);
                        var gr = new GoodsReceipt
                        {
                            SupplierId = suppliers[Math.Min(spec.SupIdx, suppliers.Count - 1)].Id,
                            UserId = spec.UserId,
                            Code = spec.Code,
                            ImportDate = spec.Date,
                            TotalImportAmount = total
                        };
                        context.GoodsReceipts.Add(gr);
                        context.SaveChanges();

                        foreach (var l in spec.Lines)
                        {
                            if (l.ItemIdx < allItems.Count)
                            {
                                context.GoodsReceiptDetails.Add(new GoodsReceiptDetail
                                {
                                    GoodsReceiptId = gr.Id,
                                    ServiceItemId = allItems[l.ItemIdx].Id,
                                    Quantity = l.Qty,
                                    ImportPrice = l.Price
                                });
                            }
                        }
                        context.SaveChanges();
                    }
                }
            }

            // 4. ĐỒNG BỘ CHÍNH XÁC 20 BÀN BIDA (KHỚP 100% SƠ ĐỒ BÀN POS & QUẢN LÝ BÀN: 14 ĐANG CHƠI, 5 TRỐNG, 1 ĐẶT TRƯỚC)
            var areas = context.TableAreas.OrderBy(a => a.Id).ToList();
            var tTypes = context.TableTypes.OrderBy(t => t.Id).ToList();
            if (areas.Count >= 3 && tTypes.Count >= 3)
            {
                // Chuyển đổi mã cũ BAN_VIP_01/02 sang BAN_17/BAN_18 để không trùng và giữ nguyên khóa ngoại
                var vip1 = context.BilliardTables.FirstOrDefault(b => b.Code == "BAN_VIP_01");
                if (vip1 != null && !context.BilliardTables.Any(b => b.Code == "BAN_17"))
                {
                    vip1.Code = "BAN_17";
                    vip1.Name = "Bàn 17 (VIP 3 Băng)";
                }
                var vip2 = context.BilliardTables.FirstOrDefault(b => b.Code == "BAN_VIP_02");
                if (vip2 != null && !context.BilliardTables.Any(b => b.Code == "BAN_18"))
                {
                    vip2.Code = "BAN_18";
                    vip2.Name = "Bàn 18 (VIP 3 Băng)";
                }
                context.SaveChanges();

                var targetTables = new (string Code, string Name, int AreaIdx, int TypeIdx, string Status)[]
                {
                    // Tầng 1 - 10 bàn Pool (7 đang chơi, 3 trống)
                    ("BAN_01", "Bàn 01 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_02", "Bàn 02 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_03", "Bàn 03 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_04", "Bàn 04 (Lỗ Pool)", 0, 0, "Trống"),
                    ("BAN_05", "Bàn 05 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_06", "Bàn 06 (Lỗ Pool)", 0, 0, "Trống"),
                    ("BAN_07", "Bàn 07 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_08", "Bàn 08 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_09", "Bàn 09 (Lỗ Pool)", 0, 0, "Đang chơi"),
                    ("BAN_10", "Bàn 10 (Lỗ Pool)", 0, 0, "Trống"),
                    // Tầng 2 - 6 bàn Carom (4 đang chơi, 2 trống)
                    ("BAN_11", "Bàn 11 (Carom Phăng)", 1, 1, "Đang chơi"),
                    ("BAN_12", "Bàn 12 (Carom Phăng)", 1, 1, "Đang chơi"),
                    ("BAN_13", "Bàn 13 (Carom Phăng)", 1, 1, "Trống"),
                    ("BAN_14", "Bàn 14 (Carom Phăng)", 1, 1, "Đang chơi"),
                    ("BAN_15", "Bàn 15 (Carom Phăng)", 1, 1, "Trống"),
                    ("BAN_16", "Bàn 16 (Carom Phăng)", 1, 1, "Đang chơi"),
                    // Phòng VIP - 4 bàn 3 Băng (3 đang chơi, 1 đặt trước)
                    ("BAN_17", "Bàn 17 (VIP 3 Băng)", 2, 2, "Đang chơi"),
                    ("BAN_18", "Bàn 18 (VIP 3 Băng)", 2, 2, "Đang chơi"),
                    ("BAN_19", "Bàn 19 (VIP 3 Băng)", 2, 2, "Đang chơi"),
                    ("BAN_20", "Bàn 20 (VIP 3 Băng)", 2, 2, "Đã đặt trước")
                };

                foreach (var t in targetTables)
                {
                    var existing = context.BilliardTables.FirstOrDefault(b => b.Code == t.Code);
                    if (existing == null)
                    {
                        context.BilliardTables.Add(new BilliardTable
                        {
                            TableAreaId = areas[t.AreaIdx].Id,
                            TableTypeId = tTypes[t.TypeIdx].Id,
                            Code = t.Code,
                            Name = t.Name,
                            Status = t.Status
                        });
                    }
                    else
                    {
                        existing.TableAreaId = areas[t.AreaIdx].Id;
                        existing.TableTypeId = tTypes[t.TypeIdx].Id;
                        existing.Name = t.Name;
                        existing.Status = t.Status;
                    }
                }
                context.SaveChanges();
            }

            // 5. BỔ SUNG KHÁCH HÀNG & HỘI VIÊN VIP (14 CƠ THỦ) + TỦ CƠ
            var tiers = context.MembershipTiers.OrderBy(m => m.Id).ToList();
            if (tiers.Count >= 4)
            {
                var extraCustomers = new (string Code, string Name, string Phone, int TierIdx, int Points, decimal Balance, int DaysAgo)[]
                {
                    ("KH_004", "Phạm Trần Bạch Long", "0982870986", 3, 1420, 2100000m, 45),
                    ("KH_005", "Hoàng Minh Tuấn", "0908776655", 2, 680, 750000m, 38),
                    ("KH_006", "Nguyễn Đình Trọng", "0933123456", 2, 590, 620000m, 34),
                    ("KH_007", "Nguyễn Văn Năm (Chú Năm)", "0903445667", 3, 1680, 1800000m, 60),
                    ("KH_008", "Trương Đăng Khoa", "0913456780", 2, 740, 900000m, 29),
                    ("KH_009", "Bùi Đức Thắng", "0971554321", 1, 310, 350000m, 24),
                    ("KH_010", "Đỗ Hải Đăng", "0934567890", 1, 245, 280000m, 20),
                    ("KH_011", "Vũ Đình Khang", "0917890123", 1, 195, 150000m, 16),
                    ("KH_012", "Trần Quang Huy", "0962333888", 0, 85, 100000m, 12),
                    ("KH_013", "Lê Thanh Sơn", "0907888999", 0, 60, 50000m, 8),
                    ("KH_014", "Ngô Trí Dũng", "0939000111", 2, 820, 1100000m, 40)
                };
                foreach (var c in extraCustomers)
                {
                    if (!context.Customers.Any(x => x.Code == c.Code))
                    {
                        context.Customers.Add(new Customer
                        {
                            MembershipTierId = tiers[c.TierIdx].Id,
                            Code = c.Code,
                            FullName = c.Name,
                            PhoneNumber = c.Phone,
                            AccumulatedPoints = c.Points,
                            AccountBalance = c.Balance,
                            CreatedAt = DateTime.Now.AddDays(-c.DaysAgo)
                        });
                    }
                }
                context.SaveChanges();
            }

            // Bổ sung tủ cơ & cơ gửi
            var allCustomers = context.Customers.OrderBy(c => c.Id).ToList();
            var extraLockers = new (string Num, string Area, string Status, decimal Rent)[]
            {
                ("TU_03", "Khu Tủ Tầng 1", "Đang thuê", 150000m),
                ("TU_04", "Khu Tủ Tầng 2", "Đang thuê", 150000m),
                ("TU_VIP_02", "Phòng VIP Tầng 2", "Trống", 200000m)
            };
            foreach (var lk in extraLockers)
            {
                if (!context.Lockers.Any(x => x.LockerNumber == lk.Num))
                {
                    context.Lockers.Add(new Locker { LockerNumber = lk.Num, Area = lk.Area, Status = lk.Status, MonthlyRent = lk.Rent });
                }
            }
            context.SaveChanges();

            if (context.CustomerCues.Count() < 4 && allCustomers.Count >= 5)
            {
                var extraCues = new (string Code, int CustIdx, string Brand, string Shaft)[]
                {
                    ("GAY_002", 2, "Cuetec Cynergy Truewood", "Ngọn Carbon 12.5mm"),
                    ("GAY_003", 3, "Fury Tempest Series", "Ngọn Maple Pro Taper"),
                    ("GAY_004", 4, "Mezz EC9-N", "Ngọn Ignite 12.2 Carbon")
                };
                foreach (var cue in extraCues)
                {
                    if (!context.CustomerCues.Any(x => x.Code == cue.Code))
                    {
                        context.CustomerCues.Add(new CustomerCue
                        {
                            CustomerId = allCustomers[cue.CustIdx].Id,
                            Code = cue.Code,
                            CueBrand = cue.Brand,
                            ShaftType = cue.Shaft,
                            ReceivedDate = DateTime.Today.AddDays(-10),
                            Status = "Đang lưu kho"
                        });
                    }
                }
                context.SaveChanges();
            }

            // 6. BỔ SUNG HÓA ĐƠN (16 HÓA ĐƠN THẬT), CHI TIẾT MÓN & THANH TOÁN (ĐỒNG BỘ 100% HÓA ĐƠN <-> DOANH THU)
            var allTables = context.BilliardTables.OrderBy(t => t.Id).ToList();
            var allUsers = context.Users.OrderBy(u => u.Id).ToList();
            var payMethods = context.PaymentMethods.OrderBy(p => p.Id).ToList();
            var cashierUsers = allUsers.Where(u => u.Username.Contains("cashier") || u.Username == "manager" || u.Username == "admin").ToList();
            if (!cashierUsers.Any()) cashierUsers = allUsers;

            if (context.Invoices.Count() < 12 && allTables.Count >= 5 && allItems.Count >= 14 && payMethods.Count >= 2)
            {
                var today = DateTime.Today;
                var invoiceSpecs = new (string Code, int DayOffset, int StartHour, int DurationMins, int TableIdx, int CustIdx, int UserIdx, int PayIdx, decimal TableAmt, decimal DiscountAmt, string Status, (int ItemIdx, int Qty)[] Lines)[]
                {
                    // Hôm nay (10 hóa đơn hoàn tất + 2 đang chơi)
                    ("HD_LAB5_T01", 0, 8, 120, 0, 0, 0, 1, 120000m, 15000m, "Đã thanh toán", new[] { (0, 2), (6, 1) }),
                    ("HD_LAB5_T02", 0, 9, 150, 1, 1, 1, 0, 150000m, 0m, "Đã thanh toán", new[] { (1, 2), (2, 2), (9, 1) }),
                    ("HD_LAB5_T03", 0, 10, 135, 4, 2, 0, 1, 180000m, 25000m, "Đã thanh toán", new[] { (11, 4), (10, 1) }),
                    ("HD_LAB5_T04", 0, 11, 120, 2, 3, 2, 1, 120000m, 10000m, "Đã thanh toán", new[] { (0, 1), (3, 2), (7, 1) }),
                    ("HD_LAB5_T05", 0, 12, 180, 3, 4, 1, 0, 150000m, 0m, "Đã thanh toán", new[] { (12, 3), (8, 2) }),
                    ("HD_LAB5_T06", 0, 13, 150, 5, 5, 2, 1, 150000m, 15000m, "Đã thanh toán", new[] { (2, 3), (6, 2) }),
                    ("HD_LAB5_T07", 0, 14, 165, 4, 6, 0, 2, 220000m, 30000m, "Đã thanh toán", new[] { (11, 6), (10, 2), (17, 1) }),
                    ("HD_LAB5_T08", 0, 15, 120, 6, 7, 1, 1, 120000m, 0m, "Đã thanh toán", new[] { (1, 3), (9, 2) }),
                    ("HD_LAB5_T09", 0, 16, 150, 7, 8, 2, 0, 125000m, 10000m, "Đã thanh toán", new[] { (12, 4), (7, 1) }),
                    ("HD_LAB5_T10", 0, 17, 120, 8, 9, 0, 1, 100000m, 0m, "Đã thanh toán", new[] { (0, 2), (4, 2), (18, 1) }),
                    // Hôm qua (6 hóa đơn hoàn tất để biểu đồ so sánh Hôm nay vs Hôm qua có dữ liệu thật!)
                    ("HD_LAB5_Y01", -1, 9, 120, 0, 1, 1, 0, 120000m, 0m, "Đã thanh toán", new[] { (0, 2), (2, 2) }),
                    ("HD_LAB5_Y02", -1, 11, 150, 4, 0, 0, 1, 200000m, 20000m, "Đã thanh toán", new[] { (11, 5), (10, 1) }),
                    ("HD_LAB5_Y03", -1, 14, 180, 1, 3, 2, 1, 180000m, 15000m, "Đã thanh toán", new[] { (1, 2), (8, 2), (9, 1) }),
                    ("HD_LAB5_Y04", -1, 16, 120, 2, 4, 1, 0, 120000m, 0m, "Đã thanh toán", new[] { (12, 4), (6, 1) }),
                    ("HD_LAB5_Y05", -1, 19, 150, 4, 6, 2, 1, 200000m, 25000m, "Đã thanh toán", new[] { (13, 4), (10, 2) }),
                    ("HD_LAB5_Y06", -1, 20, 120, 3, 2, 0, 2, 100000m, 0m, "Đã thanh toán", new[] { (2, 4), (7, 2) })
                };

                foreach (var spec in invoiceSpecs)
                {
                    if (!context.Invoices.Any(i => i.InvoiceCode == spec.Code))
                    {
                        decimal svcAmt = 0m;
                        foreach (var l in spec.Lines)
                        {
                            if (l.ItemIdx < allItems.Count)
                                svcAmt += allItems[l.ItemIdx].UnitPrice * l.Qty;
                        }
                        decimal totalPay = Math.Max(0, spec.TableAmt + svcAmt - spec.DiscountAmt);
                        var startDt = today.AddDays(spec.DayOffset).AddHours(spec.StartHour);
                        var endDt = startDt.AddMinutes(spec.DurationMins);

                        var inv = new Invoice
                        {
                            BilliardTableId = allTables[spec.TableIdx % allTables.Count].Id,
                            UserId = cashierUsers[spec.UserIdx % cashierUsers.Count].Id,
                            CustomerId = allCustomers.Any() ? allCustomers[spec.CustIdx % allCustomers.Count].Id : 1,
                            InvoiceCode = spec.Code,
                            StartTime = startDt,
                            EndTime = spec.Status == "Đã thanh toán" ? endDt : null,
                            TableAmount = spec.TableAmt,
                            ServiceAmount = svcAmt,
                            DiscountAmount = spec.DiscountAmt,
                            TotalPayment = totalPay,
                            Status = spec.Status
                        };
                        context.Invoices.Add(inv);
                        context.SaveChanges();

                        foreach (var l in spec.Lines)
                        {
                            if (l.ItemIdx < allItems.Count)
                            {
                                var item = allItems[l.ItemIdx];
                                context.InvoiceDetails.Add(new InvoiceDetail
                                {
                                    InvoiceId = inv.Id,
                                    ServiceItemId = item.Id,
                                    Quantity = l.Qty,
                                    UnitPrice = item.UnitPrice,
                                    TotalAmount = item.UnitPrice * l.Qty
                                });
                            }
                        }

                        if (spec.Status == "Đã thanh toán")
                        {
                            context.InvoicePayments.Add(new InvoicePayment
                            {
                                InvoiceId = inv.Id,
                                PaymentMethodId = payMethods[spec.PayIdx % payMethods.Count].Id,
                                Amount = totalPay,
                                PaymentTime = endDt
                            });
                        }
                        context.SaveChanges();
                    }
                }
            }

            // 7. BỔ SUNG PHIẾU CHI (PAYMENTVOUCHERS) ĐỒNG BỘ BÁO CÁO TÀI CHÍNH
            var expTypes = context.ExpenseTypes.OrderBy(e => e.Id).ToList();
            if (expTypes.Count >= 3 && context.PaymentVouchers.Count() < 4)
            {
                var vouchers = new (string Code, int ExpIdx, decimal Amount, int DaysAgo, string Purpose, string Note)[]
                {
                    ("PC_202611_002", 1, 680000m, 5, "Thanh toán hóa đơn nước sinh hoạt CLB tháng 11", "Chuyển khoản định kỳ"),
                    ("PC_202611_003", 2, 1250000m, 3, "Bảo trì đèn LED & thay nỉ góc Bàn 04, Bàn 09", "Thợ kỹ thuật Hùng Bida"),
                    ("PC_202611_004", 0, 420000m, 0, "Mua đá viên tinh khiết & vật tư vệ sinh quầy Bar", "Chi tiền mặt két ca sáng")
                };
                foreach (var v in vouchers)
                {
                    if (!context.PaymentVouchers.Any(p => p.Code == v.Code))
                    {
                        context.PaymentVouchers.Add(new PaymentVoucher
                        {
                            ExpenseTypeId = expTypes[v.ExpIdx % expTypes.Count].Id,
                            UserId = adminUser.Id,
                            Code = v.Code,
                            Amount = v.Amount,
                            CreatedAt = DateTime.Today.AddDays(-v.DaysAgo).AddHours(10),
                            ExpensePurpose = v.Purpose,
                            Notes = v.Note
                        });
                    }
                }
                context.SaveChanges();
            }

            // 8. BỔ SUNG NHẬT KÝ HỆ THỐNG (ACTIVITYLOGS) CHO TAB AUDIT TRAIL TRONG /Staff
            if (context.ActivityLogs.Count() < 8)
            {
                var extraLogs = new (int UserIdx, string Action, string Table, string Desc, int HoursAgo)[]
                {
                    (0, "IMPORT_STOCK", "GoodsReceipts", "Nhập kho phiếu PN_202611_005 từ Tổng Kho F&B Miền Nam", 18),
                    (1, "UPDATE_MENU", "ServiceItems", "Cập nhật giá bán & đồng bộ tồn kho 23 món Thực đơn F&B", 12),
                    (2, "PAY_INVOICE", "Invoices", "Thanh toán hóa đơn HD_LAB5_T03 qua VietQR (Bàn VIP 01)", 5),
                    (0, "CREATE_STAFF", "Users", "Phân quyền vai trò Cashier & Waiter cho đội ngũ ca chiều tối", 4),
                    (1, "AUDIT_STOCK", "Inventories", "Kiểm kê kho phát hiện 4 mặt hàng dưới định mức an toàn", 2),
                    (2, "PAY_INVOICE", "Invoices", "Thanh toán hóa đơn HD_LAB5_T07 qua Quẹt thẻ POS", 1)
                };
                foreach (var log in extraLogs)
                {
                    if (!context.ActivityLogs.Any(a => a.Description == log.Desc))
                    {
                        context.ActivityLogs.Add(new ActivityLog
                        {
                            UserId = allUsers[log.UserIdx % allUsers.Count].Id,
                            Action = log.Action,
                            TableName = log.Table,
                            RecordId = 1,
                            Description = log.Desc,
                            IpAddress = "192.168.1." + (10 + log.HoursAgo),
                            CreatedAt = DateTime.Now.AddHours(-log.HoursAgo)
                        });
                    }
                }
                context.SaveChanges();
            }

            // 9. CHUẨN HÓA ĐỒNG NHẤT MÃ CHỨNG TỪ TOÀN HỆ THỐNG (KHÔNG DÙNG TIỀN TỐ LAB)
            // Hóa đơn: HD-2611-001.. | Phiếu nhập: PN-2611-001.. | Phiếu chi: PC-2611-001..
            var allInvsOrder = context.Invoices.OrderBy(i => i.Id).ToList();
            bool codeChanged = false;
            int invSeq = 1;
            foreach (var inv in allInvsOrder)
            {
                if (inv.InvoiceCode.Contains("LAB5", StringComparison.OrdinalIgnoreCase) || inv.InvoiceCode.StartsWith("HD_2026", StringComparison.OrdinalIgnoreCase))
                {
                    inv.InvoiceCode = $"HD-2611-{invSeq:000}";
                    codeChanged = true;
                }
                invSeq++;
            }

            var allReceiptsOrder = context.GoodsReceipts.OrderBy(r => r.Id).ToList();
            int pnSeq = 1;
            foreach (var gr in allReceiptsOrder)
            {
                if (gr.Code.StartsWith("PN_2026", StringComparison.OrdinalIgnoreCase))
                {
                    gr.Code = $"PN-2611-{pnSeq:000}";
                    codeChanged = true;
                }
                pnSeq++;
            }

            var allVouchersOrder = context.PaymentVouchers.OrderBy(v => v.Id).ToList();
            int pcSeq = 1;
            foreach (var pv in allVouchersOrder)
            {
                if (pv.Code.StartsWith("PC_2026", StringComparison.OrdinalIgnoreCase))
                {
                    pv.Code = $"PC-2611-{pcSeq:000}";
                    codeChanged = true;
                }
                pcSeq++;
            }

            var dirtyLogs = context.ActivityLogs.Where(l => l.Description != null && (l.Description.Contains("LAB5") || l.Description.Contains("PN_202611_"))).ToList();
            foreach (var lg in dirtyLogs)
            {
                lg.Description = (lg.Description ?? "")
                    .Replace("HD_LAB5_T03", "HD-2611-005")
                    .Replace("HD_LAB5_T07", "HD-2611-009")
                    .Replace("PN_202611_005", "PN-2611-005");
                codeChanged = true;
            }

            if (codeChanged) context.SaveChanges();

            // 10. ĐỒNG BỘ LOẠI BÀN, 2 KHUNG GIỜ GIÁ CHO MỖI LOẠI BÀN (KHỚP 100% QUẢNG CÁO WEB KHÁCH) & THIẾT BỊ ĐỦ 20 BÀN
            var activeTypes = context.TableTypes.OrderBy(t => t.Id).ToList();
            if (activeTypes.Count >= 3)
            {
                // Chuẩn hóa tên 3 loại bàn chuẩn đang vận hành (Pool 9-Ball, Carom Phăng, Phòng VIP 3 Băng)
                if (activeTypes[0].Name != "Bàn Pool 9-Ball (Bida Lỗ)")
                {
                    activeTypes[0].Name = "Bàn Pool 9-Ball (Bida Lỗ)";
                    activeTypes[0].BasePrice = 59000m;
                }
                if (activeTypes[1].Name != "Bàn Carom (Bida Phăng)")
                {
                    activeTypes[1].Name = "Bàn Carom (Bida Phăng)";
                    activeTypes[1].BasePrice = 65000m;
                }
                if (activeTypes[2].Name != "Phòng VIP 3 Băng Đẳng Cấp")
                {
                    activeTypes[2].Name = "Phòng VIP 3 Băng Đẳng Cấp";
                    activeTypes[2].BasePrice = 119000m;
                }
                context.SaveChanges();

                // Đảm bảo mỗi loại bàn có ĐÚNG 2 khung giờ (Khung Ngày 08:00-17:00 & Khung Tối 17:00-08:00) khớp 100% Web Khách
                var activeTypeIds = new[] { activeTypes[0].Id, activeTypes[1].Id, activeTypes[2].Id };
                var currentSlots = context.TimeSlotPrices.ToList();
                bool needResetSlots = currentSlots.Count != 6 || activeTypeIds.Any(tid => currentSlots.Count(s => s.TableTypeId == tid) != 2);
                if (needResetSlots)
                {
                    if (currentSlots.Any()) context.TimeSlotPrices.RemoveRange(currentSlots);
                    context.TimeSlotPrices.AddRange(new TimeSlotPrice[]
                    {
                        // Pool 9-Ball: 59k (08:00-17:00) & 69k (17:00-08:00)
                        new() { TableTypeId = activeTypes[0].Id, SlotStartTime = new TimeSpan(8, 0, 0), SlotEndTime = new TimeSpan(17, 0, 0), SlotPrice = 59000m },
                        new() { TableTypeId = activeTypes[0].Id, SlotStartTime = new TimeSpan(17, 0, 0), SlotEndTime = new TimeSpan(8, 0, 0), SlotPrice = 69000m },
                        // Carom Phăng: 65k (08:00-17:00) & 75k (17:00-08:00)
                        new() { TableTypeId = activeTypes[1].Id, SlotStartTime = new TimeSpan(8, 0, 0), SlotEndTime = new TimeSpan(17, 0, 0), SlotPrice = 65000m },
                        new() { TableTypeId = activeTypes[1].Id, SlotStartTime = new TimeSpan(17, 0, 0), SlotEndTime = new TimeSpan(8, 0, 0), SlotPrice = 75000m },
                        // Phòng VIP 3 Băng: 119k (08:00-17:00) & 119k (17:00-08:00)
                        new() { TableTypeId = activeTypes[2].Id, SlotStartTime = new TimeSpan(8, 0, 0), SlotEndTime = new TimeSpan(17, 0, 0), SlotPrice = 119000m },
                        new() { TableTypeId = activeTypes[2].Id, SlotStartTime = new TimeSpan(17, 0, 0), SlotEndTime = new TimeSpan(8, 0, 0), SlotPrice = 119000m }
                    });
                    context.SaveChanges();
                }
            }

            // Đảm bảo ĐỦ 20/20 BÀN đều có bản ghi thiết bị đi kèm (TableEquipments)
            var all20Tables = context.BilliardTables.OrderBy(t => t.Id).ToList();
            var equippedTableIds = context.TableEquipments.Select(e => e.BilliardTableId).ToHashSet();
            bool eqAdded = false;
            foreach (var tbl in all20Tables)
            {
                if (!equippedTableIds.Contains(tbl.Id))
                {
                    bool isVip = tbl.Name.Contains("VIP", StringComparison.OrdinalIgnoreCase);
                    bool isCarom = tbl.Name.Contains("Carom", StringComparison.OrdinalIgnoreCase);
                    context.TableEquipments.Add(new TableEquipment
                    {
                        BilliardTableId = tbl.Id,
                        Name = isVip
                            ? "Bộ bi Aramith 3C Pro Cup + Đèn LED chao đôi + Băng sưởi nhiệt"
                            : isCarom
                                ? "Bộ bi Carom Aramith Super Pro + Đèn LED chống lóa 3 chao"
                                : "Bộ bi Pool Aramith Pro Cup + Đèn LED 3 chao + Tam giác gỗ sồi",
                        Quantity = 1,
                        Status = tbl.Status == "Bảo trì" ? "Đang bảo trì" : "Hoạt động tốt"
                    });
                    eqAdded = true;
                }
            }
            if (eqAdded) context.SaveChanges();

            // 11. ĐỒNG BỘ ĐỦ 156 HỘI VIÊN & KHÁCH HÀNG VIP TRONG SQL SERVER (14 KIM CƯƠNG, 38 VÀNG, 42 BẠC, 62 ĐỒNG)
            var memberTiers = context.MembershipTiers.OrderBy(m => m.MinPoints).ToList();
            if (memberTiers.Count >= 4 && context.Customers.Count() < 156)
            {
                int bronzeId = memberTiers[0].Id;
                int silverId = memberTiers[1].Id;
                int goldId = memberTiers[2].Id;
                int diamondId = memberTiers[3].Id;

                int curDia = context.Customers.Count(c => c.MembershipTierId == diamondId);
                int curGold = context.Customers.Count(c => c.MembershipTierId == goldId);
                int curSilv = context.Customers.Count(c => c.MembershipTierId == silverId);
                int curBron = context.Customers.Count(c => c.MembershipTierId == bronzeId);

                var existingCodes = context.Customers.Select(c => c.Code).ToHashSet();
                int seqCode = context.Customers.Count() + 1;

                string NextCustomerCode(string prefix)
                {
                    string code;
                    do { code = $"{prefix}-{seqCode++:000}"; } while (existingCodes.Contains(code));
                    existingCodes.Add(code);
                    return code;
                }

                string[] lastNames = { "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Võ", "Đặng", "Bùi", "Đỗ", "Hồ", "Ngô", "Dương" };
                string[] midNames = { "Văn", "Minh", "Quốc", "Đình", "Anh", "Gia", "Thanh", "Hữu", "Đức", "Hoàng", "Tuấn", "Tiến" };
                string[] firstNames = { "Long", "Nam", "Dũng", "Hùng", "Tuấn", "Khang", "Huy", "Bảo", "Trọng", "Đạt", "Cường", "Phúc", "Quân", "Sơn", "Tùng", "Lộc", "Thắng", "Toàn" };

                string GenName(int idx) => $"{lastNames[idx % lastNames.Length]} {midNames[(idx * 3) % midNames.Length]} {firstNames[(idx * 7) % firstNames.Length]}";

                for (int i = curDia; i < 14; i++)
                {
                    context.Customers.Add(new Customer
                    {
                        MembershipTierId = diamondId,
                        Code = NextCustomerCode("VIP"),
                        FullName = GenName(i + 1),
                        PhoneNumber = $"090{(10 + i):00}{(20000 + i * 137):00000}",
                        AccumulatedPoints = 1100 + (14 - i) * 180,
                        AccountBalance = 1200000m + (14 - i) * 150000m,
                        CreatedAt = DateTime.Now.AddDays(-(60 + i * 3))
                    });
                }
                for (int i = curGold; i < 38; i++)
                {
                    context.Customers.Add(new Customer
                    {
                        MembershipTierId = goldId,
                        Code = NextCustomerCode("VIP"),
                        FullName = GenName(i + 25),
                        PhoneNumber = $"091{(10 + i):00}{(30000 + i * 193):00000}",
                        AccumulatedPoints = 520 + (38 - i) * 14,
                        AccountBalance = 450000m + (38 - i) * 30000m,
                        CreatedAt = DateTime.Now.AddDays(-(40 + (i % 35)))
                    });
                }
                for (int i = curSilv; i < 42; i++)
                {
                    context.Customers.Add(new Customer
                    {
                        MembershipTierId = silverId,
                        Code = NextCustomerCode("VIP"),
                        FullName = GenName(i + 75),
                        PhoneNumber = $"093{(10 + i):00}{(40000 + i * 151):00000}",
                        AccumulatedPoints = 120 + (42 - i) * 8,
                        AccountBalance = 150000m + (42 - i) * 15000m,
                        CreatedAt = DateTime.Now.AddDays(-(25 + (i % 30)))
                    });
                }
                for (int i = curBron; i < 62; i++)
                {
                    context.Customers.Add(new Customer
                    {
                        MembershipTierId = bronzeId,
                        Code = NextCustomerCode("STD"),
                        FullName = GenName(i + 130),
                        PhoneNumber = $"097{(10 + i):00}{(50000 + i * 113):00000}",
                        AccumulatedPoints = 15 + (62 - i) * 1,
                        AccountBalance = 50000m + (i % 5) * 20000m,
                        CreatedAt = DateTime.Now.AddDays(-(5 + (i % 25)))
                    });
                }
                context.SaveChanges();
            }

            // 12. MỞ RỘNG HỆ THỐNG TỦ ĐỒ CÁ NHÂN ĐỦ 200 TỦ (PHÂN TRANG 30 TỦ / TRANG)
            int curLockerCount = context.Lockers.Count();
            if (curLockerCount < 200)
            {
                var existingLockerNums = context.Lockers.Select(l => l.LockerNumber).ToHashSet();
                int toAdd = 200 - curLockerCount;
                int numSeq = 1;
                for (int i = 0; i < toAdd; i++)
                {
                    string area;
                    decimal rent;
                    string prefix;
                    int totalIdx = curLockerCount + i;
                    if (totalIdx < 100)
                    {
                        area = "Khu Tủ Tầng 1";
                        rent = 150000m;
                        prefix = "TU";
                    }
                    else if (totalIdx < 160)
                    {
                        area = "Khu Tủ Tầng 2";
                        rent = 150000m;
                        prefix = "TU";
                    }
                    else
                    {
                        area = "Phòng VIP Tầng 2";
                        rent = 200000m;
                        prefix = "TU-VIP";
                    }

                    string lockerNo;
                    do { lockerNo = $"{prefix}-{numSeq++:000}"; } while (existingLockerNums.Contains(lockerNo));
                    existingLockerNums.Add(lockerNo);

                    // Phân bổ thực tế khoảng 15 tủ đang thuê, còn lại trống sẵn sàng cho khách gửi cơ/đồ
                    string st = (i < 12) ? "Đang thuê" : "Trống";
                    context.Lockers.Add(new Locker
                    {
                        LockerNumber = lockerNo,
                        Area = area,
                        Status = st,
                        MonthlyRent = rent
                    });
                }
                context.SaveChanges();
            }
        }

        // HÀM BĂM MẬT KHẨU SHA256 (Khắc phục góp ý thầy chấm về mật khẩu thô)
        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}

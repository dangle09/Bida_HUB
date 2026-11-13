using System;
using System.Linq;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuanLyQuanBidaContext context)
        {
            context.Database.EnsureCreated();

            // Kiểm tra nếu đã có dữ liệu thì không nạp lại
            if (context.Roles.Any())
            {
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

            // 7. Categories (Đồng bộ 100% với Web vãng lai)
            var categories = new Category[]
            {
                new() { Code = "DRINK", Name = "Nước Giải Khát & Cà Phê", Image = "images/fnb/fnb-1-caphe-muoi.png" },
                new() { Code = "FOOD", Name = "Ẩm Thực F&B & Đồ Ăn Nhanh", Image = "images/fnb/fnb-4-mi-xuc-xich.jpg" },
                new() { Code = "BEER", Name = "Bia Các Loại Ướp Lạnh", Image = "images/fnb/fnb-9-heineken.jpg" },
                new() { Code = "CUES", Name = "Cơ Bida & Ngọn Carbon", Image = "images/shop/shop-1-co-fury.jpg" },
                new() { Code = "ACCESSORIES", Name = "Dụng Cụ & Phụ Kiện Cơ Thủ", Image = "images/shop/shop-2-gang-tay.jpg" }
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
                new() { Username = "admin", PasswordHash = HashPassword("giangcc111"), FullName = "Đào Ngọc Giang (Admin)", Email = "giangadmin@bms.com", PhoneNumber = "0988001122", IsActive = true, CreatedAt = DateTime.Now },
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
                new() { MembershipTierId = membershipTiers[2].Id, Code = "KH_001", FullName = "Đào Ngọc Giang", PhoneNumber = "0988112233", AccumulatedPoints = 550, AccountBalance = 500000, CreatedAt = DateTime.Now },
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

                // Nhóm 4: Cơ Bida & Ngọn Carbon
                new() { CategoryId = categories[3].Id, Code = "SP_CO_FURY", Name = "Cơ Bida Fury Carbon Pro", Unit = "Cây", UnitPrice = 2850000, Image = "images/shop/shop-1-co-fury.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_CO_AILEEX", Name = "Cơ Bida Lỗ Aileex Carbon", Unit = "Cây", UnitPrice = 1650000, Image = "images/shop/shop-1-co-fury.jpg", IsActive = true },
                new() { CategoryId = categories[3].Id, Code = "SP_KHUC_NOI", Name = "Khúc Nối Cơ Carbon Extension", Unit = "Cây", UnitPrice = 220000, Image = "images/shop/shop-5-khuc-noi.jpg", IsActive = true },

                // Nhóm 5: Dụng Cụ & Phụ Kiện Cơ Thủ
                new() { CategoryId = categories[4].Id, Code = "SP_GANG_TAY", Name = "Găng Tay Bolt Co Giãn", Unit = "Cái", UnitPrice = 65000, Image = "images/shop/shop-2-gang-tay.jpg", IsActive = true },
                new() { CategoryId = categories[4].Id, Code = "SP_LO_MASTER", Name = "Lơ Bida Master USA (Hộp)", Unit = "Hộp", UnitPrice = 40000, Image = "images/shop/shop-3-lo-master.jpg", IsActive = true },
                new() { CategoryId = categories[4].Id, Code = "SP_BAO_DA", Name = "Bao Da Đựng Cơ 1 Gốc 2 Ngọn", Unit = "Bao", UnitPrice = 350000, Image = "images/shop/shop-4-bao-da.jpg", IsActive = true },
                new() { CategoryId = categories[4].Id, Code = "SP_DAU_TAY", Name = "Đầu Tẩy Kamui Nhật Bản", Unit = "Hộp", UnitPrice = 180000, Image = "images/shop/shop-6-dau-tay-kamui.jpg", IsActive = true },
                new() { CategoryId = categories[4].Id, Code = "SP_KHAN_LAU", Name = "Khăn Lau Cơ Chuyên Dụng", Unit = "Cái", UnitPrice = 30000, Image = "images/shop/shop-7-khan-lau.jpg", IsActive = true },
                new() { CategoryId = categories[4].Id, Code = "SP_BIT_REN", Name = "Nắp Bịt Ren Bảo Vệ Khớp Nối", Unit = "Bộ", UnitPrice = 80000, Image = "images/shop/shop-8-bit-ren.jpg", IsActive = true }
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

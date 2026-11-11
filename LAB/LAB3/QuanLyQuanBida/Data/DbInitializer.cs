using System;
using System.Linq;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuanLyQuanBidaContext context)
        {
            // Đảm bảo Database đã được tạo
            context.Database.EnsureCreated();

            // Kiểm tra nếu đã có dữ liệu thì không nạp lại (tránh trùng lặp)
            if (context.VaiTros.Any())
            {
                return;
            }

            // ==========================================
            // GIAI ĐOẠN 1: CÁC BẢNG DANH MỤC ĐỘC LẬP
            // ==========================================

            // 1. Vai trò
            var vaiTros = new VaiTro[]
            {
                new() { Ten = "Admin", MoTa = "Quản trị viên toàn quyền hệ thống" },
                new() { Ten = "QuanLy", MoTa = "Quản lý điều hành cơ sở CLB Bida" },
                new() { Ten = "ThuNgan", MoTa = "Nhân viên thu ngân bán hàng POS" },
                new() { Ten = "PhucVu", MoTa = "Nhân viên phục vụ xếp bi, chạy bàn" }
            };
            context.VaiTros.AddRange(vaiTros);

            // 2. Quyền hạn
            var quyenHans = new QuyenHan[]
            {
                new() { MaCode = "XEM_BAO_CAO", Ten = "Xem báo cáo thống kê doanh thu", PhanHe = "BAO_CAO" },
                new() { MaCode = "BAN_HANG_POS", Ten = "Mở bàn, order món và thanh toán", PhanHe = "BAN_HANG" },
                new() { MaCode = "HUY_HOA_DON", Ten = "Hủy hóa đơn đã xuất bill", PhanHe = "BAN_HANG" },
                new() { MaCode = "NHAP_KHO", Ten = "Lập và phê duyệt phiếu nhập hàng", PhanHe = "KHO_HANG" },
                new() { MaCode = "QUAN_LY_NHAN_SU", Ten = "Xếp ca và tính lương nhân viên", PhanHe = "NHAN_SU" }
            };
            context.QuyenHans.AddRange(quyenHans);

            // 3. Ca làm
            var caLams = new CaLam[]
            {
                new() { Ten = "Ca Sáng", GioBatDau = new TimeSpan(8, 0, 0), GioKetThuc = new TimeSpan(16, 0, 0), LuongTheoGio = 25000 },
                new() { Ten = "Ca Chiều Tối", GioBatDau = new TimeSpan(16, 0, 0), GioKetThuc = new TimeSpan(23, 59, 59), LuongTheoGio = 28000 },
                new() { Ten = "Ca Đêm", GioBatDau = new TimeSpan(0, 0, 0), GioKetThuc = new TimeSpan(8, 0, 0), LuongTheoGio = 32000 }
            };
            context.CaLams.AddRange(caLams);

            // 4. Khu vực bàn
            var khuVucBans = new KhuVucBan[]
            {
                new() { MaCode = "TANG_1", Ten = "Tầng 1 - Khu Bàn Lỗ Tiêu Chuẩn" },
                new() { MaCode = "TANG_2", Ten = "Tầng 2 - Khu Bida Phăng Carom" },
                new() { MaCode = "VIP_ROOM", Ten = "Phòng VIP 3 Băng Máy Lạnh Cao Cấp" }
            };
            context.KhuVucBans.AddRange(khuVucBans);

            // 5. Loại bàn
            var loaiBans = new LoaiBan[]
            {
                new() { Ten = "Bida Lỗ (Pool 9 Feet)", GiaCoBan = 60000 },
                new() { Ten = "Bida Phăng (Carom Libre)", GiaCoBan = 50000 },
                new() { Ten = "Bida 3 Băng (3-Cushion Hàn Quốc)", GiaCoBan = 80000 },
                new() { Ten = "Snooker Quốc Tế", GiaCoBan = 100000 }
            };
            context.LoaiBans.AddRange(loaiBans);

            // 6. Hạng thành viên
            var hangThanhViens = new HangThanhVien[]
            {
                new() { Ten = "Hội viên Đồng", DiemToiThieu = 0, TiLeGiamGia = 0, MoTa = "Hạng tiêu chuẩn khi mới đăng ký" },
                new() { Ten = "Hội viên Bạc", DiemToiThieu = 100, TiLeGiamGia = 5, MoTa = "Tích lũy từ 100 điểm, giảm 5% giờ chơi" },
                new() { Ten = "Hội viên Vàng", DiemToiThieu = 500, TiLeGiamGia = 10, MoTa = "Tích lũy từ 500 điểm, giảm 10% giờ chơi" },
                new() { Ten = "Hội viên Kim Cương", DiemToiThieu = 1000, TiLeGiamGia = 15, MoTa = "VIP đặc quyền, giảm 15% tổng hóa đơn" }
            };
            context.HangThanhViens.AddRange(hangThanhViens);

            // 7. Danh mục món
            var danhMucMons = new DanhMucMon[]
            {
                new() { MaCode = "NUOC_NGOT", Ten = "Nước Giải Khát & Đóng Lon" },
                new() { MaCode = "BIA", Ten = "Bia Các Loại" },
                new() { MaCode = "AN_VAT", Ten = "Đồ Ăn Nhanh & Mì Ăn Liền" },
                new() { MaCode = "THUOC_LA", Ten = "Thuốc Lá & Khăn Lạnh" }
            };
            context.DanhMucMons.AddRange(danhMucMons);

            // 8. Nhà cung cấp
            var nhaCungCaps = new NhaCungCap[]
            {
                new() { Ten = "Công ty TNHH Nước Giải Khát Suntory PepsiCo", SoDienThoai = "02838234567", DiaChi = "Quận 1, TP. Hồ Chí Minh" },
                new() { Ten = "Đại lý Phân Phối Bia Nước Ngọt Minh Châu", SoDienThoai = "0903112233", DiaChi = "Đống Đa, Hà Nội" },
                new() { Ten = "Xưởng Phụ Kiện & Thiết Bị Bida Thịnh Kent", SoDienThoai = "0988776655", DiaChi = "Bình Thạnh, TP. Hồ Chí Minh" }
            };
            context.NhaCungCaps.AddRange(nhaCungCaps);

            // 9. Khuyến mãi
            var khuyenMais = new KhuyenMai[]
            {
                new() { MaCode = "KHAI_TRUONG", Ten = "Khuyến mãi Khai trương Giảm 20% Giờ chơi", PhanTramGiam = 20, SoTienGiam = 0, NgayBatDau = new DateTime(2026, 11, 1), NgayHetHan = new DateTime(2026, 11, 30) },
                new() { MaCode = "HAPPY_HOUR", Ten = "Giờ Vàng Giảm Giá Buổi Sáng", PhanTramGiam = 10, SoTienGiam = 0, NgayBatDau = new DateTime(2026, 11, 1), NgayHetHan = new DateTime(2026, 12, 31) },
                new() { MaCode = "VOUCHER_50K", Ten = "Giảm 50K cho hóa đơn trên 300K", PhanTramGiam = 0, SoTienGiam = 50000, NgayBatDau = new DateTime(2026, 11, 1), NgayHetHan = new DateTime(2026, 11, 15) }
            };
            context.KhuyenMais.AddRange(khuyenMais);

            // 10. Phương thức thanh toán
            var phuongThucs = new PhuongThucThanhToan[]
            {
                new() { Ten = "Tiền mặt (Cash)", DangHoatDong = true },
                new() { Ten = "Chuyển khoản VietQR", DangHoatDong = true },
                new() { Ten = "Quẹt thẻ ngân hàng POS", DangHoatDong = true }
            };
            context.PhuongThucThanhToans.AddRange(phuongThucs);

            // 11. Tủ đồ
            var tuDos = new TuDo[]
            {
                new() { SoTu = "TU_01", KhuVuc = "Khu Tủ Tầng 1", TrangThai = "Đang thuê", TienThueThang = 150000 },
                new() { SoTu = "TU_02", KhuVuc = "Khu Tủ Tầng 1", TrangThai = "Trống", TienThueThang = 150000 },
                new() { SoTu = "TU_VIP_01", KhuVuc = "Phòng VIP Tầng 2", TrangThai = "Đang thuê", TienThueThang = 200000 }
            };
            context.TuDos.AddRange(tuDos);

            // 12. Giải đấu
            var giaiDaus = new GiaiDau[]
            {
                new() { TenGiai = "Giải Bida Lỗ Open CLB Mùa Đông 2026", NgayBatDau = new DateTime(2026, 12, 1), NgayKetThuc = new DateTime(2026, 12, 5), LePhiThamGia = 200000, TongGiaiThuong = 10000000, TrangThai = "Đang mở đăng ký" },
                new() { TenGiai = "Giải Bida Phăng Giao Lưu Nội Bộ", NgayBatDau = new DateTime(2026, 11, 20), NgayKetThuc = new DateTime(2026, 11, 21), LePhiThamGia = 100000, TongGiaiThuong = 3000000, TrangThai = "Sắp diễn ra" }
            };
            context.GiaiDaus.AddRange(giaiDaus);

            // 13. Loại chi phí
            var loaiChiPhis = new LoaiChiPhi[]
            {
                new() { Ten = "Tiền điện sinh hoạt và Máy lạnh", MoTa = "Hóa đơn điện lực hàng tháng" },
                new() { Ten = "Tiền nước sinh hoạt", MoTa = "Hóa đơn nước máy" },
                new() { Ten = "Chi phí bảo trì, thay nỉ bàn bida", MoTa = "Căng nỉ mới định kỳ" }
            };
            context.LoaiChiPhis.AddRange(loaiChiPhis);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 2: BẢNG PHỤ THUỘC CẤP 1
            // ==========================================

            // 14. Quyền - Vai trò
            var quyenVaiTros = new QuyenVaiTro[]
            {
                new() { VaiTroId = vaiTros[0].Id, QuyenHanId = quyenHans[0].Id },
                new() { VaiTroId = vaiTros[0].Id, QuyenHanId = quyenHans[1].Id },
                new() { VaiTroId = vaiTros[0].Id, QuyenHanId = quyenHans[2].Id },
                new() { VaiTroId = vaiTros[0].Id, QuyenHanId = quyenHans[3].Id },
                new() { VaiTroId = vaiTros[0].Id, QuyenHanId = quyenHans[4].Id },
                new() { VaiTroId = vaiTros[1].Id, QuyenHanId = quyenHans[0].Id },
                new() { VaiTroId = vaiTros[1].Id, QuyenHanId = quyenHans[1].Id },
                new() { VaiTroId = vaiTros[1].Id, QuyenHanId = quyenHans[3].Id },
                new() { VaiTroId = vaiTros[2].Id, QuyenHanId = quyenHans[1].Id }
            };
            context.QuyenVaiTros.AddRange(quyenVaiTros);

            // 15. Người dùng
            var nguoiDungs = new NguoiDung[]
            {
                new() { TenDangNhap = "admin", MatKhauHash = "giangcc111", HoTen = "Đào Ngọc Giang (Admin)", Email = "giangadmin@bms.com", SoDienThoai = "0988001122", DangHoatDong = true, NgayTao = DateTime.Now },
                new() { TenDangNhap = "thungan_mai", MatKhauHash = "dangcc111", HoTen = "Trần Thị Mai", Email = "maitt@bms.com", SoDienThoai = "0912334455", DangHoatDong = true, NgayTao = DateTime.Now },
                new() { TenDangNhap = "phucvu_tuan", MatKhauHash = "khoacc111", HoTen = "Lê Tuấn Vũ", Email = "vult@bms.com", SoDienThoai = "0977889900", DangHoatDong = true, NgayTao = DateTime.Now }
            };
            context.NguoiDungs.AddRange(nguoiDungs);

            // 16. Giá bàn khung giờ
            var giaBanKhungGios = new GiaBanKhungGio[]
            {
                new() { LoaiBanId = loaiBans[0].Id, GioBatDauKg = new TimeSpan(8, 0, 0), GioKetThucKg = new TimeSpan(14, 0, 0), GiaKhungGio = 45000 },
                new() { LoaiBanId = loaiBans[0].Id, GioBatDauKg = new TimeSpan(18, 0, 0), GioKetThucKg = new TimeSpan(23, 0, 0), GiaKhungGio = 60000 },
                new() { LoaiBanId = loaiBans[2].Id, GioBatDauKg = new TimeSpan(8, 0, 0), GioKetThucKg = new TimeSpan(14, 0, 0), GiaKhungGio = 65000 }
            };
            context.GiaBanKhungGios.AddRange(giaBanKhungGios);

            // 17. Bàn bida
            var banBidas = new BanBida[]
            {
                new() { KhuVucBanId = khuVucBans[0].Id, LoaiBanId = loaiBans[0].Id, MaCode = "BAN_01", Ten = "Bàn 01 (Lỗ)", TrangThai = "Đang chơi" },
                new() { KhuVucBanId = khuVucBans[0].Id, LoaiBanId = loaiBans[0].Id, MaCode = "BAN_02", Ten = "Bàn 02 (Lỗ)", TrangThai = "Trống" },
                new() { KhuVucBanId = khuVucBans[0].Id, LoaiBanId = loaiBans[0].Id, MaCode = "BAN_03", Ten = "Bàn 03 (Lỗ)", TrangThai = "Đã đặt trước" },
                new() { KhuVucBanId = khuVucBans[1].Id, LoaiBanId = loaiBans[1].Id, MaCode = "BAN_04", Ten = "Bàn 04 (Phăng)", TrangThai = "Trống" },
                new() { KhuVucBanId = khuVucBans[2].Id, LoaiBanId = loaiBans[2].Id, MaCode = "BAN_VIP_01", Ten = "Bàn VIP 01 (3 Băng)", TrangThai = "Đang chơi" }
            };
            context.BanBidas.AddRange(banBidas);

            // 18. Khách hàng
            var khachHangs = new KhachHang[]
            {
                new() { HangThanhVienId = hangThanhViens[2].Id, MaCode = "KH_001", HoTen = "Đào Ngọc Giang", SoDienThoai = "0988112233", DiemTichLuy = 550, AccountBalance = 500000, NgayTao = DateTime.Now },
                new() { HangThanhVienId = hangThanhViens[1].Id, MaCode = "KH_002", HoTen = "Nguyễn Hoàng Nam", SoDienThoai = "0912345678", DiemTichLuy = 180, AccountBalance = 200000, NgayTao = DateTime.Now },
                new() { HangThanhVienId = hangThanhViens[3].Id, MaCode = "KH_003", HoTen = "Trần Quốc Bảo", SoDienThoai = "0909998877", DiemTichLuy = 1250, AccountBalance = 1500000, NgayTao = DateTime.Now }
            };
            context.KhachHangs.AddRange(khachHangs);

            // 19. Món dịch vụ
            var monDichVus = new MonDichVu[]
            {
                new() { DanhMucId = danhMucMons[0].Id, MaCode = "SP_REDBULL", Ten = "Nước tăng lực RedBull Thái", DonViTinh = "Lon", DonGia = 20000, DangHoatDong = true },
                new() { DanhMucId = danhMucMons[0].Id, MaCode = "SP_C2", Ten = "Trà xanh C2 hương chanh", DonViTinh = "Chai", DonGia = 15000, DangHoatDong = true },
                new() { DanhMucId = danhMucMons[0].Id, MaCode = "SP_SUOI", Ten = "Nước khoáng Aquafina 500ml", DonViTinh = "Chai", DonGia = 10000, DangHoatDong = true },
                new() { DanhMucId = danhMucMons[1].Id, MaCode = "SP_TIGER", Ten = "Bia Tiger Bạc 330ml", DonViTinh = "Lon", DonGia = 25000, DangHoatDong = true },
                new() { DanhMucId = danhMucMons[2].Id, MaCode = "SP_MI_TRUNG", Ten = "Mì tôm 2 trứng xúc xích", DonViTinh = "Tô", DonGia = 35000, DangHoatDong = true },
                new() { DanhMucId = danhMucMons[3].Id, MaCode = "SP_555", Ten = "Thuốc lá State Express 555", DonViTinh = "Gói", DonGia = 40000, DangHoatDong = true }
            };
            context.MonDichVus.AddRange(monDichVus);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 3: BẢNG PHỤ THUỘC CẤP 2
            // ==========================================

            // 20. Vai trò - Người dùng
            var vaiTroNguoiDungs = new VaiTroNguoiDung[]
            {
                new() { NguoiDungId = nguoiDungs[0].Id, VaiTroId = vaiTros[0].Id, NgayTao = DateTime.Now },
                new() { NguoiDungId = nguoiDungs[1].Id, VaiTroId = vaiTros[2].Id, NgayTao = DateTime.Now },
                new() { NguoiDungId = nguoiDungs[2].Id, VaiTroId = vaiTros[3].Id, NgayTao = DateTime.Now }
            };
            context.VaiTroNguoiDungs.AddRange(vaiTroNguoiDungs);

            // 21. Nhật ký hoạt động
            var nhatKyHoatDongs = new NhatKyHoatDong[]
            {
                new() { NguoiDungId = nguoiDungs[0].Id, HanhDong = "DANG_NHAP", BangDuLieu = "nguoi_dung", BanGhiId = nguoiDungs[0].Id, MoTa = "Quản trị viên đăng nhập vào hệ thống", DiaChiIp = "192.168.1.10", ThoiGian = DateTime.Now },
                new() { NguoiDungId = nguoiDungs[1].Id, HanhDong = "MO_BAN", BangDuLieu = "hoa_don", BanGhiId = 1, MoTa = "Mở Bàn 01 tính giờ cho khách Đào Ngọc Giang", DiaChiIp = "192.168.1.15", ThoiGian = DateTime.Now }
            };
            context.NhatKyHoatDongs.AddRange(nhatKyHoatDongs);

            // 22. Lịch làm việc
            var lichLamViecs = new LichLamViec[]
            {
                new() { NguoiDungId = nguoiDungs[1].Id, CaLamId = caLams[0].Id, NgayLamViec = new DateTime(2026, 11, 10), GhiChu = "Trực thu ngân quầy chính" },
                new() { NguoiDungId = nguoiDungs[2].Id, CaLamId = caLams[0].Id, NgayLamViec = new DateTime(2026, 11, 10), GhiChu = "Phục vụ tầng 1" }
            };
            context.LichLamViecs.AddRange(lichLamViecs);

            // 23. Chấm công
            var chamCongs = new ChamCong[]
            {
                new() { NguoiDungId = nguoiDungs[1].Id, NgayLamViec = new DateTime(2026, 11, 10), GioVao = new DateTime(2026, 11, 10, 7, 55, 0), GioRa = new DateTime(2026, 11, 10, 16, 5, 0), SoGioLam = 8.0m, DiTrePhut = 0, VeSomPhut = 0 },
                new() { NguoiDungId = nguoiDungs[2].Id, NgayLamViec = new DateTime(2026, 11, 10), GioVao = new DateTime(2026, 11, 10, 8, 5, 0), GioRa = new DateTime(2026, 11, 10, 16, 0, 0), SoGioLam = 7.9m, DiTrePhut = 5, VeSomPhut = 0 }
            };
            context.ChamCongs.AddRange(chamCongs);

            // 24. Bảng lương
            var bangLuongs = new BangLuong[]
            {
                new() { NguoiDungId = nguoiDungs[1].Id, ThangLuong = 10, NamLuong = 2026, TongGioLam = 208, LuongCoBan = 5824000, PhuCap = 500000, TienThuong = 300000, TienPhat = 0, ThucLinh = 6624000 },
                new() { NguoiDungId = nguoiDungs[2].Id, ThangLuong = 10, NamLuong = 2026, TongGioLam = 200, LuongCoBan = 5000000, PhuCap = 400000, TienThuong = 200000, TienPhat = 50000, ThucLinh = 5550000 }
            };
            context.BangLuongs.AddRange(bangLuongs);

            // 25. Phiếu kiểm kho
            var phieuKiemKhos = new PhieuKiemKho[]
            {
                new() { NguoiDungId = nguoiDungs[0].Id, MaCode = "KK_20261101", NgayKiemKe = new DateTime(2026, 11, 1), GhiChu = "Kiểm kê đầu tháng 11 toàn bộ đồ uống F&B" }
            };
            context.PhieuKiemKhos.AddRange(phieuKiemKhos);

            // 26. Đặt bàn
            var datBans = new DatBan[]
            {
                new() { KhachHangId = khachHangs[1].Id, BanBidaId = banBidas[2].Id, ThoiGianDen = new DateTime(2026, 11, 10, 19, 30, 0), SoNguoi = 4, TienCoc = 100000, TrangThai = "Đã đặt cọc" }
            };
            context.DatBans.AddRange(datBans);

            // 27. Thiết bị bàn
            var thietBiBans = new ThietBiBan[]
            {
                new() { BanBidaId = banBidas[0].Id, Ten = "Bộ bi bida lỗ Aramith Pro Cup", SoLuong = 1, TrangThai = "Rất tốt" },
                new() { BanBidaId = banBidas[0].Id, Ten = "Đèn chiếu sáng 3 chao LED", SoLuong = 1, TrangThai = "Hoạt động tốt" },
                new() { BanBidaId = banBidas[0].Id, Ten = "Tam giác xếp bi gỗ sồi", SoLuong = 1, TrangThai = "Tốt" }
            };
            context.ThietBiBans.AddRange(thietBiBans);

            // 28. Bảo trì bàn
            var baoTriBans = new BaoTriBan[]
            {
                new() { BanBidaId = banBidas[0].Id, LoaiBaoTri = "Thay vải nỉ mặt bàn Simonis 860", ChiPhi = 1800000, NgayBaoTri = new DateTime(2026, 10, 15), NguoiThucHien = "Thợ Hùng Bida", GhiChu = "Nỉ cao cấp thi đấu" }
            };
            context.BaoTriBans.AddRange(baoTriBans);

            // 29. Giao dịch điểm
            var giaoDichDiems = new GiaoDichDiem[]
            {
                new() { KhachHangId = khachHangs[0].Id, LoaiGiaoDich = "CONG_DIEM", SoDiem = 50, NgayTao = DateTime.Now, GhiChu = "Tích điểm từ hóa đơn HD-0001" },
                new() { KhachHangId = khachHangs[0].Id, LoaiGiaoDich = "TRU_DIEM", SoDiem = 100, NgayTao = DateTime.Now, GhiChu = "Đổi 1 giờ chơi bida miễn phí" }
            };
            context.GiaoDichDiems.AddRange(giaoDichDiems);

            // 30. Thuê tủ đồ
            var thueTuDos = new ThueTuDo[]
            {
                new() { TuDoId = tuDos[0].Id, KhachHangId = khachHangs[0].Id, NgayBatDau = new DateTime(2026, 11, 1), NgayHetHan = new DateTime(2026, 12, 1), TienThueThang = 150000, TienCoc = 100000, TrangThai = "Đang thuê" }
            };
            context.ThueTuDos.AddRange(thueTuDos);

            // 31. Gậy khách gửi
            var gayKhachGuis = new GayKhachGui[]
            {
                new() { KhachHangId = khachHangs[0].Id, MaCode = "GAY_001", ThuongHieuGay = "Predator P3 Cobalt", LoaiNgonCo = "Ngọn Revo 12.4 Carbon", NgayGui = new DateTime(2026, 11, 1), TrangThai = "Đang lưu kho" }
            };
            context.GayKhachGuis.AddRange(gayKhachGuis);

            // 32. Cơ thủ tham gia giải
            var coThuThamGias = new CoThuThamGia[]
            {
                new() { GiaiDauId = giaiDaus[0].Id, KhachHangId = khachHangs[0].Id, SoHatGiong = 1, NgayDangKy = new DateTime(2026, 11, 5, 10, 0, 0) },
                new() { GiaiDauId = giaiDaus[0].Id, KhachHangId = khachHangs[1].Id, SoHatGiong = 4, NgayDangKy = new DateTime(2026, 11, 6, 14, 30, 0) },
                new() { GiaiDauId = giaiDaus[0].Id, KhachHangId = khachHangs[2].Id, SoHatGiong = 2, NgayDangKy = new DateTime(2026, 11, 7, 9, 15, 0) }
            };
            context.CoThuThamGias.AddRange(coThuThamGias);

            // 33. Trận đấu
            var tranDaus = new TranDau[]
            {
                new() { GiaiDauId = giaiDaus[0].Id, BanBidaId = banBidas[0].Id, TenVongDau = "Vòng Bán Kết 1", CoThu1Id = khachHangs[0].Id, CoThu2Id = khachHangs[1].Id, TiSoCoThu1 = 9, TiSoCoThu2 = 5, ThoiGianDau = new DateTime(2026, 12, 4, 14, 0, 0), TrangThai = "Đã kết thúc" }
            };
            context.TranDaus.AddRange(tranDaus);

            // 34. Phiếu chi
            var phieuChis = new PhieuChi[]
            {
                new() { LoaiChiPhiId = loaiChiPhis[0].Id, NguoiDungId = nguoiDungs[0].Id, MaCode = "PC_20261101", SoTien = 4500000, NgayTao = DateTime.Now, MucDichChi = "Thanh toán tiền điện tháng 10 CLB", GhiChu = "Chuyển khoản EVN" }
            };
            context.PhieuChis.AddRange(phieuChis);

            // 35. Tồn kho
            var tonKhos = new TonKho[]
            {
                new() { MonDichVuId = monDichVus[0].Id, SoLuongTon = 120, MucCanhBao = 24 },
                new() { MonDichVuId = monDichVus[1].Id, SoLuongTon = 95, MucCanhBao = 24 },
                new() { MonDichVuId = monDichVus[2].Id, SoLuongTon = 200, MucCanhBao = 48 },
                new() { MonDichVuId = monDichVus[3].Id, SoLuongTon = 150, MucCanhBao = 48 },
                new() { MonDichVuId = monDichVus[4].Id, SoLuongTon = 50, MucCanhBao = 10 },
                new() { MonDichVuId = monDichVus[5].Id, SoLuongTon = 40, MucCanhBao = 10 }
            };
            context.TonKhos.AddRange(tonKhos);

            // 36. Phiếu nhập kho
            var phieuNhapKhos = new PhieuNhapKho[]
            {
                new() { NhaCungCapId = nhaCungCaps[0].Id, NguoiDungId = nguoiDungs[0].Id, MaCode = "PN_20261102", NgayNhap = new DateTime(2026, 11, 2, 9, 0, 0), TongTienNhap = 3600000 }
            };
            context.PhieuNhapKhos.AddRange(phieuNhapKhos);

            // 37. Hóa đơn
            var hoaDons = new HoaDon[]
            {
                new() { BanBidaId = banBidas[0].Id, NguoiDungId = nguoiDungs[1].Id, KhachHangId = khachHangs[0].Id, KhuyenMaiId = khuyenMais[0].Id, MaHoaDon = "HD_20261110_01", GioBatDau = new DateTime(2026, 11, 10, 13, 0, 0), GioKetThuc = new DateTime(2026, 11, 10, 14, 30, 0), TablePrice = 90000, ServicePrice = 75000, TienGiamGia = 18000, TongThanhToan = 147000, TrangThai = "Đã thanh toán" },
                new() { BanBidaId = banBidas[4].Id, NguoiDungId = nguoiDungs[1].Id, KhachHangId = khachHangs[2].Id, KhuyenMaiId = null, MaHoaDon = "HD_20261110_02", GioBatDau = new DateTime(2026, 11, 10, 14, 0, 0), GioKetThuc = null, TablePrice = 80000, ServicePrice = 40000, TienGiamGia = 0, TongThanhToan = 120000, TrangThai = "Đang chơi" }
            };
            context.HoaDons.AddRange(hoaDons);

            context.SaveChanges();

            // ==========================================
            // GIAI ĐOẠN 4: BẢNG PHỤ THUỘC CẤP 3
            // ==========================================

            // 38. Chi tiết nhập kho
            var chiTietNhapKhos = new ChiTietNhapKho[]
            {
                new() { PhieuNhapId = phieuNhapKhos[0].Id, MonDichVuId = monDichVus[0].Id, SoLuong = 100, GiaNhap = 14000 },
                new() { PhieuNhapId = phieuNhapKhos[0].Id, MonDichVuId = monDichVus[1].Id, SoLuong = 100, GiaNhap = 9000 }
            };
            context.ChiTietNhapKhos.AddRange(chiTietNhapKhos);

            // 39. Chi tiết hóa đơn
            var chiTietHoaDons = new ChiTietHoaDon[]
            {
                new() { HoaDonId = hoaDons[0].Id, MonDichVuId = monDichVus[0].Id, SoLuong = 2, DonGia = 20000, ThanhTien = 40000 },
                new() { HoaDonId = hoaDons[0].Id, MonDichVuId = monDichVus[4].Id, SoLuong = 1, DonGia = 35000, ThanhTien = 35000 }
            };
            context.ChiTietHoaDons.AddRange(chiTietHoaDons);

            // 40. Chuyển bàn
            var chuyenBans = new ChuyenBan[]
            {
                new() { HoaDonId = hoaDons[0].Id, TuBanId = banBidas[0].Id, DenBanId = banBidas[4].Id, GioChuyenBan = new DateTime(2026, 11, 10, 13, 45, 0), GhiChu = "Khách đổi từ Bàn Lỗ 01 sang Bàn VIP 3 Băng" }
            };
            context.ChuyenBans.AddRange(chuyenBans);

            // 41. Thanh toán hóa đơn
            var thanhToanHoaDons = new ThanhToanHoaDon[]
            {
                new() { HoaDonId = hoaDons[0].Id, PhuongThucId = phuongThucs[1].Id, SoTien = 147000, ThoiGianTt = new DateTime(2026, 11, 10, 14, 30, 0) }
            };
            context.ThanhToanHoaDons.AddRange(thanhToanHoaDons);

            context.SaveChanges();
        }
    }
}

USE [QuanLyQuanBida];
GO

INSERT INTO vai_tro (ten, mo_ta) VALUES
('Admin', N'Quản trị viên toàn quyền hệ thống'),
('QuanLy', N'Quản lý điều hành cơ sở CLB Bida'),
('ThuNgan', N'Nhân viên thu ngân bán hàng POS'),
('PhucVu', N'Nhân viên phục vụ xếp bi, chạy bàn');

INSERT INTO quyen_han (ma_code, ten, phan_he) VALUES
('XEM_BAO_CAO', N'Xem báo cáo thống kê doanh thu', 'BAO_CAO'),
('BAN_HANG_POS', N'Mở bàn, order món và thanh toán', 'BAN_HANG'),
('HUY_HOA_DON', N'Hủy hóa đơn đã xuất bill', 'BAN_HANG'),
('NHAP_KHO', N'Lập và phê duyệt phiếu nhập hàng', 'KHO_HANG'),
('QUAN_LY_NHAN_SU', N'Xếp ca và tính lương nhân viên', 'NHAN_SU');

INSERT INTO ca_lam (ten, gio_bat_dau, gio_ket_thuc, luong_theo_gio) VALUES
(N'Ca Sáng', '08:00:00', '16:00:00', 25000),
(N'Ca Chiều Tối', '16:00:00', '23:59:59', 28000),
(N'Ca Đêm', '00:00:00', '08:00:00', 32000);

INSERT INTO khu_vuc_ban (ma_code, ten) VALUES
('TANG_1', N'Tầng 1 - Khu Bàn Lỗ Tiêu Chuẩn'),
('TANG_2', N'Tầng 2 - Khu Bida Phăng Carom'),
('VIP_ROOM', N'Phòng VIP 3 Băng Máy Lạnh Cao Cấp');

INSERT INTO loai_ban (ten, gia_co_ban) VALUES
(N'Bida Lỗ (Pool 9 Feet)', 60000),
(N'Bida Phăng (Carom Libre)', 50000),
(N'Bida 3 Băng (3-Cushion Hàn Quốc)', 80000),
(N'Snooker Quốc Tế', 100000);

INSERT INTO hang_thanh_vien (ten, diem_toi_thieu, ti_le_giam_gia, mo_ta) VALUES
(N'Hội viên Đồng', 0, 0, N'Hạng tiêu chuẩn khi mới đăng ký'),
(N'Hội viên Bạc', 100, 5, N'Tích lũy từ 100 điểm, giảm 5% giờ chơi'),
(N'Hội viên Vàng', 500, 10, N'Tích lũy từ 500 điểm, giảm 10% giờ chơi'),
(N'Hội viên Kim Cương', 1000, 15, N'VIP đặc quyền, giảm 15% tổng hóa đơn');

INSERT INTO danh_muc_mon (ma_code, ten) VALUES
('NUOC_NGOT', N'Nước Giải Khát & Đóng Lon'),
('BIA', N'Bia Các Loại'),
('AN_VAT', N'Đồ Ăn Nhanh & Mì Ăn Liền'),
('THUOC_LA', N'Thuốc Lá & Khăn Lạnh');

INSERT INTO nha_cung_cap (ten, so_dien_thoai, dia_chi) VALUES
(N'Công ty TNHH Nước Giải Khát Suntory PepsiCo', '02838234567', N'Quận 1, TP. Hồ Chí Minh'),
(N'Đại lý Phân Phối Bia Nước Ngọt Minh Châu', '0903112233', N'Đống Đa, Hà Nội'),
(N'Xưởng Phụ Kiện & Thiết Bị Bida Thịnh Kent', '0988776655', N'Bình Thạnh, TP. Hồ Chí Minh');

INSERT INTO khuyen_mai (ma_code, ten, phan_tram_giam, so_tien_giam, ngay_bat_dau, ngay_het_han) VALUES
('KHAI_TRUONG', N'Khuyến mãi Khai trương Giảm 20% Giờ chơi', 20, 0, '2026-11-01', '2026-11-30'),
('HAPPY_HOUR', N'Giờ Vàng Giảm Giá Buổi Sáng', 10, 0, '2026-11-01', '2026-12-31'),
('VOUCHER_50K', N'Giảm 50K cho hóa đơn trên 300K', 0, 50000, '2026-11-01', '2026-11-15');

INSERT INTO phuong_thuc_thanh_toan (ten, dang_hoat_dong) VALUES
(N'Tiền mặt (Cash)', 1),
(N'Chuyển khoản VietQR', 1),
(N'Quẹt thẻ ngân hàng POS', 1);

INSERT INTO tu_do (so_tu, khu_vuc, trang_thai, tien_thue_thang) VALUES
('TU_01', N'Khu Tủ Tầng 1', N'Đang thuê', 150000),
('TU_02', N'Khu Tủ Tầng 1', N'Trống', 150000),
('TU_VIP_01', N'Phòng VIP Tầng 2', N'Đang thuê', 200000);

INSERT INTO giai_dau (ten_giai, ngay_bat_dau, ngay_ket_thuc, le_phi_tham_gia, tong_giai_thuong, trang_thai) VALUES
(N'Giải Bida Lỗ Open CLB Mùa Đông 2026', '2026-12-01', '2026-12-05', 200000, 10000000, N'Đang mở đăng ký'),
(N'Giải Bida Phăng Giao Lưu Nội Bộ', '2026-11-20', '2026-11-21', 100000, 3000000, N'Sắp diễn ra');

INSERT INTO loai_chi_phi (ten, mo_ta) VALUES
(N'Tiền điện sinh hoạt và Máy lạnh', N'Hóa đơn điện lực hàng tháng'),
(N'Tiền nước sinh hoạt', N'Hóa đơn nước máy'),
(N'Chi phí bảo trì, thay nỉ bàn bida', N'Căng nỉ mới định kỳ');

INSERT INTO quyen_vai_tro (vai_tro_id, quyen_han_id) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), -- Admin có đủ quyền
(2, 1), (2, 2), (2, 4), -- Quản lý
(3, 2); -- Thu ngân bán hàng

INSERT INTO nguoi_dung (ten_dang_nhap, mat_khau_hash, ho_ten, email, so_dien_thoai, dang_hoat_dong) VALUES
('admin', 'giangcc111', N'Đào Ngọc Giang (Admin)', 'giangadmin@bms.com', '0988001122', 1),
('thungan_mai', 'dangcc111', N'Trần Thị Mai', 'maitt@bms.com', '0912334455', 1),
('phucvu_tuan', 'khoacc111', N'Lê Tuấn Vũ', 'vult@bms.com', '0977889900', 1);

INSERT INTO gia_ban_khung_gio (loai_ban_id, gio_bat_dau_kg, gio_ket_thuc_kg, gia_khung_gio) VALUES
(1, '08:00:00', '14:00:00', 45000), -- Bida Lỗ giá sáng
(1, '18:00:00', '23:00:00', 60000), -- Bida Lỗ giờ cao điểm
(3, '08:00:00', '14:00:00', 65000); -- 3 Băng giá sáng

INSERT INTO ban_bida (khu_vuc_ban_id, loai_ban_id, ma_code, ten, trang_thai) VALUES
(1, 1, 'BAN_01', N'Bàn 01 (Lỗ)', N'Đang chơi'),
(1, 1, 'BAN_02', N'Bàn 02 (Lỗ)', N'Trống'),
(1, 1, 'BAN_03', N'Bàn 03 (Lỗ)', N'Đã đặt trước'),
(2, 2, 'BAN_04', N'Bàn 04 (Phăng)', N'Trống'),
(3, 3, 'BAN_VIP_01', N'Bàn VIP 01 (3 Băng)', N'Đang chơi');

INSERT INTO khach_hang (hang_thanh_vien_id, ma_code, ho_ten, so_dien_thoai, diem_tich_luy, account_balance) VALUES
(3, 'KH_001', N'Đào Ngọc Giang', '0988112233', 550, 500000),
(2, 'KH_002', N'Nguyễn Hoàng Nam', '0912345678', 180, 200000),
(4, 'KH_003', N'Trần Quốc Bảo', '0909998877', 1250, 1500000);

INSERT INTO mon_dich_vu (danh_muc_id, ma_code, ten, don_vi_tinh, don_gia, dang_hoat_dong) VALUES
(1, 'SP_REDBULL', N'Nước tăng lực RedBull Thái', N'Lon', 20000, 1),
(1, 'SP_C2', N'Trà xanh C2 hương chanh', N'Chai', 15000, 1),
(1, 'SP_SUOI', N'Nước khoáng Aquafina 500ml', N'Chai', 10000, 1),
(2, 'SP_TIGER', N'Bia Tiger Bạc 330ml', N'Lon', 25000, 1),
(3, 'SP_MI_TRUNG', N'Mì tôm 2 trứng xúc xích', N'Tô', 35000, 1),
(4, 'SP_555', N'Thuốc lá State Express 555', N'Gói', 40000, 1);

INSERT INTO vai_tro_nguoi_dung (nguoi_dung_id, vai_tro_id) VALUES
(1, 1), -- Admin
(2, 3), -- Thu ngân
(3, 4); -- Phục vụ

INSERT INTO nhat_ky_hoat_dong (nguoi_dung_id, hanh_dong, bang_du_lieu, ban_ghi_id, mo_ta, dia_chi_ip) VALUES
(1, 'DANG_NHAP', 'nguoi_dung', 1, N'Quản trị viên đăng nhập vào hệ thống', '192.168.1.10'),
(2, 'MO_BAN', 'hoa_don', 1, N'Mở Bàn 01 tính giờ cho khách Đào Ngọc Giang', '192.168.1.15');

INSERT INTO lich_lam_viec (nguoi_dung_id, ca_lam_id, ngay_lam_viec, ghi_chu) VALUES
(2, 1, '2026-11-10', N'Trực thu ngân quầy chính'),
(3, 1, '2026-11-10', N'Phục vụ tầng 1');

INSERT INTO cham_cong (nguoi_dung_id, ngay_lam_viec, gio_vao, gio_ra, so_gio_lam, di_tre_phut, ve_som_phut) VALUES
(2, '2026-11-10', '2026-11-10 07:55:00', '2026-11-10 16:05:00', 8.0, 0, 0),
(3, '2026-11-10', '2026-11-10 08:05:00', '2026-11-10 16:00:00', 7.9, 5, 0);

INSERT INTO bang_luong (nguoi_dung_id, thang_luong, nam_luong, tong_gio_lam, luong_co_ban, phu_cap, tien_thuong, tien_phat, thuc_linh) VALUES
(2, 10, 2026, 208, 5824000, 500000, 300000, 0, 6624000),
(3, 10, 2026, 200, 5000000, 400000, 200000, 50000, 5550000);

INSERT INTO phieu_kiem_kho (nguoi_dung_id, ma_code, ngay_kiem_ke, ghi_chu) VALUES
(1, 'KK_20261101', '2026-11-01', N'Kiểm kê đầu tháng 11 toàn bộ đồ uống F&B');

INSERT INTO dat_ban (khach_hang_id, ban_bida_id, thoi_gian_den, so_nguoi, tien_coc, trang_thai) VALUES
(2, 3, '2026-11-10 19:30:00', 4, 100000, N'Đã đặt cọc');

INSERT INTO thiet_bi_ban (ban_bida_id, ten, so_luong, trang_thai) VALUES
(1, N'Bộ bi bida lỗ Aramith Pro Cup', 1, N'Rất tốt'),
(1, N'Đèn chiếu sáng 3 chao LED', 1, N'Hoạt động tốt'),
(1, N'Tam giác xếp bi gỗ sồi', 1, N'Tốt');

INSERT INTO bao_tri_ban (ban_bida_id, loai_bao_tri, chi_phi, ngay_bao_tri, nguoi_thuc_hien, ghi_chu) VALUES
(1, N'Thay vải nỉ mặt bàn Simonis 860', 1800000, '2026-10-15', N'Thợ Hùng Bida', N'Nỉ cao cấp thi đấu');

INSERT INTO giao_dich_diem (khach_hang_id, loai_giao_dich, so_diem, ghi_chu) VALUES
(1, 'CONG_DIEM', 50, N'Tích điểm từ hóa đơn HD-0001'),
(1, 'TRU_DIEM', 100, N'Đổi 1 giờ chơi bida miễn phí');

INSERT INTO thue_tu_do (tu_do_id, khach_hang_id, ngay_bat_dau, ngay_het_han, tien_thue_thang, tien_coc, trang_thai) VALUES
(1, 1, '2026-11-01', '2026-12-01', 150000, 100000, N'Đang thuê');

INSERT INTO gay_khach_gui (khach_hang_id, ma_code, thuong_hieu_gay, loai_ngon_co, ngay_gui, trang_thai) VALUES
(1, 'GAY_001', N'Predator P3 Cobalt', N'Ngọn Revo 12.4 Carbon', '2026-11-01', N'Đang lưu kho');

INSERT INTO co_thu_tham_gia (giai_dau_id, khach_hang_id, so_hat_giong, ngay_dang_ky) VALUES
(1, 1, 1, '2026-11-05 10:00:00'),
(1, 2, 4, '2026-11-06 14:30:00'),
(1, 3, 2, '2026-11-07 09:15:00');

INSERT INTO tran_dau (giai_dau_id, ban_bida_id, ten_vong_dau, co_thu_1_id, co_thu_2_id, ti_so_co_thu_1, ti_so_co_thu_2, thoi_gian_dau, trang_thai) VALUES
(1, 1, N'Vòng Bán Kết 1', 1, 2, 9, 5, '2026-12-04 14:00:00', N'Đã kết thúc');

INSERT INTO phieu_chi (loai_chi_phi_id, nguoi_dung_id, ma_code, so_tien, muc_dich_chi, ghi_chu) VALUES
(1, 1, 'PC_20261101', 4500000, N'Thanh toán tiền điện tháng 10 CLB', N'Chuyển khoản EVN');

INSERT INTO ton_kho (mon_dich_vu_id, so_luong_ton, muc_canh_bao) VALUES
(1, 120, 24),
(2, 95, 24),
(3, 200, 48),
(4, 150, 48),
(5, 50, 10),
(6, 40, 10);

INSERT INTO phieu_nhap_kho (nha_cung_cap_id, nguoi_dung_id, ma_code, ngay_nhap, tong_tien_nhap) VALUES
(1, 1, 'PN_20261102', '2026-11-02 09:00:00', 3600000);

INSERT INTO hoa_don (ban_bida_id, nguoi_dung_id, khach_hang_id, khuyen_mai_id, ma_hoa_don, gio_bat_dau, gio_ket_thuc, table_price, service_price, tien_giam_gia, tong_thanh_toan, trang_thai) VALUES
(1, 2, 1, 1, 'HD_20261110_01', '2026-11-10 13:00:00', '2026-11-10 14:30:00', 90000, 75000, 18000, 147000, N'Đã thanh toán'),
(5, 2, 3, NULL, 'HD_20261110_02', '2026-11-10 14:00:00', NULL, 80000, 40000, 0, 120000, N'Đang chơi');

INSERT INTO chi_tiet_nhap_kho (phieu_nhap_id, mon_dich_vu_id, so_luong, gia_nhap) VALUES
(1, 1, 100, 14000), -- 100 lon RedBull x 14.000đ
(1, 2, 100, 9000);  -- 100 chai C2 x 9.000đ

INSERT INTO chi_tiet_hoa_don (hoa_don_id, mon_dich_vu_id, so_luong, don_gia, thanh_tien) VALUES
(1, 1, 2, 20000, 40000), -- 2 RedBull = 40.000đ
(1, 5, 1, 35000, 35000); -- 1 Mì tôm = 35.000đ

INSERT INTO chuyen_ban (hoa_don_id, tu_ban_id, den_ban_id, gio_chuyen_ban, ghi_chu) VALUES
(1, 1, 5, '2026-11-10 13:45:00', N'Khách đổi từ Bàn Lỗ 01 sang Bàn VIP 3 Băng');

INSERT INTO thanh_toan_hoa_don (hoa_don_id, phuong_thuc_id, so_tien, thoi_gian_tt) VALUES
(1, 2, 147000, '2026-11-10 14:30:00'); -- Thanh toán chuyển khoản VietQR 147.000đ

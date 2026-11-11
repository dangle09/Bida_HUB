IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [ban_bida] (
    [id] int NOT NULL IDENTITY,
    [khu_vuc_ban_id] int NOT NULL,
    [loai_ban_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [ten] nvarchar(50) NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_ban_bida] PRIMARY KEY ([id])
);

CREATE TABLE [bang_luong] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [thang_luong] int NOT NULL,
    [nam_luong] int NOT NULL,
    [tong_gio_lam] decimal(6,2) NOT NULL,
    [luong_co_ban] decimal(18,2) NOT NULL,
    [phu_cap] decimal(18,2) NOT NULL,
    [tien_thuong] decimal(18,2) NOT NULL,
    [tien_phat] decimal(18,2) NOT NULL,
    [thuc_linh] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_bang_luong] PRIMARY KEY ([id])
);

CREATE TABLE [bao_tri_ban] (
    [id] int NOT NULL IDENTITY,
    [ban_bida_id] int NOT NULL,
    [loai_bao_tri] nvarchar(100) NOT NULL,
    [chi_phi] decimal(18,2) NOT NULL,
    [ngay_bao_tri] datetime2 NOT NULL,
    [nguoi_thuc_hien] nvarchar(100) NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_bao_tri_ban] PRIMARY KEY ([id])
);

CREATE TABLE [ca_lam] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(50) NOT NULL,
    [gio_bat_dau] time NOT NULL,
    [gio_ket_thuc] time NOT NULL,
    [luong_theo_gio] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ca_lam] PRIMARY KEY ([id])
);

CREATE TABLE [cham_cong] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [ngay_lam_viec] datetime2 NOT NULL,
    [gio_vao] datetime2 NOT NULL,
    [gio_ra] datetime2 NULL,
    [so_gio_lam] decimal(5,2) NULL,
    [di_tre_phut] int NULL,
    [ve_som_phut] int NULL,
    CONSTRAINT [PK_cham_cong] PRIMARY KEY ([id])
);

CREATE TABLE [chi_tiet_hoa_don] (
    [id] int NOT NULL IDENTITY,
    [hoa_don_id] int NOT NULL,
    [mon_dich_vu_id] int NOT NULL,
    [so_luong] int NOT NULL,
    [don_gia] decimal(18,2) NOT NULL,
    [thanh_tien] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_chi_tiet_hoa_don] PRIMARY KEY ([id])
);

CREATE TABLE [chi_tiet_nhap_kho] (
    [id] int NOT NULL IDENTITY,
    [phieu_nhap_id] int NOT NULL,
    [mon_dich_vu_id] int NOT NULL,
    [so_luong] int NOT NULL,
    [gia_nhap] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_chi_tiet_nhap_kho] PRIMARY KEY ([id])
);

CREATE TABLE [chuyen_ban] (
    [id] int NOT NULL IDENTITY,
    [hoa_don_id] int NOT NULL,
    [tu_ban_id] int NOT NULL,
    [den_ban_id] int NOT NULL,
    [gio_chuyen_ban] datetime2 NOT NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_chuyen_ban] PRIMARY KEY ([id])
);

CREATE TABLE [co_thu_tham_gia] (
    [id] int NOT NULL IDENTITY,
    [giai_dau_id] int NOT NULL,
    [khach_hang_id] int NOT NULL,
    [so_hat_giong] int NULL,
    [ngay_dang_ky] datetime2 NOT NULL,
    [thu_hang_chung_cuoc] int NULL,
    CONSTRAINT [PK_co_thu_tham_gia] PRIMARY KEY ([id])
);

CREATE TABLE [danh_muc_mon] (
    [id] int NOT NULL IDENTITY,
    [ma_code] nvarchar(20) NOT NULL,
    [ten] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_danh_muc_mon] PRIMARY KEY ([id])
);

CREATE TABLE [dat_ban] (
    [id] int NOT NULL IDENTITY,
    [khach_hang_id] int NOT NULL,
    [ban_bida_id] int NOT NULL,
    [thoi_gian_den] datetime2 NOT NULL,
    [so_nguoi] int NOT NULL,
    [tien_coc] decimal(18,2) NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_dat_ban] PRIMARY KEY ([id])
);

CREATE TABLE [gay_khach_gui] (
    [id] int NOT NULL IDENTITY,
    [khach_hang_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [thuong_hieu_gay] nvarchar(100) NULL,
    [loai_ngon_co] nvarchar(100) NULL,
    [ngay_gui] datetime2 NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_gay_khach_gui] PRIMARY KEY ([id])
);

CREATE TABLE [gia_ban_khung_gio] (
    [id] int NOT NULL IDENTITY,
    [loai_ban_id] int NOT NULL,
    [gio_bat_dau_kg] time NOT NULL,
    [gio_ket_thuc_kg] time NOT NULL,
    [gia_khung_gio] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_gia_ban_khung_gio] PRIMARY KEY ([id])
);

CREATE TABLE [giai_dau] (
    [id] int NOT NULL IDENTITY,
    [ten_giai] nvarchar(150) NOT NULL,
    [ngay_bat_dau] datetime2 NOT NULL,
    [ngay_ket_thuc] datetime2 NOT NULL,
    [le_phi_tham_gia] decimal(18,2) NOT NULL,
    [tong_giai_thuong] decimal(18,2) NOT NULL,
    [trang_thai] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_giai_dau] PRIMARY KEY ([id])
);

CREATE TABLE [giao_dich_diem] (
    [id] int NOT NULL IDENTITY,
    [khach_hang_id] int NOT NULL,
    [loai_giao_dich] nvarchar(20) NOT NULL,
    [so_diem] int NOT NULL,
    [ngay_tao] datetime2 NOT NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_giao_dich_diem] PRIMARY KEY ([id])
);

CREATE TABLE [hang_thanh_vien] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(50) NOT NULL,
    [diem_toi_thieu] int NOT NULL,
    [ti_le_giam_gia] decimal(5,2) NOT NULL,
    [mo_ta] nvarchar(255) NULL,
    CONSTRAINT [PK_hang_thanh_vien] PRIMARY KEY ([id])
);

CREATE TABLE [hoa_don] (
    [id] int NOT NULL IDENTITY,
    [ban_bida_id] int NOT NULL,
    [nguoi_dung_id] int NOT NULL,
    [khach_hang_id] int NOT NULL,
    [khuyen_mai_id] int NULL,
    [ma_hoa_don] nvarchar(20) NOT NULL,
    [gio_bat_dau] datetime2 NOT NULL,
    [gio_ket_thuc] datetime2 NULL,
    [table_price] decimal(18,2) NOT NULL,
    [service_price] decimal(18,2) NOT NULL,
    [tien_giam_gia] decimal(18,2) NOT NULL,
    [tong_thanh_toan] decimal(18,2) NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_hoa_don] PRIMARY KEY ([id])
);

CREATE TABLE [khach_hang] (
    [id] int NOT NULL IDENTITY,
    [hang_thanh_vien_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [ho_ten] nvarchar(100) NOT NULL,
    [so_dien_thoai] nvarchar(15) NOT NULL,
    [diem_tich_luy] int NOT NULL,
    [account_balance] decimal(18,2) NOT NULL,
    [ngay_tao] datetime2 NOT NULL,
    CONSTRAINT [PK_khach_hang] PRIMARY KEY ([id])
);

CREATE TABLE [khu_vuc_ban] (
    [id] int NOT NULL IDENTITY,
    [ma_code] nvarchar(20) NOT NULL,
    [ten] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_khu_vuc_ban] PRIMARY KEY ([id])
);

CREATE TABLE [khuyen_mai] (
    [id] int NOT NULL IDENTITY,
    [ma_code] nvarchar(50) NOT NULL,
    [ten] nvarchar(150) NOT NULL,
    [phan_tram_giam] decimal(5,2) NULL,
    [so_tien_giam] decimal(18,2) NULL,
    [ngay_bat_dau] datetime2 NOT NULL,
    [ngay_het_han] datetime2 NOT NULL,
    CONSTRAINT [PK_khuyen_mai] PRIMARY KEY ([id])
);

CREATE TABLE [lich_lam_viec] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [ca_lam_id] int NOT NULL,
    [ngay_lam_viec] datetime2 NOT NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_lich_lam_viec] PRIMARY KEY ([id])
);

CREATE TABLE [loai_ban] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(50) NOT NULL,
    [gia_co_ban] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_loai_ban] PRIMARY KEY ([id])
);

CREATE TABLE [loai_chi_phi] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(100) NOT NULL,
    [mo_ta] nvarchar(255) NULL,
    CONSTRAINT [PK_loai_chi_phi] PRIMARY KEY ([id])
);

CREATE TABLE [mon_dich_vu] (
    [id] int NOT NULL IDENTITY,
    [danh_muc_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [ten] nvarchar(100) NOT NULL,
    [don_vi_tinh] nvarchar(20) NOT NULL,
    [don_gia] decimal(18,2) NOT NULL,
    [hinh_anh] nvarchar(255) NULL,
    [dang_hoat_dong] bit NOT NULL,
    CONSTRAINT [PK_mon_dich_vu] PRIMARY KEY ([id])
);

CREATE TABLE [nguoi_dung] (
    [id] int NOT NULL IDENTITY,
    [ten_dang_nhap] nvarchar(50) NOT NULL,
    [mat_khau_hash] nvarchar(255) NOT NULL,
    [ho_ten] nvarchar(100) NOT NULL,
    [email] nvarchar(100) NULL,
    [so_dien_thoai] nvarchar(15) NULL,
    [anh_dai_dien] nvarchar(255) NULL,
    [dang_hoat_dong] bit NOT NULL,
    [ngay_tao] datetime2 NOT NULL,
    CONSTRAINT [PK_nguoi_dung] PRIMARY KEY ([id])
);

CREATE TABLE [nha_cung_cap] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(150) NOT NULL,
    [so_dien_thoai] nvarchar(15) NOT NULL,
    [dia_chi] nvarchar(200) NULL,
    CONSTRAINT [PK_nha_cung_cap] PRIMARY KEY ([id])
);

CREATE TABLE [nhat_ky_hoat_dong] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [hanh_dong] nvarchar(100) NOT NULL,
    [bang_du_lieu] nvarchar(50) NULL,
    [ban_ghi_id] int NULL,
    [mo_ta] nvarchar(max) NULL,
    [dia_chi_ip] nvarchar(45) NULL,
    [thoi_gian] datetime2 NOT NULL,
    CONSTRAINT [PK_nhat_ky_hoat_dong] PRIMARY KEY ([id])
);

CREATE TABLE [phieu_chi] (
    [id] int NOT NULL IDENTITY,
    [loai_chi_phi_id] int NOT NULL,
    [nguoi_dung_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [so_tien] decimal(18,2) NOT NULL,
    [ngay_tao] datetime2 NOT NULL,
    [muc_dich_chi] nvarchar(255) NOT NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_phieu_chi] PRIMARY KEY ([id])
);

CREATE TABLE [phieu_kiem_kho] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [ngay_kiem_ke] datetime2 NOT NULL,
    [ghi_chu] nvarchar(255) NULL,
    CONSTRAINT [PK_phieu_kiem_kho] PRIMARY KEY ([id])
);

CREATE TABLE [phieu_nhap_kho] (
    [id] int NOT NULL IDENTITY,
    [nha_cung_cap_id] int NOT NULL,
    [nguoi_dung_id] int NOT NULL,
    [ma_code] nvarchar(20) NOT NULL,
    [ngay_nhap] datetime2 NOT NULL,
    [tong_tien_nhap] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_phieu_nhap_kho] PRIMARY KEY ([id])
);

CREATE TABLE [phuong_thuc_thanh_toan] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(50) NOT NULL,
    [dang_hoat_dong] bit NOT NULL,
    CONSTRAINT [PK_phuong_thuc_thanh_toan] PRIMARY KEY ([id])
);

CREATE TABLE [quyen_han] (
    [id] int NOT NULL IDENTITY,
    [ma_code] nvarchar(50) NOT NULL,
    [ten] nvarchar(100) NOT NULL,
    [phan_he] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_quyen_han] PRIMARY KEY ([id])
);

CREATE TABLE [quyen_vai_tro] (
    [id] int NOT NULL IDENTITY,
    [vai_tro_id] int NOT NULL,
    [quyen_han_id] int NOT NULL,
    CONSTRAINT [PK_quyen_vai_tro] PRIMARY KEY ([id])
);

CREATE TABLE [thanh_toan_hoa_don] (
    [id] int NOT NULL IDENTITY,
    [hoa_don_id] int NOT NULL,
    [phuong_thuc_id] int NOT NULL,
    [so_tien] decimal(18,2) NOT NULL,
    [thoi_gian_tt] datetime2 NOT NULL,
    CONSTRAINT [PK_thanh_toan_hoa_don] PRIMARY KEY ([id])
);

CREATE TABLE [thiet_bi_ban] (
    [id] int NOT NULL IDENTITY,
    [ban_bida_id] int NOT NULL,
    [ten] nvarchar(100) NOT NULL,
    [so_luong] int NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_thiet_bi_ban] PRIMARY KEY ([id])
);

CREATE TABLE [thue_tu_do] (
    [id] int NOT NULL IDENTITY,
    [tu_do_id] int NOT NULL,
    [khach_hang_id] int NOT NULL,
    [ngay_bat_dau] datetime2 NOT NULL,
    [ngay_het_han] datetime2 NOT NULL,
    [tien_thue_thang] decimal(18,2) NOT NULL,
    [tien_coc] decimal(18,2) NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_thue_tu_do] PRIMARY KEY ([id])
);

CREATE TABLE [ton_kho] (
    [id] int NOT NULL IDENTITY,
    [mon_dich_vu_id] int NOT NULL,
    [so_luong_ton] int NOT NULL,
    [muc_canh_bao] int NOT NULL,
    CONSTRAINT [PK_ton_kho] PRIMARY KEY ([id])
);

CREATE TABLE [tran_dau] (
    [id] int NOT NULL IDENTITY,
    [giai_dau_id] int NOT NULL,
    [ban_bida_id] int NOT NULL,
    [ten_vong_dau] nvarchar(50) NOT NULL,
    [co_thu_1_id] int NOT NULL,
    [co_thu_2_id] int NOT NULL,
    [ti_so_co_thu_1] int NULL,
    [ti_so_co_thu_2] int NULL,
    [nguoi_thang_id] int NULL,
    [thoi_gian_dau] datetime2 NOT NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_tran_dau] PRIMARY KEY ([id])
);

CREATE TABLE [tu_do] (
    [id] int NOT NULL IDENTITY,
    [so_tu] nvarchar(20) NOT NULL,
    [khu_vuc] nvarchar(50) NULL,
    [trang_thai] nvarchar(20) NOT NULL,
    [tien_thue_thang] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_tu_do] PRIMARY KEY ([id])
);

CREATE TABLE [vai_tro] (
    [id] int NOT NULL IDENTITY,
    [ten] nvarchar(50) NOT NULL,
    [mo_ta] nvarchar(255) NULL,
    CONSTRAINT [PK_vai_tro] PRIMARY KEY ([id])
);

CREATE TABLE [vai_tro_nguoi_dung] (
    [id] int NOT NULL IDENTITY,
    [nguoi_dung_id] int NOT NULL,
    [vai_tro_id] int NOT NULL,
    [ngay_tao] datetime2 NOT NULL,
    CONSTRAINT [PK_vai_tro_nguoi_dung] PRIMARY KEY ([id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261111063947_InitialCreate', N'10.0.0');

COMMIT;
GO


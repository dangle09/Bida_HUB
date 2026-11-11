using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyQuanBida.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ban_bida",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    khu_vuc_ban_id = table.Column<int>(type: "int", nullable: false),
                    loai_ban_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ban_bida", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bang_luong",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    thang_luong = table.Column<int>(type: "int", nullable: false),
                    nam_luong = table.Column<int>(type: "int", nullable: false),
                    tong_gio_lam = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    luong_co_ban = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    phu_cap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tien_thuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tien_phat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    thuc_linh = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bang_luong", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bao_tri_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ban_bida_id = table.Column<int>(type: "int", nullable: false),
                    loai_bao_tri = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    chi_phi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ngay_bao_tri = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nguoi_thuc_hien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bao_tri_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ca_lam",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gio_bat_dau = table.Column<TimeSpan>(type: "time", nullable: false),
                    gio_ket_thuc = table.Column<TimeSpan>(type: "time", nullable: false),
                    luong_theo_gio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ca_lam", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cham_cong",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ngay_lam_viec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gio_vao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gio_ra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    so_gio_lam = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    di_tre_phut = table.Column<int>(type: "int", nullable: true),
                    ve_som_phut = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cham_cong", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_hoa_don",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoa_don_id = table.Column<int>(type: "int", nullable: false),
                    mon_dich_vu_id = table.Column<int>(type: "int", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    don_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    thanh_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_hoa_don", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chi_tiet_nhap_kho",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    phieu_nhap_id = table.Column<int>(type: "int", nullable: false),
                    mon_dich_vu_id = table.Column<int>(type: "int", nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    gia_nhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chi_tiet_nhap_kho", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chuyen_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoa_don_id = table.Column<int>(type: "int", nullable: false),
                    tu_ban_id = table.Column<int>(type: "int", nullable: false),
                    den_ban_id = table.Column<int>(type: "int", nullable: false),
                    gio_chuyen_ban = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chuyen_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "co_thu_tham_gia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    giai_dau_id = table.Column<int>(type: "int", nullable: false),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    so_hat_giong = table.Column<int>(type: "int", nullable: true),
                    ngay_dang_ky = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thu_hang_chung_cuoc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_co_thu_tham_gia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "danh_muc_mon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_danh_muc_mon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dat_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    ban_bida_id = table.Column<int>(type: "int", nullable: false),
                    thoi_gian_den = table.Column<DateTime>(type: "datetime2", nullable: false),
                    so_nguoi = table.Column<int>(type: "int", nullable: false),
                    tien_coc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dat_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gay_khach_gui",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    thuong_hieu_gay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    loai_ngon_co = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ngay_gui = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gay_khach_gui", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gia_ban_khung_gio",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loai_ban_id = table.Column<int>(type: "int", nullable: false),
                    gio_bat_dau_kg = table.Column<TimeSpan>(type: "time", nullable: false),
                    gio_ket_thuc_kg = table.Column<TimeSpan>(type: "time", nullable: false),
                    gia_khung_gio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gia_ban_khung_gio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "giai_dau",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_giai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ngay_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    le_phi_tham_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tong_giai_thuong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giai_dau", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "giao_dich_diem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    loai_giao_dich = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    so_diem = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_giao_dich_diem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hang_thanh_vien",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    diem_toi_thieu = table.Column<int>(type: "int", nullable: false),
                    ti_le_giam_gia = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hang_thanh_vien", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hoa_don",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ban_bida_id = table.Column<int>(type: "int", nullable: false),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    khuyen_mai_id = table.Column<int>(type: "int", nullable: true),
                    ma_hoa_don = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    gio_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gio_ket_thuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    table_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    service_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tien_giam_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tong_thanh_toan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoa_don", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "khach_hang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hang_thanh_vien_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    diem_tich_luy = table.Column<int>(type: "int", nullable: false),
                    account_balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khach_hang", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "khu_vuc_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khu_vuc_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "khuyen_mai",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    phan_tram_giam = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    so_tien_giam = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ngay_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_het_han = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khuyen_mai", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lich_lam_viec",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ca_lam_id = table.Column<int>(type: "int", nullable: false),
                    ngay_lam_viec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lich_lam_viec", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loai_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gia_co_ban = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "loai_chi_phi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loai_chi_phi", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mon_dich_vu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    danh_muc_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    don_vi_tinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    don_gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    hinh_anh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    dang_hoat_dong = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mon_dich_vu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nguoi_dung",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten_dang_nhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mat_khau_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ho_ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    anh_dai_dien = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    dang_hoat_dong = table.Column<bool>(type: "bit", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nguoi_dung", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nha_cung_cap",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    so_dien_thoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    dia_chi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nha_cung_cap", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nhat_ky_hoat_dong",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    hanh_dong = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    bang_du_lieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ban_ghi_id = table.Column<int>(type: "int", nullable: true),
                    mo_ta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dia_chi_ip = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    thoi_gian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhat_ky_hoat_dong", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_chi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loai_chi_phi_id = table.Column<int>(type: "int", nullable: false),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    so_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    muc_dich_chi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_chi", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_kiem_kho",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ngay_kiem_ke = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ghi_chu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_kiem_kho", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phieu_nhap_kho",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nha_cung_cap_id = table.Column<int>(type: "int", nullable: false),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    ma_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ngay_nhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tong_tien_nhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieu_nhap_kho", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phuong_thuc_thanh_toan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dang_hoat_dong = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phuong_thuc_thanh_toan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quyen_han",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ma_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phan_he = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quyen_han", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quyen_vai_tro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vai_tro_id = table.Column<int>(type: "int", nullable: false),
                    quyen_han_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quyen_vai_tro", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thanh_toan_hoa_don",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoa_don_id = table.Column<int>(type: "int", nullable: false),
                    phuong_thuc_id = table.Column<int>(type: "int", nullable: false),
                    so_tien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    thoi_gian_tt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanh_toan_hoa_don", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thiet_bi_ban",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ban_bida_id = table.Column<int>(type: "int", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    so_luong = table.Column<int>(type: "int", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thiet_bi_ban", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thue_tu_do",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tu_do_id = table.Column<int>(type: "int", nullable: false),
                    khach_hang_id = table.Column<int>(type: "int", nullable: false),
                    ngay_bat_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngay_het_han = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tien_thue_thang = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tien_coc = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thue_tu_do", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ton_kho",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mon_dich_vu_id = table.Column<int>(type: "int", nullable: false),
                    so_luong_ton = table.Column<int>(type: "int", nullable: false),
                    muc_canh_bao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ton_kho", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tran_dau",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    giai_dau_id = table.Column<int>(type: "int", nullable: false),
                    ban_bida_id = table.Column<int>(type: "int", nullable: false),
                    ten_vong_dau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    co_thu_1_id = table.Column<int>(type: "int", nullable: false),
                    co_thu_2_id = table.Column<int>(type: "int", nullable: false),
                    ti_so_co_thu_1 = table.Column<int>(type: "int", nullable: true),
                    ti_so_co_thu_2 = table.Column<int>(type: "int", nullable: true),
                    nguoi_thang_id = table.Column<int>(type: "int", nullable: true),
                    thoi_gian_dau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tran_dau", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tu_do",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    so_tu = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    khu_vuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    trang_thai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tien_thue_thang = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tu_do", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vai_tro",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    mo_ta = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vai_tro", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vai_tro_nguoi_dung",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nguoi_dung_id = table.Column<int>(type: "int", nullable: false),
                    vai_tro_id = table.Column<int>(type: "int", nullable: false),
                    ngay_tao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vai_tro_nguoi_dung", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ban_bida");

            migrationBuilder.DropTable(
                name: "bang_luong");

            migrationBuilder.DropTable(
                name: "bao_tri_ban");

            migrationBuilder.DropTable(
                name: "ca_lam");

            migrationBuilder.DropTable(
                name: "cham_cong");

            migrationBuilder.DropTable(
                name: "chi_tiet_hoa_don");

            migrationBuilder.DropTable(
                name: "chi_tiet_nhap_kho");

            migrationBuilder.DropTable(
                name: "chuyen_ban");

            migrationBuilder.DropTable(
                name: "co_thu_tham_gia");

            migrationBuilder.DropTable(
                name: "danh_muc_mon");

            migrationBuilder.DropTable(
                name: "dat_ban");

            migrationBuilder.DropTable(
                name: "gay_khach_gui");

            migrationBuilder.DropTable(
                name: "gia_ban_khung_gio");

            migrationBuilder.DropTable(
                name: "giai_dau");

            migrationBuilder.DropTable(
                name: "giao_dich_diem");

            migrationBuilder.DropTable(
                name: "hang_thanh_vien");

            migrationBuilder.DropTable(
                name: "hoa_don");

            migrationBuilder.DropTable(
                name: "khach_hang");

            migrationBuilder.DropTable(
                name: "khu_vuc_ban");

            migrationBuilder.DropTable(
                name: "khuyen_mai");

            migrationBuilder.DropTable(
                name: "lich_lam_viec");

            migrationBuilder.DropTable(
                name: "loai_ban");

            migrationBuilder.DropTable(
                name: "loai_chi_phi");

            migrationBuilder.DropTable(
                name: "mon_dich_vu");

            migrationBuilder.DropTable(
                name: "nguoi_dung");

            migrationBuilder.DropTable(
                name: "nha_cung_cap");

            migrationBuilder.DropTable(
                name: "nhat_ky_hoat_dong");

            migrationBuilder.DropTable(
                name: "phieu_chi");

            migrationBuilder.DropTable(
                name: "phieu_kiem_kho");

            migrationBuilder.DropTable(
                name: "phieu_nhap_kho");

            migrationBuilder.DropTable(
                name: "phuong_thuc_thanh_toan");

            migrationBuilder.DropTable(
                name: "quyen_han");

            migrationBuilder.DropTable(
                name: "quyen_vai_tro");

            migrationBuilder.DropTable(
                name: "thanh_toan_hoa_don");

            migrationBuilder.DropTable(
                name: "thiet_bi_ban");

            migrationBuilder.DropTable(
                name: "thue_tu_do");

            migrationBuilder.DropTable(
                name: "ton_kho");

            migrationBuilder.DropTable(
                name: "tran_dau");

            migrationBuilder.DropTable(
                name: "tu_do");

            migrationBuilder.DropTable(
                name: "vai_tro");

            migrationBuilder.DropTable(
                name: "vai_tro_nguoi_dung");
        }
    }
}

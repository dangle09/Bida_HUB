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

        public DbSet<VaiTro> VaiTros { get; set; }
        public DbSet<QuyenHan> QuyenHans { get; set; }
        public DbSet<CaLam> CaLams { get; set; }
        public DbSet<KhuVucBan> KhuVucBans { get; set; }
        public DbSet<LoaiBan> LoaiBans { get; set; }
        public DbSet<HangThanhVien> HangThanhViens { get; set; }
        public DbSet<DanhMucMon> DanhMucMons { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public DbSet<TuDo> TuDos { get; set; }
        public DbSet<GiaiDau> GiaiDaus { get; set; }
        public DbSet<LoaiChiPhi> LoaiChiPhis { get; set; }
        public DbSet<QuyenVaiTro> QuyenVaiTros { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<GiaBanKhungGio> GiaBanKhungGios { get; set; }
        public DbSet<BanBida> BanBidas { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<MonDichVu> MonDichVus { get; set; }
        public DbSet<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }
        public DbSet<NhatKyHoatDong> NhatKyHoatDongs { get; set; }
        public DbSet<LichLamViec> LichLamViecs { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
        public DbSet<BangLuong> BangLuongs { get; set; }
        public DbSet<PhieuKiemKho> PhieuKiemKhos { get; set; }
        public DbSet<DatBan> DatBans { get; set; }
        public DbSet<ThietBiBan> ThietBiBans { get; set; }
        public DbSet<BaoTriBan> BaoTriBans { get; set; }
        public DbSet<GiaoDichDiem> GiaoDichDiems { get; set; }
        public DbSet<ThueTuDo> ThueTuDos { get; set; }
        public DbSet<GayKhachGui> GayKhachGuis { get; set; }
        public DbSet<CoThuThamGia> CoThuThamGias { get; set; }
        public DbSet<TranDau> TranDaus { get; set; }
        public DbSet<PhieuChi> PhieuChis { get; set; }
        public DbSet<TonKho> TonKhos { get; set; }
        public DbSet<PhieuNhapKho> PhieuNhapKhos { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietNhapKho> ChiTietNhapKhos { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ChuyenBan> ChuyenBans { get; set; }
        public DbSet<ThanhToanHoaDon> ThanhToanHoaDons { get; set; }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.ToTable("hoa_don");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.BanBidaId)
                .HasColumnName("ban_bida_id")
                .IsRequired();

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.KhuyenMaiId)
                .HasColumnName("khuyen_mai_id")
                .IsRequired(false);

            builder.Property(x => x.MaHoaDon)
                .HasColumnName("ma_hoa_don")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.GioBatDau)
                .HasColumnName("gio_bat_dau")
                .IsRequired();

            builder.Property(x => x.GioKetThuc)
                .HasColumnName("gio_ket_thuc")
                .IsRequired(false);

            builder.Property(x => x.TablePrice)
                .HasColumnName("table_price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ServicePrice)
                .HasColumnName("service_price")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TienGiamGia)
                .HasColumnName("tien_giam_gia")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TongThanhToan)
                .HasColumnName("tong_thanh_toan")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

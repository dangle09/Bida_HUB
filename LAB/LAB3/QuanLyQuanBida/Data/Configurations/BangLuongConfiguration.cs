using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class BangLuongConfiguration : IEntityTypeConfiguration<BangLuong>
    {
        public void Configure(EntityTypeBuilder<BangLuong> builder)
        {
            builder.ToTable("bang_luong");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.ThangLuong)
                .HasColumnName("thang_luong")
                .IsRequired();

            builder.Property(x => x.NamLuong)
                .HasColumnName("nam_luong")
                .IsRequired();

            builder.Property(x => x.TongGioLam)
                .HasColumnName("tong_gio_lam")
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            builder.Property(x => x.LuongCoBan)
                .HasColumnName("luong_co_ban")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.PhuCap)
                .HasColumnName("phu_cap")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TienThuong)
                .HasColumnName("tien_thuong")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TienPhat)
                .HasColumnName("tien_phat")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ThucLinh)
                .HasColumnName("thuc_linh")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

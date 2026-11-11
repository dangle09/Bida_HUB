using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class GiaiDauConfiguration : IEntityTypeConfiguration<GiaiDau>
    {
        public void Configure(EntityTypeBuilder<GiaiDau> builder)
        {
            builder.ToTable("giai_dau");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.TenGiai)
                .HasColumnName("ten_giai")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.NgayBatDau)
                .HasColumnName("ngay_bat_dau")
                .IsRequired();

            builder.Property(x => x.NgayKetThuc)
                .HasColumnName("ngay_ket_thuc")
                .IsRequired();

            builder.Property(x => x.LePhiThamGia)
                .HasColumnName("le_phi_tham_gia")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TongGiaiThuong)
                .HasColumnName("tong_giai_thuong")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(30)
                .IsRequired();

        }
    }
}

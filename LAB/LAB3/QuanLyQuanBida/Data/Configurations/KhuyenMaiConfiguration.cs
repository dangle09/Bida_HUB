using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class KhuyenMaiConfiguration : IEntityTypeConfiguration<KhuyenMai>
    {
        public void Configure(EntityTypeBuilder<KhuyenMai> builder)
        {
            builder.ToTable("khuyen_mai");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.PhanTramGiam)
                .HasColumnName("phan_tram_giam")
                .HasColumnType("decimal(5,2)")
                .IsRequired(false);

            builder.Property(x => x.SoTienGiam)
                .HasColumnName("so_tien_giam")
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(x => x.NgayBatDau)
                .HasColumnName("ngay_bat_dau")
                .IsRequired();

            builder.Property(x => x.NgayHetHan)
                .HasColumnName("ngay_het_han")
                .IsRequired();

        }
    }
}

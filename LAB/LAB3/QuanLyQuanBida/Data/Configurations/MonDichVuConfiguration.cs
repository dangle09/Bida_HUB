using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class MonDichVuConfiguration : IEntityTypeConfiguration<MonDichVu>
    {
        public void Configure(EntityTypeBuilder<MonDichVu> builder)
        {
            builder.ToTable("mon_dich_vu");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.DanhMucId)
                .HasColumnName("danh_muc_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DonViTinh)
                .HasColumnName("don_vi_tinh")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.DonGia)
                .HasColumnName("don_gia")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.HinhAnh)
                .HasColumnName("hinh_anh")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.DangHoatDong)
                .HasColumnName("dang_hoat_dong")
                .IsRequired();

        }
    }
}

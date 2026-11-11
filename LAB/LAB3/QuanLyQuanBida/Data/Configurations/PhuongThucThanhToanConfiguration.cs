using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PhuongThucThanhToanConfiguration : IEntityTypeConfiguration<PhuongThucThanhToan>
    {
        public void Configure(EntityTypeBuilder<PhuongThucThanhToan> builder)
        {
            builder.ToTable("phuong_thuc_thanh_toan");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DangHoatDong)
                .HasColumnName("dang_hoat_dong")
                .IsRequired();

        }
    }
}

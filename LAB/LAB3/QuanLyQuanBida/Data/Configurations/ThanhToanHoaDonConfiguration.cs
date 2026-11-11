using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ThanhToanHoaDonConfiguration : IEntityTypeConfiguration<ThanhToanHoaDon>
    {
        public void Configure(EntityTypeBuilder<ThanhToanHoaDon> builder)
        {
            builder.ToTable("thanh_toan_hoa_don");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.HoaDonId)
                .HasColumnName("hoa_don_id")
                .IsRequired();

            builder.Property(x => x.PhuongThucId)
                .HasColumnName("phuong_thuc_id")
                .IsRequired();

            builder.Property(x => x.SoTien)
                .HasColumnName("so_tien")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ThoiGianTt)
                .HasColumnName("thoi_gian_tt")
                .IsRequired();

        }
    }
}

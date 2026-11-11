using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ChiTietHoaDonConfiguration : IEntityTypeConfiguration<ChiTietHoaDon>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDon> builder)
        {
            builder.ToTable("chi_tiet_hoa_don");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.HoaDonId)
                .HasColumnName("hoa_don_id")
                .IsRequired();

            builder.Property(x => x.MonDichVuId)
                .HasColumnName("mon_dich_vu_id")
                .IsRequired();

            builder.Property(x => x.SoLuong)
                .HasColumnName("so_luong")
                .IsRequired();

            builder.Property(x => x.DonGia)
                .HasColumnName("don_gia")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ThanhTien)
                .HasColumnName("thanh_tien")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

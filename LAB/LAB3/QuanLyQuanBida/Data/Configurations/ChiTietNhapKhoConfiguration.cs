using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ChiTietNhapKhoConfiguration : IEntityTypeConfiguration<ChiTietNhapKho>
    {
        public void Configure(EntityTypeBuilder<ChiTietNhapKho> builder)
        {
            builder.ToTable("chi_tiet_nhap_kho");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.PhieuNhapId)
                .HasColumnName("phieu_nhap_id")
                .IsRequired();

            builder.Property(x => x.MonDichVuId)
                .HasColumnName("mon_dich_vu_id")
                .IsRequired();

            builder.Property(x => x.SoLuong)
                .HasColumnName("so_luong")
                .IsRequired();

            builder.Property(x => x.GiaNhap)
                .HasColumnName("gia_nhap")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

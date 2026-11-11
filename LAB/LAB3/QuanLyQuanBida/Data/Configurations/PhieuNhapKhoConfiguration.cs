using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PhieuNhapKhoConfiguration : IEntityTypeConfiguration<PhieuNhapKho>
    {
        public void Configure(EntityTypeBuilder<PhieuNhapKho> builder)
        {
            builder.ToTable("phieu_nhap_kho");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NhaCungCapId)
                .HasColumnName("nha_cung_cap_id")
                .IsRequired();

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.NgayNhap)
                .HasColumnName("ngay_nhap")
                .IsRequired();

            builder.Property(x => x.TongTienNhap)
                .HasColumnName("tong_tien_nhap")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

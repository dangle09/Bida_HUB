using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class NguoiDungConfiguration : IEntityTypeConfiguration<NguoiDung>
    {
        public void Configure(EntityTypeBuilder<NguoiDung> builder)
        {
            builder.ToTable("nguoi_dung");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.TenDangNhap)
                .HasColumnName("ten_dang_nhap")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.MatKhauHash)
                .HasColumnName("mat_khau_hash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.HoTen)
                .HasColumnName("ho_ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.SoDienThoai)
                .HasColumnName("so_dien_thoai")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(x => x.AnhDaiDien)
                .HasColumnName("anh_dai_dien")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.DangHoatDong)
                .HasColumnName("dang_hoat_dong")
                .IsRequired();

            builder.Property(x => x.NgayTao)
                .HasColumnName("ngay_tao")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.ToTable("khach_hang");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.HangThanhVienId)
                .HasColumnName("hang_thanh_vien_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.HoTen)
                .HasColumnName("ho_ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.SoDienThoai)
                .HasColumnName("so_dien_thoai")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.DiemTichLuy)
                .HasColumnName("diem_tich_luy")
                .IsRequired();

            builder.Property(x => x.AccountBalance)
                .HasColumnName("account_balance")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.NgayTao)
                .HasColumnName("ngay_tao")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

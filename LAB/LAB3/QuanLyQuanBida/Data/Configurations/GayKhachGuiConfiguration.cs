using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class GayKhachGuiConfiguration : IEntityTypeConfiguration<GayKhachGui>
    {
        public void Configure(EntityTypeBuilder<GayKhachGui> builder)
        {
            builder.ToTable("gay_khach_gui");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.ThuongHieuGay)
                .HasColumnName("thuong_hieu_gay")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.LoaiNgonCo)
                .HasColumnName("loai_ngon_co")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.NgayGui)
                .HasColumnName("ngay_gui")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

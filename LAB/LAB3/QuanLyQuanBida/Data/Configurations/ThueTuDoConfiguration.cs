using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ThueTuDoConfiguration : IEntityTypeConfiguration<ThueTuDo>
    {
        public void Configure(EntityTypeBuilder<ThueTuDo> builder)
        {
            builder.ToTable("thue_tu_do");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.TuDoId)
                .HasColumnName("tu_do_id")
                .IsRequired();

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.NgayBatDau)
                .HasColumnName("ngay_bat_dau")
                .IsRequired();

            builder.Property(x => x.NgayHetHan)
                .HasColumnName("ngay_het_han")
                .IsRequired();

            builder.Property(x => x.TienThueThang)
                .HasColumnName("tien_thue_thang")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TienCoc)
                .HasColumnName("tien_coc")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

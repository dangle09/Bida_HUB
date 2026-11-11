using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TuDoConfiguration : IEntityTypeConfiguration<TuDo>
    {
        public void Configure(EntityTypeBuilder<TuDo> builder)
        {
            builder.ToTable("tu_do");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.SoTu)
                .HasColumnName("so_tu")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.KhuVuc)
                .HasColumnName("khu_vuc")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.TienThueThang)
                .HasColumnName("tien_thue_thang")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

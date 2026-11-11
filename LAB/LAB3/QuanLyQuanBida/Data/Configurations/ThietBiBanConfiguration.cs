using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ThietBiBanConfiguration : IEntityTypeConfiguration<ThietBiBan>
    {
        public void Configure(EntityTypeBuilder<ThietBiBan> builder)
        {
            builder.ToTable("thiet_bi_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.BanBidaId)
                .HasColumnName("ban_bida_id")
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.SoLuong)
                .HasColumnName("so_luong")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

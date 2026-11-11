using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class BanBidaConfiguration : IEntityTypeConfiguration<BanBida>
    {
        public void Configure(EntityTypeBuilder<BanBida> builder)
        {
            builder.ToTable("ban_bida");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.KhuVucBanId)
                .HasColumnName("khu_vuc_ban_id")
                .IsRequired();

            builder.Property(x => x.LoaiBanId)
                .HasColumnName("loai_ban_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

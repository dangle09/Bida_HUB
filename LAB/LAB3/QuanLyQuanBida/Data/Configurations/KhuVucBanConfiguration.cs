using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class KhuVucBanConfiguration : IEntityTypeConfiguration<KhuVucBan>
    {
        public void Configure(EntityTypeBuilder<KhuVucBan> builder)
        {
            builder.ToTable("khu_vuc_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}

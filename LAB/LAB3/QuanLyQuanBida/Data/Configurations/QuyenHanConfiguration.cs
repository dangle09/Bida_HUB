using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class QuyenHanConfiguration : IEntityTypeConfiguration<QuyenHan>
    {
        public void Configure(EntityTypeBuilder<QuyenHan> builder)
        {
            builder.ToTable("quyen_han");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PhanHe)
                .HasColumnName("phan_he")
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}

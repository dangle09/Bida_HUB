using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class NhaCungCapConfiguration : IEntityTypeConfiguration<NhaCungCap>
    {
        public void Configure(EntityTypeBuilder<NhaCungCap> builder)
        {
            builder.ToTable("nha_cung_cap");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.SoDienThoai)
                .HasColumnName("so_dien_thoai")
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(x => x.DiaChi)
                .HasColumnName("dia_chi")
                .HasMaxLength(200)
                .IsRequired(false);

        }
    }
}

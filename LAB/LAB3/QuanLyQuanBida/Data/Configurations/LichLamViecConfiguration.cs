using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class LichLamViecConfiguration : IEntityTypeConfiguration<LichLamViec>
    {
        public void Configure(EntityTypeBuilder<LichLamViec> builder)
        {
            builder.ToTable("lich_lam_viec");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.CaLamId)
                .HasColumnName("ca_lam_id")
                .IsRequired();

            builder.Property(x => x.NgayLamViec)
                .HasColumnName("ngay_lam_viec")
                .IsRequired();

            builder.Property(x => x.GhiChu)
                .HasColumnName("ghi_chu")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

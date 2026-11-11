using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ChuyenBanConfiguration : IEntityTypeConfiguration<ChuyenBan>
    {
        public void Configure(EntityTypeBuilder<ChuyenBan> builder)
        {
            builder.ToTable("chuyen_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.HoaDonId)
                .HasColumnName("hoa_don_id")
                .IsRequired();

            builder.Property(x => x.TuBanId)
                .HasColumnName("tu_ban_id")
                .IsRequired();

            builder.Property(x => x.DenBanId)
                .HasColumnName("den_ban_id")
                .IsRequired();

            builder.Property(x => x.GioChuyenBan)
                .HasColumnName("gio_chuyen_ban")
                .IsRequired();

            builder.Property(x => x.GhiChu)
                .HasColumnName("ghi_chu")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class DatBanConfiguration : IEntityTypeConfiguration<DatBan>
    {
        public void Configure(EntityTypeBuilder<DatBan> builder)
        {
            builder.ToTable("dat_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.BanBidaId)
                .HasColumnName("ban_bida_id")
                .IsRequired();

            builder.Property(x => x.ThoiGianDen)
                .HasColumnName("thoi_gian_den")
                .IsRequired();

            builder.Property(x => x.SoNguoi)
                .HasColumnName("so_nguoi")
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

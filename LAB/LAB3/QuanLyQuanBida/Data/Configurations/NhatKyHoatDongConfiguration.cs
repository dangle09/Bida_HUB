using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class NhatKyHoatDongConfiguration : IEntityTypeConfiguration<NhatKyHoatDong>
    {
        public void Configure(EntityTypeBuilder<NhatKyHoatDong> builder)
        {
            builder.ToTable("nhat_ky_hoat_dong");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.HanhDong)
                .HasColumnName("hanh_dong")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.BangDuLieu)
                .HasColumnName("bang_du_lieu")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.BanGhiId)
                .HasColumnName("ban_ghi_id")
                .IsRequired(false);

            builder.Property(x => x.MoTa)
                .HasColumnName("mo_ta")
                .IsRequired(false);

            builder.Property(x => x.DiaChiIp)
                .HasColumnName("dia_chi_ip")
                .HasMaxLength(45)
                .IsRequired(false);

            builder.Property(x => x.ThoiGian)
                .HasColumnName("thoi_gian")
                .IsRequired();

        }
    }
}

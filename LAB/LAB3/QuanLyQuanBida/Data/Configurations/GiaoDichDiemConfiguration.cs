using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class GiaoDichDiemConfiguration : IEntityTypeConfiguration<GiaoDichDiem>
    {
        public void Configure(EntityTypeBuilder<GiaoDichDiem> builder)
        {
            builder.ToTable("giao_dich_diem");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.LoaiGiaoDich)
                .HasColumnName("loai_giao_dich")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.SoDiem)
                .HasColumnName("so_diem")
                .IsRequired();

            builder.Property(x => x.NgayTao)
                .HasColumnName("ngay_tao")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.GhiChu)
                .HasColumnName("ghi_chu")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

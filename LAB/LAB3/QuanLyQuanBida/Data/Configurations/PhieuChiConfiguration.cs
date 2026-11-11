using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PhieuChiConfiguration : IEntityTypeConfiguration<PhieuChi>
    {
        public void Configure(EntityTypeBuilder<PhieuChi> builder)
        {
            builder.ToTable("phieu_chi");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.LoaiChiPhiId)
                .HasColumnName("loai_chi_phi_id")
                .IsRequired();

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.SoTien)
                .HasColumnName("so_tien")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.NgayTao)
                .HasColumnName("ngay_tao")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.MucDichChi)
                .HasColumnName("muc_dich_chi")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.GhiChu)
                .HasColumnName("ghi_chu")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class BaoTriBanConfiguration : IEntityTypeConfiguration<BaoTriBan>
    {
        public void Configure(EntityTypeBuilder<BaoTriBan> builder)
        {
            builder.ToTable("bao_tri_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.BanBidaId)
                .HasColumnName("ban_bida_id")
                .IsRequired();

            builder.Property(x => x.LoaiBaoTri)
                .HasColumnName("loai_bao_tri")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.ChiPhi)
                .HasColumnName("chi_phi")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.NgayBaoTri)
                .HasColumnName("ngay_bao_tri")
                .IsRequired();

            builder.Property(x => x.NguoiThucHien)
                .HasColumnName("nguoi_thuc_hien")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.GhiChu)
                .HasColumnName("ghi_chu")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

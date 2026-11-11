using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ChamCongConfiguration : IEntityTypeConfiguration<ChamCong>
    {
        public void Configure(EntityTypeBuilder<ChamCong> builder)
        {
            builder.ToTable("cham_cong");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.NgayLamViec)
                .HasColumnName("ngay_lam_viec")
                .IsRequired();

            builder.Property(x => x.GioVao)
                .HasColumnName("gio_vao")
                .IsRequired();

            builder.Property(x => x.GioRa)
                .HasColumnName("gio_ra")
                .IsRequired(false);

            builder.Property(x => x.SoGioLam)
                .HasColumnName("so_gio_lam")
                .HasColumnType("decimal(5,2)")
                .IsRequired(false);

            builder.Property(x => x.DiTrePhut)
                .HasColumnName("di_tre_phut")
                .IsRequired(false);

            builder.Property(x => x.VeSomPhut)
                .HasColumnName("ve_som_phut")
                .IsRequired(false);

        }
    }
}

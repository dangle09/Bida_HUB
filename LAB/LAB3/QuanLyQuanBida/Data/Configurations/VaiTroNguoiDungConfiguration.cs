using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class VaiTroNguoiDungConfiguration : IEntityTypeConfiguration<VaiTroNguoiDung>
    {
        public void Configure(EntityTypeBuilder<VaiTroNguoiDung> builder)
        {
            builder.ToTable("vai_tro_nguoi_dung");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.NguoiDungId)
                .HasColumnName("nguoi_dung_id")
                .IsRequired();

            builder.Property(x => x.VaiTroId)
                .HasColumnName("vai_tro_id")
                .IsRequired();

            builder.Property(x => x.NgayTao)
                .HasColumnName("ngay_tao")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

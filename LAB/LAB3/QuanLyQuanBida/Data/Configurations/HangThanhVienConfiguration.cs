using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class HangThanhVienConfiguration : IEntityTypeConfiguration<HangThanhVien>
    {
        public void Configure(EntityTypeBuilder<HangThanhVien> builder)
        {
            builder.ToTable("hang_thanh_vien");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DiemToiThieu)
                .HasColumnName("diem_toi_thieu")
                .IsRequired();

            builder.Property(x => x.TiLeGiamGia)
                .HasColumnName("ti_le_giam_gia")
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.MoTa)
                .HasColumnName("mo_ta")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

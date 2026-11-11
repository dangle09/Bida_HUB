using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class LoaiChiPhiConfiguration : IEntityTypeConfiguration<LoaiChiPhi>
    {
        public void Configure(EntityTypeBuilder<LoaiChiPhi> builder)
        {
            builder.ToTable("loai_chi_phi");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.MoTa)
                .HasColumnName("mo_ta")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class QuyenVaiTroConfiguration : IEntityTypeConfiguration<QuyenVaiTro>
    {
        public void Configure(EntityTypeBuilder<QuyenVaiTro> builder)
        {
            builder.ToTable("quyen_vai_tro");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.VaiTroId)
                .HasColumnName("vai_tro_id")
                .IsRequired();

            builder.Property(x => x.QuyenHanId)
                .HasColumnName("quyen_han_id")
                .IsRequired();

        }
    }
}

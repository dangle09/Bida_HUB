using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class DanhMucMonConfiguration : IEntityTypeConfiguration<DanhMucMon>
    {
        public void Configure(EntityTypeBuilder<DanhMucMon> builder)
        {
            builder.ToTable("danh_muc_mon");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.MaCode)
                .HasColumnName("ma_code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}

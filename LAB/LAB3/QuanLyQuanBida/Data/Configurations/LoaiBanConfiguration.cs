using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class LoaiBanConfiguration : IEntityTypeConfiguration<LoaiBan>
    {
        public void Configure(EntityTypeBuilder<LoaiBan> builder)
        {
            builder.ToTable("loai_ban");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.GiaCoBan)
                .HasColumnName("gia_co_ban")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class CaLamConfiguration : IEntityTypeConfiguration<CaLam>
    {
        public void Configure(EntityTypeBuilder<CaLam> builder)
        {
            builder.ToTable("ca_lam");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.Ten)
                .HasColumnName("ten")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.GioBatDau)
                .HasColumnName("gio_bat_dau")
                .IsRequired();

            builder.Property(x => x.GioKetThuc)
                .HasColumnName("gio_ket_thuc")
                .IsRequired();

            builder.Property(x => x.LuongTheoGio)
                .HasColumnName("luong_theo_gio")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

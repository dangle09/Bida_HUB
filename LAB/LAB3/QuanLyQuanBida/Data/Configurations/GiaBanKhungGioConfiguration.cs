using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class GiaBanKhungGioConfiguration : IEntityTypeConfiguration<GiaBanKhungGio>
    {
        public void Configure(EntityTypeBuilder<GiaBanKhungGio> builder)
        {
            builder.ToTable("gia_ban_khung_gio");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.LoaiBanId)
                .HasColumnName("loai_ban_id")
                .IsRequired();

            builder.Property(x => x.GioBatDauKg)
                .HasColumnName("gio_bat_dau_kg")
                .IsRequired();

            builder.Property(x => x.GioKetThucKg)
                .HasColumnName("gio_ket_thuc_kg")
                .IsRequired();

            builder.Property(x => x.GiaKhungGio)
                .HasColumnName("gia_khung_gio")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

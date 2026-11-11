using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TonKhoConfiguration : IEntityTypeConfiguration<TonKho>
    {
        public void Configure(EntityTypeBuilder<TonKho> builder)
        {
            builder.ToTable("ton_kho");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.MonDichVuId)
                .HasColumnName("mon_dich_vu_id")
                .IsRequired();

            builder.Property(x => x.SoLuongTon)
                .HasColumnName("so_luong_ton")
                .IsRequired();

            builder.Property(x => x.MucCanhBao)
                .HasColumnName("muc_canh_bao")
                .IsRequired();

        }
    }
}

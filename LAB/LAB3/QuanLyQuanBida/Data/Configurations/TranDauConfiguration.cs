using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TranDauConfiguration : IEntityTypeConfiguration<TranDau>
    {
        public void Configure(EntityTypeBuilder<TranDau> builder)
        {
            builder.ToTable("tran_dau");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.GiaiDauId)
                .HasColumnName("giai_dau_id")
                .IsRequired();

            builder.Property(x => x.BanBidaId)
                .HasColumnName("ban_bida_id")
                .IsRequired();

            builder.Property(x => x.TenVongDau)
                .HasColumnName("ten_vong_dau")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CoThu1Id)
                .HasColumnName("co_thu_1_id")
                .IsRequired();

            builder.Property(x => x.CoThu2Id)
                .HasColumnName("co_thu_2_id")
                .IsRequired();

            builder.Property(x => x.TiSoCoThu1)
                .HasColumnName("ti_so_co_thu_1")
                .IsRequired(false);

            builder.Property(x => x.TiSoCoThu2)
                .HasColumnName("ti_so_co_thu_2")
                .IsRequired(false);

            builder.Property(x => x.NguoiThangId)
                .HasColumnName("nguoi_thang_id")
                .IsRequired(false);

            builder.Property(x => x.ThoiGianDau)
                .HasColumnName("thoi_gian_dau")
                .IsRequired();

            builder.Property(x => x.TrangThai)
                .HasColumnName("trang_thai")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

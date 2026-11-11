using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class CoThuThamGiaConfiguration : IEntityTypeConfiguration<CoThuThamGia>
    {
        public void Configure(EntityTypeBuilder<CoThuThamGia> builder)
        {
            builder.ToTable("co_thu_tham_gia");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.GiaiDauId)
                .HasColumnName("giai_dau_id")
                .IsRequired();

            builder.Property(x => x.KhachHangId)
                .HasColumnName("khach_hang_id")
                .IsRequired();

            builder.Property(x => x.SoHatGiong)
                .HasColumnName("so_hat_giong")
                .IsRequired(false);

            builder.Property(x => x.NgayDangKy)
                .HasColumnName("ngay_dang_ky")
                .IsRequired();

            builder.Property(x => x.ThuHangChungCuoc)
                .HasColumnName("thu_hang_chung_cuoc")
                .IsRequired(false);

        }
    }
}

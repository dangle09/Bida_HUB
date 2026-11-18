using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class GoodsReceiptDetailConfiguration : IEntityTypeConfiguration<GoodsReceiptDetail>
    {
        public void Configure(EntityTypeBuilder<GoodsReceiptDetail> builder)
        {
            builder.ToTable("GoodsReceiptDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.GoodsReceiptId)
                .HasColumnName("GoodsReceiptId")
                .IsRequired();

            builder.Property(x => x.ServiceItemId)
                .HasColumnName("ServiceItemId")
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder.Property(x => x.ImportPrice)
                .HasColumnName("ImportPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

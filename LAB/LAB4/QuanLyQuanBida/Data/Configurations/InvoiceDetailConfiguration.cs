using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class InvoiceDetailConfiguration : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.InvoiceId)
                .HasColumnName("InvoiceId")
                .IsRequired();

            builder.Property(x => x.ServiceItemId)
                .HasColumnName("ServiceItemId")
                .IsRequired();

            builder.Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .HasColumnName("TotalAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.BilliardTableId)
                .HasColumnName("BilliardTableId")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(x => x.PromotionId)
                .HasColumnName("PromotionId")
                .IsRequired(false);

            builder.Property(x => x.InvoiceCode)
                .HasColumnName("InvoiceCode")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.StartTime)
                .HasColumnName("StartTime")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnName("EndTime")
                .IsRequired(false);

            builder.Property(x => x.TableAmount)
                .HasColumnName("TableAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.ServiceAmount)
                .HasColumnName("ServiceAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .HasColumnName("DiscountAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TotalPayment)
                .HasColumnName("TotalPayment")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

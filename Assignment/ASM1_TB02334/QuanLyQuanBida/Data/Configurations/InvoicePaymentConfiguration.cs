using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class InvoicePaymentConfiguration : IEntityTypeConfiguration<InvoicePayment>
    {
        public void Configure(EntityTypeBuilder<InvoicePayment> builder)
        {
            builder.ToTable("InvoicePayments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.InvoiceId)
                .HasColumnName("InvoiceId")
                .IsRequired();

            builder.Property(x => x.PaymentMethodId)
                .HasColumnName("PaymentMethodId")
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnName("Amount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.PaymentTime)
                .HasColumnName("PaymentTime")
                .IsRequired();

        }
    }
}

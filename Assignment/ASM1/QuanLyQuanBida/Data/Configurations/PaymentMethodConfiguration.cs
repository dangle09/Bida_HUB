using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

        }
    }
}

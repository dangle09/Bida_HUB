using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ServiceItemConfiguration : IEntityTypeConfiguration<ServiceItem>
    {
        public void Configure(EntityTypeBuilder<ServiceItem> builder)
        {
            builder.ToTable("ServiceItems");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.CategoryId)
                .HasColumnName("CategoryId")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Unit)
                .HasColumnName("Unit")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Image)
                .HasColumnName("Image")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

        }
    }
}

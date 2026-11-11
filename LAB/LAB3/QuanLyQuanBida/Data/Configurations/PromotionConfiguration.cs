using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.DiscountPercent)
                .HasColumnName("DiscountPercent")
                .HasColumnType("decimal(5,2)")
                .IsRequired(false);

            builder.Property(x => x.DiscountAmount)
                .HasColumnName("DiscountAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired(false);

            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();

        }
    }
}

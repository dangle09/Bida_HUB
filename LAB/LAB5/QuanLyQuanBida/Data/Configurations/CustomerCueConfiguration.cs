using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class CustomerCueConfiguration : IEntityTypeConfiguration<CustomerCue>
    {
        public void Configure(EntityTypeBuilder<CustomerCue> builder)
        {
            builder.ToTable("CustomerCues");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CueBrand)
                .HasColumnName("CueBrand")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.ShaftType)
                .HasColumnName("ShaftType")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.ReceivedDate)
                .HasColumnName("ReceivedDate")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class InventoryAuditConfiguration : IEntityTypeConfiguration<InventoryAudit>
    {
        public void Configure(EntityTypeBuilder<InventoryAudit> builder)
        {
            builder.ToTable("InventoryAudits");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.AuditDate)
                .HasColumnName("AuditDate")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

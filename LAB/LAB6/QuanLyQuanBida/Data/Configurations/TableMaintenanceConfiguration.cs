using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TableMaintenanceConfiguration : IEntityTypeConfiguration<TableMaintenance>
    {
        public void Configure(EntityTypeBuilder<TableMaintenance> builder)
        {
            builder.ToTable("TableMaintenances");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.BilliardTableId)
                .HasColumnName("BilliardTableId")
                .IsRequired();

            builder.Property(x => x.MaintenanceType)
                .HasColumnName("MaintenanceType")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Cost)
                .HasColumnName("Cost")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.MaintenanceDate)
                .HasColumnName("MaintenanceDate")
                .IsRequired();

            builder.Property(x => x.PerformedBy)
                .HasColumnName("PerformedBy")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

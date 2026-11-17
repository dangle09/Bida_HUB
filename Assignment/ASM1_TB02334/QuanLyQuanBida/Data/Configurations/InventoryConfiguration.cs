using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.ServiceItemId)
                .HasColumnName("ServiceItemId")
                .IsRequired();

            builder.Property(x => x.StockQuantity)
                .HasColumnName("StockQuantity")
                .IsRequired();

            builder.Property(x => x.ReorderLevel)
                .HasColumnName("ReorderLevel")
                .IsRequired();

        }
    }
}

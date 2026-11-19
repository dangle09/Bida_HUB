using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TableTransferConfiguration : IEntityTypeConfiguration<TableTransfer>
    {
        public void Configure(EntityTypeBuilder<TableTransfer> builder)
        {
            builder.ToTable("TableTransfers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.InvoiceId)
                .HasColumnName("InvoiceId")
                .IsRequired();

            builder.Property(x => x.FromTableId)
                .HasColumnName("FromTableId")
                .IsRequired();

            builder.Property(x => x.ToTableId)
                .HasColumnName("ToTableId")
                .IsRequired();

            builder.Property(x => x.TransferTime)
                .HasColumnName("TransferTime")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

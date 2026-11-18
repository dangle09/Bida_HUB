using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TableTypeConfiguration : IEntityTypeConfiguration<TableType>
    {
        public void Configure(EntityTypeBuilder<TableType> builder)
        {
            builder.ToTable("TableTypes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.BasePrice)
                .HasColumnName("BasePrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

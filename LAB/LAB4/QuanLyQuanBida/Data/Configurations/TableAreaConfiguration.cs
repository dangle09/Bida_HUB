using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TableAreaConfiguration : IEntityTypeConfiguration<TableArea>
    {
        public void Configure(EntityTypeBuilder<TableArea> builder)
        {
            builder.ToTable("TableAreas");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}

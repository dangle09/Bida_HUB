using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class BilliardTableConfiguration : IEntityTypeConfiguration<BilliardTable>
    {
        public void Configure(EntityTypeBuilder<BilliardTable> builder)
        {
            builder.ToTable("BilliardTables");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.TableAreaId)
                .HasColumnName("TableAreaId")
                .IsRequired();

            builder.Property(x => x.TableTypeId)
                .HasColumnName("TableTypeId")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.StartTime)
                .HasColumnName("StartTime")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnName("EndTime")
                .IsRequired();

            builder.Property(x => x.HourlyRate)
                .HasColumnName("HourlyRate")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class LockerConfiguration : IEntityTypeConfiguration<Locker>
    {
        public void Configure(EntityTypeBuilder<Locker> builder)
        {
            builder.ToTable("Lockers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.LockerNumber)
                .HasColumnName("LockerNumber")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Area)
                .HasColumnName("Area")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.MonthlyRent)
                .HasColumnName("MonthlyRent")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

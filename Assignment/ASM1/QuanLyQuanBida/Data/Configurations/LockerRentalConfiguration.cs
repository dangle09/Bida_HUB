using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class LockerRentalConfiguration : IEntityTypeConfiguration<LockerRental>
    {
        public void Configure(EntityTypeBuilder<LockerRental> builder)
        {
            builder.ToTable("LockerRentals");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.LockerId)
                .HasColumnName("LockerId")
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();

            builder.Property(x => x.MonthlyRent)
                .HasColumnName("MonthlyRent")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Deposit)
                .HasColumnName("Deposit")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(x => x.BilliardTableId)
                .HasColumnName("BilliardTableId")
                .IsRequired();

            builder.Property(x => x.ArrivalTime)
                .HasColumnName("ArrivalTime")
                .IsRequired();

            builder.Property(x => x.GuestCount)
                .HasColumnName("GuestCount")
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

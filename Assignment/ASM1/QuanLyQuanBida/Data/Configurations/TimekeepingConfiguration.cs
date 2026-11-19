using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TimekeepingConfiguration : IEntityTypeConfiguration<Timekeeping>
    {
        public void Configure(EntityTypeBuilder<Timekeeping> builder)
        {
            builder.ToTable("Timekeepings");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.WorkDate)
                .HasColumnName("WorkDate")
                .IsRequired();

            builder.Property(x => x.CheckInTime)
                .HasColumnName("CheckInTime")
                .IsRequired();

            builder.Property(x => x.CheckOutTime)
                .HasColumnName("CheckOutTime")
                .IsRequired(false);

            builder.Property(x => x.WorkHours)
                .HasColumnName("WorkHours")
                .HasColumnType("decimal(4,2)")
                .IsRequired(false);

            builder.Property(x => x.LateMinutes)
                .HasColumnName("LateMinutes")
                .IsRequired(false);

            builder.Property(x => x.EarlyLeaveMinutes)
                .HasColumnName("EarlyLeaveMinutes")
                .IsRequired(false);

        }
    }
}

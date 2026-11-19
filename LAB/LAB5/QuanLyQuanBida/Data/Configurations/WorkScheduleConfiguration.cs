using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class WorkScheduleConfiguration : IEntityTypeConfiguration<WorkSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkSchedule> builder)
        {
            builder.ToTable("WorkSchedules");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.ShiftId)
                .HasColumnName("ShiftId")
                .IsRequired();

            builder.Property(x => x.WorkDate)
                .HasColumnName("WorkDate")
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasColumnName("Notes")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

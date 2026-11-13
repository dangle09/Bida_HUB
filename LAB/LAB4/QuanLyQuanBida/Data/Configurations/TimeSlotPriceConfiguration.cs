using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TimeSlotPriceConfiguration : IEntityTypeConfiguration<TimeSlotPrice>
    {
        public void Configure(EntityTypeBuilder<TimeSlotPrice> builder)
        {
            builder.ToTable("TimeSlotPrices");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.TableTypeId)
                .HasColumnName("TableTypeId")
                .IsRequired();

            builder.Property(x => x.SlotStartTime)
                .HasColumnName("SlotStartTime")
                .IsRequired();

            builder.Property(x => x.SlotEndTime)
                .HasColumnName("SlotEndTime")
                .IsRequired();

            builder.Property(x => x.SlotPrice)
                .HasColumnName("SlotPrice")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            builder.ToTable("ActivityLogs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.Action)
                .HasColumnName("Action")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.TableName)
                .HasColumnName("TableName")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.RecordId)
                .HasColumnName("RecordId")
                .IsRequired(false);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.IpAddress)
                .HasColumnName("IpAddress")
                .HasMaxLength(45)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

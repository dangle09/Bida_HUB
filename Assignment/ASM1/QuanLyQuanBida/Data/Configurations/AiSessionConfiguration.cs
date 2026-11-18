using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiSessionConfiguration : IEntityTypeConfiguration<AiSession>
    {
        public void Configure(EntityTypeBuilder<AiSession> builder)
        {
            builder.ToTable("AiSessions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired(false);

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired(false);

            builder.Property(x => x.SessionKey)
                .HasColumnName("SessionKey")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.StartedAt)
                .HasColumnName("StartedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.LastActiveAt)
                .HasColumnName("LastActiveAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();
        }
    }
}

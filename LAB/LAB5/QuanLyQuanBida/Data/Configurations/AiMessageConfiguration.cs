using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiMessageConfiguration : IEntityTypeConfiguration<AiMessage>
    {
        public void Configure(EntityTypeBuilder<AiMessage> builder)
        {
            builder.ToTable("AiMessages");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.AiSessionId)
                .HasColumnName("AiSessionId")
                .IsRequired();

            builder.Property(x => x.Role)
                .HasColumnName("Role")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnName("Content")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

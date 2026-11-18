using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiStateConfiguration : IEntityTypeConfiguration<AiState>
    {
        public void Configure(EntityTypeBuilder<AiState> builder)
        {
            builder.ToTable("AiStates");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.AiSessionId)
                .HasColumnName("AiSessionId")
                .IsRequired(false);

            builder.Property(x => x.StateKey)
                .HasColumnName("StateKey")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.StateValue)
                .HasColumnName("StateValue")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}

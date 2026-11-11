using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiAgentLogConfiguration : IEntityTypeConfiguration<AiAgentLog>
    {
        public void Configure(EntityTypeBuilder<AiAgentLog> builder)
        {
            builder.ToTable("AiAgentLogs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.AgentName)
                .HasColumnName("AgentName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ActionTaken)
                .HasColumnName("ActionTaken")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.IsSuccess)
                .HasColumnName("IsSuccess")
                .IsRequired();

            builder.Property(x => x.ExecutionTimeMs)
                .HasColumnName("ExecutionTimeMs")
                .IsRequired();

            builder.Property(x => x.LogDetails)
                .HasColumnName("LogDetails")
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

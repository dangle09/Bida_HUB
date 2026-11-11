using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiPredictionConfiguration : IEntityTypeConfiguration<AiPrediction>
    {
        public void Configure(EntityTypeBuilder<AiPrediction> builder)
        {
            builder.ToTable("AiPredictions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.PredictionType)
                .HasColumnName("PredictionType")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.TargetEntity)
                .HasColumnName("TargetEntity")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.TargetId)
                .HasColumnName("TargetId")
                .IsRequired(false);

            builder.Property(x => x.ConfidenceScore)
                .HasColumnName("ConfidenceScore")
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.PredictionDetails)
                .HasColumnName("PredictionDetails")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

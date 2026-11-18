using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.ToTable("Tournaments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.TournamentName)
                .HasColumnName("TournamentName")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate")
                .IsRequired();

            builder.Property(x => x.EndDate)
                .HasColumnName("EndDate")
                .IsRequired();

            builder.Property(x => x.EntryFee)
                .HasColumnName("EntryFee")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.TotalPrize)
                .HasColumnName("TotalPrize")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

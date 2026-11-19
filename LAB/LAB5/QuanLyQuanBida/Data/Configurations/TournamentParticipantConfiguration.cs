using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class TournamentParticipantConfiguration : IEntityTypeConfiguration<TournamentParticipant>
    {
        public void Configure(EntityTypeBuilder<TournamentParticipant> builder)
        {
            builder.ToTable("TournamentParticipants");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.TournamentId)
                .HasColumnName("TournamentId")
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property(x => x.SeedNumber)
                .HasColumnName("SeedNumber")
                .IsRequired(false);

            builder.Property(x => x.RegisteredDate)
                .HasColumnName("RegisteredDate")
                .IsRequired();

            builder.Property(x => x.FinalRank)
                .HasColumnName("FinalRank")
                .IsRequired(false);

        }
    }
}

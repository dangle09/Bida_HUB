using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.TournamentId)
                .HasColumnName("TournamentId")
                .IsRequired();

            builder.Property(x => x.BilliardTableId)
                .HasColumnName("BilliardTableId")
                .IsRequired();

            builder.Property(x => x.RoundName)
                .HasColumnName("RoundName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Player1Id)
                .HasColumnName("Player1Id")
                .IsRequired();

            builder.Property(x => x.Player2Id)
                .HasColumnName("Player2Id")
                .IsRequired();

            builder.Property(x => x.ScorePlayer1)
                .HasColumnName("ScorePlayer1")
                .IsRequired(false);

            builder.Property(x => x.ScorePlayer2)
                .HasColumnName("ScorePlayer2")
                .IsRequired(false);

            builder.Property(x => x.WinnerId)
                .HasColumnName("WinnerId")
                .IsRequired(false);

            builder.Property(x => x.MatchTime)
                .HasColumnName("MatchTime")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnName("Status")
                .HasMaxLength(20)
                .IsRequired();

        }
    }
}

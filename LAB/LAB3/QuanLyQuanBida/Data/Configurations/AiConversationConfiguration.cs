using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class AiConversationConfiguration : IEntityTypeConfiguration<AiConversation>
    {
        public void Configure(EntityTypeBuilder<AiConversation> builder)
        {
            builder.ToTable("AiConversations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .IsRequired(false);

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired(false);

            builder.Property(x => x.SessionId)
                .HasColumnName("SessionId")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.UserPrompt)
                .HasColumnName("UserPrompt")
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.AiResponse)
                .HasColumnName("AiResponse")
                .HasMaxLength(4000)
                .IsRequired();

            builder.Property(x => x.ModelName)
                .HasColumnName("ModelName")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

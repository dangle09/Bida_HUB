using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Username)
                .HasColumnName("Username")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasColumnName("FullName")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(x => x.Avatar)
                .HasColumnName("Avatar")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}

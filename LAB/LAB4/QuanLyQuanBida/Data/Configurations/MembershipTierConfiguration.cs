using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class MembershipTierConfiguration : IEntityTypeConfiguration<MembershipTier>
    {
        public void Configure(EntityTypeBuilder<MembershipTier> builder)
        {
            builder.ToTable("MembershipTiers");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.MinPoints)
                .HasColumnName("MinPoints")
                .IsRequired();

            builder.Property(x => x.DiscountRate)
                .HasColumnName("DiscountRate")
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

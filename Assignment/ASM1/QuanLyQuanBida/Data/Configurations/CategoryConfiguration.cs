using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.Code)
                .HasColumnName("Code")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasColumnName("Image")
                .HasMaxLength(255)
                .IsRequired(false);

        }
    }
}

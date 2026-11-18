using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();

            builder.Property(x => x.PermissionId)
                .HasColumnName("PermissionId")
                .IsRequired();

        }
    }
}

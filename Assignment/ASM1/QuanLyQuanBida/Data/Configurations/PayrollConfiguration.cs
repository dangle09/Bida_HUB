using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyQuanBida.Models;

namespace QuanLyQuanBida.Data.Configurations
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.ToTable("Payrolls");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.SalaryMonth)
                .HasColumnName("SalaryMonth")
                .IsRequired();

            builder.Property(x => x.SalaryYear)
                .HasColumnName("SalaryYear")
                .IsRequired();

            builder.Property(x => x.TotalHours)
                .HasColumnName("TotalHours")
                .HasColumnType("decimal(6,2)")
                .IsRequired();

            builder.Property(x => x.BaseSalary)
                .HasColumnName("BaseSalary")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Allowance)
                .HasColumnName("Allowance")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Bonus)
                .HasColumnName("Bonus")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Penalty)
                .HasColumnName("Penalty")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.NetSalary)
                .HasColumnName("NetSalary")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

        }
    }
}

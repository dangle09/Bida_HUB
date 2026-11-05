using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DemoCodeFirst.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Khai báo 2 bảng trong cơ sở dữ liệu
    public DbSet<LopHoc> LopHocs { get; set; }
    public DbSet<SinhVien> SinhViens { get; set; }
}

// Cấu hình kết nối trực tiếp vào (localdb)\MSSQLLocalDB
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DemoCodeFirstDB;Trusted_Connection=True;TrustServerCertificate=True;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
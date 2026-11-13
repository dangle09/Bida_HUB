using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanBida.Data;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình kết nối SQL Server và DbContext chuẩn kiến trúc ASP.NET Core MVC + Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QuanLyQuanBidaContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký dịch vụ MVC (Controllers with Views)
builder.Services.AddControllersWithViews();

// =========================================================================
// PHẦN I: PHÂN QUYỀN VÀ ĐĂNG NHẬP (BÀI 1 - 4 ĐIỂM)
// =========================================================================

// 1. Xác thực dựa trên Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        // Xác định thời gian tồn tại của cookie là 24 giờ
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.SlidingExpiration = true;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    });

// 2. Phân quyền: Cấu hình các Policy vai trò
builder.Services.AddAuthorization(options =>
{
    // Policy cho phép cả Employee và Manager (dùng khi chấp nhận một trong hai)
    options.AddPolicy("EmployeeOrManager", policy =>
        policy.RequireRole("Employee", "Manager"));

    // Policy chỉ cho phép Manager
    options.AddPolicy("ManagerOnly", policy =>
        policy.RequireRole("Manager"));
});

var app = builder.Build();

// Tự động khởi tạo cơ sở dữ liệu và nạp dữ liệu mẫu qua EF Core DbContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<QuanLyQuanBidaContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Lỗi xảy ra trong quá trình khởi tạo dữ liệu mẫu.");
    }
}

// Cấu hình HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// QUAN TRỌNG: Xác thực (Authentication) phải được gọi trước Phân quyền (Authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

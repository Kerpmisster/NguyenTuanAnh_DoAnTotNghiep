using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNoiThatHoangGia.Data;
using QuanLyNoiThatHoangGia.Handlers;
using QuanLyNoiThatHoangGia.Middlewares;

namespace QuanLyNoiThatHoangGia;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient();
        //builder.Services.AddHttpClient("AuthorizedClient", client =>
        //{
        //    client.BaseAddress = new Uri("https://localhost:7261/");
        //}).AddHttpMessageHandler<AuthTokenHandler>();   
        builder.Services.AddTransient<AuthTokenHandler>();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(1);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.Name = "NoiThatHoangGia";
        });
        // Cấu hình Authentication với Cookie
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "NoiThatHoangGia"; // Tùy chỉnh tên cookie nếu muốn
                options.ExpireTimeSpan = TimeSpan.FromHours(1); // Thời gian hết hạn của cookie
                options.SlidingExpiration = true; // Tự động gia hạn khi người dùng hoạt động
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.HttpOnly = true;
            })
            /*.AddCookie()*/;

        // Thêm Authorization
        builder.Services.AddAuthorization();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseMiddleware<AdminJwtMiddleware>();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

      


        app.MapControllerRoute(
            name: "default-root", // Khi truy cập /
            pattern: "",
            defaults: new { area = "User", controller = "Home", action = "Index" });

        //app.MapControllerRoute(
        //    name: "user",
        //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
        //    defaults: new { area = "Users", controller = "Home", action = "Index" }
        //);

        app.MapControllerRoute(
            name: "admins",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


        app.MapRazorPages();

        app.Run();
    }
}

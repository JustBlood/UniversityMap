using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using UniversityMap.Data;
using UniversityMap.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        const string AuthScheme = "cookie";
        const string AuthScheme2 = "cookie2";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<UniversityMapContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityMapContext") ?? throw new InvalidOperationException("Connection string 'UniversityMapContext' not found.")));

        // Add services to the container.
        builder.Services.AddIdentity<User, IdentityRole>(o =>
        {
            o.User.RequireUniqueEmail = true;

            o.Password.RequireDigit = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireUppercase = false;
            o.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<UniversityMapContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddControllersWithViews();
        builder.Services.AddAuthentication(AuthScheme)
            .AddCookie(AuthScheme)
            .AddCookie(AuthScheme2);

        
        builder.Services.AddAuthorization();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using(var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            await SeedData.Initialize(userManager, roleManager);
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllerRoute(
            name: "maps",
            pattern: "maps/{building?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
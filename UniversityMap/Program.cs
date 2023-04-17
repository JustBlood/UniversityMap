using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection.StackExchangeRedis;

using UniversityMap.Data;
using UniversityMap.Models;
using System.Configuration;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

internal class Program
{
    private static async Task Main(string[] args)
    {
        const string AuthScheme = "cookie";

        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddDbContext<UniversityMapContext>(options =>
        //    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreConnection") ?? throw new InvalidOperationException("Connection string 'UniversityMapContext' not found.")));
        builder.Services.AddDbContext<UniversityMapContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new InvalidOperationException("Connection string 'PostgreConnection' not found")));

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
            .AddCookie(AuthScheme);
        
        builder.Services.AddAuthorization();

        builder.Services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(@"/var/keys"))
            .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            })
            .SetApplicationName("MyApp");

        var app = builder.Build();
        //app.UseHttpsRedirection();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<UniversityMapContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            await SeedData.Initialize(userManager, roleManager);
        }


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
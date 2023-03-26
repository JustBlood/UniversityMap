using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using UniversityMap.Data;

const string AuthScheme = "cookie";
const string AuthScheme2 = "cookie2";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UniversityMapContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityMapContext") ?? throw new InvalidOperationException("Connection string 'UniversityMapContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(AuthScheme)
    .AddCookie(AuthScheme)
    .AddCookie(AuthScheme2);

//builder.Services.AddAuthorization(builder =>
//{
//    builder.AddPolicy("role", pb =>
//    {
//        pb.RequireAuthenticatedUser()
//        .AddAuthenticationSchemes(AuthScheme)
//        .RequireClaim("rl", "user");
//    });
//});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
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

using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MovieWebsite.Data;
using MovieWebsite.Enums;
using MovieWebsite.Filters;
using MovieWebsite.Service;
using System;
using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//----------------------------------------------------------
builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDb")));
//----------------------------------------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
//----------------------------------------------------------
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalFilter>();
});
builder.Services.AddRazorPages();
//----------------------------------------------------------
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddCookie()
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId= builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
})
.AddFacebook(facebookOptions =>
{
    facebookOptions.ClientId = builder.Configuration["Authentication:Facebook:ClientId"]; ;
    facebookOptions.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]; ;
})
;
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole(roles: Roles.Admin.ToString());
        policy.RequireRole(roles: Roles.SuperAdmin.ToString());
    });
});
//----------------------------------------------------------
builder.Services.AddTransient<IEmailSender, EmailSender>();
//----------------------------------------------------------

var app = builder.Build();
//seed data
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    //identity
    await ContextSeed.SeedRolesAsync(roleManager);
    await ContextSeed.SeedSuperAdminAsync(userManager,roleManager);
    //tables
    await ContextSeed.SeedMovieRole(services);
    await ContextSeed.SeedMovieStatus(services);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//-------
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseExceptionHandler("/Error");
//------
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new
    {
        controller="Movies",
        action="Index",
    });
app.MapRazorPages();

app.Run();

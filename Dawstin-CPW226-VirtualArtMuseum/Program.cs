using Dawstin_CPW226_VirtualArtMuseum.VirtualArt_Data;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register database context using SQL Server connection string
builder.Services.AddDbContext<VirtualArtMuseum>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VirtualArtMuseum")));

// Enable developer-friendly error pages for database-related issues
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure ASP.NET Core Identity for authentication
builder.Services.AddDefaultIdentity<Dawstin_CPW226_VirtualArtMuseum.Models.ApplicationUser>(options =>
{
    // Allows users to sign in without email confirmation (modify if needed)
    options.SignIn.RequireConfirmedAccount = false;
})
    // Store user accounts in the BaseballShop database using Entity Framework
    .AddEntityFrameworkStores<VirtualArtMuseum>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

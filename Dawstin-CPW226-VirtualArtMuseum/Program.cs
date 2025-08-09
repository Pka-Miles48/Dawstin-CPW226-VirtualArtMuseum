using Dawstin_CPW226_VirtualArtMuseum.VirtualArt_Data;
using Dawstin_CPW226_VirtualArtMuseum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Identity with Roles
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<VirtualArtMuseum>()
    .AddDefaultTokenProviders(); // Adds support for password reset, email confirmation, etc.

// Register database context
builder.Services.AddDbContext<VirtualArtMuseum>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("VirtualArtMuseum"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);


// Optional: helpful during development
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Seed roles if they don't exist
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Admin", "Artist" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Domain.Entities;
using Shopping.Infrastructure.Repositories;
using Shopping.Persistence;
using Shopping.Persistence.Data;
using Shopping.Persistence.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection22322700"));
});

//TODO: make strongest password
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<DataContext>();

builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

var app = builder.Build();

SeedData();

//Inject SeedDb on Program class
void SeedData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service.SeedAsync().Wait();
    }

}

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

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Entities;
using Shopping.Interface;
using Shopping.Data;
using Shopping.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection22322700"));
});

//TODO: make strongest password
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    cfg.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
    cfg.SignIn.RequireConfirmedEmail = true;
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    cfg.Lockout.MaxFailedAccessAttempts = 3;
    cfg.Lockout.AllowedForNewUsers = true;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Accounts/NotAuthorized";
    opt.AccessDeniedPath = "/Accounts/NotAuthorized";
});



builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
builder.Services.AddScoped<IMailHelper, MailHelper>();
builder.Services.AddScoped<IOrderHelper, OrderHelper>();
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

app.UseStatusCodePagesWithReExecute("/error/{0}");//Codigo de error para el page notfound
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

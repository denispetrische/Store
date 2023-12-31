using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Data;
using Store.Web.Seeders;
using AutoMapper;
using Store.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("StoreWebContextConnection") ?? throw new InvalidOperationException("Connection string 'StoreWebContextConnection' not found.");

builder.Services.AddDbContext<StoreWebContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StoreWebContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepo<Product>, ProductRepo>();
builder.Services.AddScoped<IHistoryNoteRepo<HistoryNote>, HistoryNoteRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Logging.SetMinimumLevel(LogLevel.Debug);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreWebContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

PrepDb.PrepDbInfo(app); // Seeder

app.Run();

using Microsoft.EntityFrameworkCore;
using Zlatara.Data;
using Microsoft.AspNetCore.Identity;
using Zlatara.Models;
using Microsoft.Extensions.DependencyInjection;
using Zlatara.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ZlataraContext>(options=>options.UseSqlServer(
	builder.Configuration.GetConnectionString("Dafault")
	));

builder.Services.AddIdentity<IdentityRadnik, IdentityRole>(options => 
{   
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = false; 
    options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 4;
}).AddEntityFrameworkStores<ZlataraContext>()
.AddDefaultTokenProviders();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Identity/Account/Login";
	options.LogoutPath = "/Identity/Account/Logout";
	options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(100);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Prodaja}/{action=Index}/{id?}");
	

app.Run();

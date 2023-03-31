using ECommerce.Clients.WEB.Handler;
using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Clients.WEB.Services.UserServices;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings")); //options pattern
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));


builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IIdentityService, IdentityServices>();


builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>().IdentityUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "Auth/SignIn";
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
    opt.Cookie.Name = "ecoomercewebcookie";
}) ;


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

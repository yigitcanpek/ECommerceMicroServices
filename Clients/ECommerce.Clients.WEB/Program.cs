using ECommerce.Clients.WEB.Handler;
using ECommerce.Clients.WEB.Handlers;
using ECommerce.Clients.WEB.Helpers;
using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.GeneralServices;
using ECommerce.Clients.WEB.Services.IdentityServices;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Clients.WEB.Services.UserServices;
using ECommerce.Shared.Services;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings")); //options pattern
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

ServiceApiSettings serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IClientCredentialTokenService, ClientCredentialService>();
builder.Services.AddHttpClient<IIdentityService, IdentityServices>();
builder.Services.AddHttpClient<ICatalogService, CatalogService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.Catalog.path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
{
    opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.PhotoStock.path}");
}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

builder.Services.AddHttpClient<IUserService, UserService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>().IdentityUrl);
}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
{
    opt.LoginPath = "/Auth/SignIn";
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

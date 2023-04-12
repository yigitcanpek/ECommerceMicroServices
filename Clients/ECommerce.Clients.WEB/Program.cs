using ECommerce.Clients.WEB.Extentions;
using ECommerce.Clients.WEB.Handler;
using ECommerce.Clients.WEB.Handlers;
using ECommerce.Clients.WEB.Helpers;
using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.GeneralServices;
using ECommerce.Clients.WEB.Services.IdentityServices;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Clients.WEB.Services.UserServices;
using ECommerce.Clients.WEB.Validators;
using ECommerce.Shared.Services;
using FluentValidation.AspNetCore;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings")); //options pattern
builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

builder.Services.AddControllersWithViews().AddFluentValidation(fv=> fv.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>());

builder.Services.AddSingleton<PhotoHelper>();

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddScoped<ClientCredentialTokenHandler>();
builder.Services.AddAccessTokenManagement();

builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClientServices(builder);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
}
app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

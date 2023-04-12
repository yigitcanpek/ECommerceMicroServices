using ECommerce.Clients.WEB.Handler;
using ECommerce.Clients.WEB.Handlers;
using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.GeneralServices;
using ECommerce.Clients.WEB.Services.IdentityServices;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Clients.WEB.Services.UserServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ECommerce.Clients.WEB.Extentions
{
    public static class ServicesExtention
    {
        public static void AddHttpClientServices(this IServiceCollection services,WebApplicationBuilder builder)
        {
           
           
            ServiceApiSettings serviceApiSettings = builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialService>();
            services.AddHttpClient<IIdentityService, IdentityServices>();
            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.Basket.path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.Catalog.path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.PhotoStock.path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>().IdentityUrl);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();


            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GateWayUrl}/{serviceApiSettings.Discount.path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = "/Auth/SignIn";
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;
                opt.Cookie.Name = "ecoomercewebcookie";
            });

        }
    }
}

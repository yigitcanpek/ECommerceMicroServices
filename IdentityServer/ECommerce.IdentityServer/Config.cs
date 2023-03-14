// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace ECommerce.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource("resource_basket"){Scopes={ "basket_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };



        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                        new IdentityResources.Email(),
                        new IdentityResources.OpenId(), //jwt sub claim
                        new IdentityResources.Profile(),
                        new IdentityResource(){Name="role",DisplayName="Roles",Description="Kullanıcı rolleri",UserClaims=new[]{"role" } }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission","Catalog API full access"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API full access"),
                new ApiScope("basket_fullpermission","Basket API full access"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientName="ECommerceWebMvc",
                    ClientId="WebMvcClient",
                    ClientSecrets={new Secret("secret".Sha512())},
                    AllowedGrantTypes=GrantTypes.ClientCredentials, //With refresh token
                    AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
                },
                     new Client
                {
                    ClientName="ECommerceWebMvc",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets={new Secret("secret".Sha512())},
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={"basket_fullpermission",IdentityServerConstants.StandardScopes.Email,IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,IdentityServerConstants.StandardScopes.OfflineAccess,IdentityServerConstants.LocalApi.ScopeName,"roles"/*Refresh tokken*/ },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RefreshTokenUsage=TokenUsage.ReUse
                        
                }
            };
    }
}
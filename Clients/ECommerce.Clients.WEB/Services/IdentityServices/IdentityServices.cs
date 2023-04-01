using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;
using System.Text.Json;

namespace ECommerce.Clients.WEB.Services.IdentityServices
{
    public class IdentityServices : IIdentityService
    {
        private readonly HttpClient _httpclient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSetting;
        private readonly ServiceApiSettings _serviceApiSettings;

        public IdentityServices(HttpClient client, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSetting, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _httpclient = client;
            _httpContextAccessor = httpContextAccessor;
            _clientSetting = clientSetting.Value;
            _serviceApiSettings = serviceApiSettings.Value;
        }





        public async Task<TokenResponse> GetAccessTokenByRefreshToken()
        {
            DiscoveryDocumentResponse discoveryEndPoint = await _httpclient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discoveryEndPoint.IsError)
            {
                throw discoveryEndPoint.Exception;
            }

            string refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest()
            {
                ClientId = _clientSetting.WebClientForUser.ClientId,
                ClientSecret = _clientSetting.WebClientForUser.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            TokenResponse token = await _httpclient.RequestRefreshTokenAsync(refreshTokenRequest);

            if (token.IsError)
            {
                return null;
                //throw new Exception(token.Error);
            }

            List<AuthenticationToken> authenticationtokens = new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
                new AuthenticationToken{Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken },
                new AuthenticationToken{Name=OpenIdConnectParameterNames.ExpiresIn,Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            };

            AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();

            AuthenticationProperties properties = authenticateResult.Properties;
            properties.StoreTokens(authenticationtokens);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal, properties);

            return token;

        }

        public async Task RevokeRefreshToken()
        {
            DiscoveryDocumentResponse discoveryEndPoint = await _httpclient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discoveryEndPoint.IsError)
            {
                throw discoveryEndPoint.Exception;
            }

            Task<string> refreshToken = _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            TokenRevocationRequest tokenRevocationRequest = new TokenRevocationRequest
            {
                ClientId = _clientSetting.WebClientForUser.ClientId,
                ClientSecret = _clientSetting.WebClientForUser.ClientSecret,
                Address = discoveryEndPoint.RevocationEndpoint,
                Token = refreshToken.ToString(),
                TokenTypeHint = "refresh_token"
            };

            await _httpclient.RevokeTokenAsync(tokenRevocationRequest);

        }

        public async Task<Response<bool>> SıgnIn(SignInInput signInInput)
        {
            DiscoveryDocumentResponse discoveryEndPoint = await _httpclient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discoveryEndPoint.IsError)
            {
                throw discoveryEndPoint.Exception;
            }

            PasswordTokenRequest passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSetting.WebClientForUser.ClientId,
                ClientSecret = _clientSetting.WebClientForUser.ClientSecret,
                UserName = signInInput.Email,
                Password = signInInput.Password,
                Address = discoveryEndPoint.TokenEndpoint
            };

            TokenResponse token = await _httpclient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (token.IsError)
            {
                //throw token.Exception;
                string responseContent = await token.HttpResponse.Content.ReadAsStringAsync();

                ErrorDto errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return Response<bool>.Fail(errorDto.Errors, 400);
            }

            UserInfoRequest userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndPoint.UserInfoEndpoint
            };

            UserInfoResponse userInfo = await _httpclient.GetUserInfoAsync(userInfoRequest);

            if (userInfo.IsError)
            {
                throw userInfo.Exception;
            }


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationProperties authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken{Name=OpenIdConnectParameterNames.AccessToken,Value=token.AccessToken},
                new AuthenticationToken{Name=OpenIdConnectParameterNames.RefreshToken,Value=token.RefreshToken },
                new AuthenticationToken{Name=OpenIdConnectParameterNames.ExpiresIn,Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)}
            });

            authenticationProperties.IsPersistent = signInInput.IsRemember;
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
            return Response<bool>.Success(200);

        }

    }
}

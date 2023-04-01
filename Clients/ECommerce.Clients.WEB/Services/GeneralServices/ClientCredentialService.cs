using ECommerce.Clients.WEB.Models;
using ECommerce.Clients.WEB.Services.Interfaces;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class ClientCredentialService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly HttpClient _httpclient;

        public ClientCredentialService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpclient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpclient = httpclient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            ClientAccessToken currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken");

            if (currentToken!=null)
            {
                return currentToken.AccessToken;
            }

            DiscoveryDocumentResponse discoveryEndPoint = await _httpclient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discoveryEndPoint.IsError)
            {
                throw discoveryEndPoint.Exception;
            }

            ClientCredentialsTokenRequest clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            TokenResponse newToken = await _httpclient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);

            if (newToken.IsError)
            {
                throw newToken.Exception;
            }

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn);
            return newToken.AccessToken;

        }
    }
}

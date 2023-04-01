using ECommerce.Clients.WEB.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using static IdentityModel.OidcConstants;
using IdentityModel.Client;
using ECommerce.Clients.WEB.Exceptions;

namespace ECommerce.Clients.WEB.Handler
{
    public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService     _identityService;
        private readonly ILogger<ResourceOwnerPasswordTokenHandler> _logger;

        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ILogger<ResourceOwnerPasswordTokenHandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode==System.Net.HttpStatusCode.Unauthorized)
            {
                IdentityModel.Client.TokenResponse tokenResponse = await _identityService.GetAccessTokenByRefreshToken();

                if (tokenResponse!=null)
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }


            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //You have to throw exception
                throw new UnAuthorizeException();
            }

            return response;

        }


    }

}

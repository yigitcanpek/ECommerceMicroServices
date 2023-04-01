using ECommerce.Clients.WEB.Exceptions;
using ECommerce.Clients.WEB.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace ECommerce.Clients.WEB.Handlers
{
    public class ClientCredentialTokenHandler:DelegatingHandler
    {
        private readonly IClientCredentialTokenService _clientCredentialTokenService;

        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetAccessTokenAsync());

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;
        }
    }
}

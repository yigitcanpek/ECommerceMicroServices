using ECommerce.Clients.WEB.Models;
using ECommerce.Shared.Dtos;
using IdentityModel.Client;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SıgnIn(SignInInput signInInput);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}

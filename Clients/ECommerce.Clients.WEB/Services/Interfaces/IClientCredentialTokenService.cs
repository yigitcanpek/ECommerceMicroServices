namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetAccessTokenAsync();
    }
}

using ECommerce.Clients.WEB.Models;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}

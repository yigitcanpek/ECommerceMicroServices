using ECommerce.Clients.WEB.Models.BaskesViewModels;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);
        Task<BasketViewModel> Get();
        Task<bool> Delete();
        Task AddBasketItem(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItem(string id);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();

    }
}

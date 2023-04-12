using ECommerce.Clients.WEB.Models.DiscountViewModels;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}

using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpclient;

        public BasketService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public Task AddBasketItem(BasketViewModel basketViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<BasketViewModel> Get()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveBasketItem(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            throw new NotImplementedException();
        }
    }
}

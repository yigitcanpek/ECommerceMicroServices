using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpclient;

        public BasketService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            BasketViewModel basket = await Get();
            if (basket!= null)
            {
                if (basket.basketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.basketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                BasketViewModel basketViewModel = new BasketViewModel();
                basketViewModel.basketItems.Add(basketItemViewModel);
            }
            await SaveOrUpdate(basket);
        }

        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete()
        {
            HttpResponseMessage response = await _httpclient.DeleteAsync("baskets");
            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            HttpResponseMessage response = await _httpclient.GetAsync("baskets");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

                Response<BasketViewModel> basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

                return basketViewModel.Data;
            
        }

        public async Task<bool> RemoveBasketItem(string id)
        {
            BasketViewModel Basket = await Get();

            if (Basket==null)
            {
                return false;
            }

            BasketItemViewModel deleteItem = Basket.basketItems.FirstOrDefault(x=> x.CourseId==id);

            if (deleteItem ==null)
            {
                return false;
            }

            bool deletedItem = Basket.basketItems.Remove(deleteItem);

            if (!deletedItem)
            {
                return false;
            }
            if (!Basket.basketItems.Any())
            {
                Basket.DiscountCode = null;
            }

            return await SaveOrUpdate(Basket);

            

        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            HttpResponseMessage response = await _httpclient.PostAsJsonAsync<BasketViewModel>("baskets",basketViewModel);

            return response.IsSuccessStatusCode;
        }
    }
}

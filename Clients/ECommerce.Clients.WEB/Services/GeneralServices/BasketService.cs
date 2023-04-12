using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Models.DiscountViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpclient;
        private readonly IDiscountService _discountService;


        public BasketService(HttpClient httpclient, IDiscountService discountService)
        {
            _httpclient = httpclient;
            _discountService = discountService;
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

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            BasketViewModel basket = await Get();

            if (basket == null || basket.DiscountCode == null)
            {
                return false;
            }

            DiscountViewModel hasDiscount = await _discountService.GetDiscount(discountCode);

            if (hasDiscount ==null)
            {
                return false;
            }

            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode = hasDiscount.Code;
            await SaveOrUpdate(basket);
            return true;

        }

        public async Task<bool> CancelApplyDiscount()
        {
            BasketViewModel basket = await Get();
            if (basket ==null || basket.DiscountCode == null)
            {
                return false;
            }
            basket.DiscountCode = null;
            await SaveOrUpdate(basket);
            return true;
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

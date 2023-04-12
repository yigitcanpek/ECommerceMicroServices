using ECommerce.Clients.WEB.Models.DiscountViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class DiscountService : IDiscountService
    {

        private readonly HttpClient _httpclient;

        public DiscountService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
             HttpResponseMessage response = await _httpclient.GetAsync($"discounts/GetByCode/{discountCode}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();
            return discount.Data;
        }
    }
}

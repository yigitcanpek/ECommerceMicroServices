using ECommerce.Services.Basket.Dtos;
using ECommerce.Services.Basket.Services;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Services.Basket.Controllers
{
    
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;

        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            basketDto.UserId = _sharedIdentityService.GetUserId;


            Shared.Dtos.Response<bool> response = await _basketService.SaveOrUpdate(basketDto);

            return CreateActionResultInstance<bool>(response);/**/
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }
    }
}

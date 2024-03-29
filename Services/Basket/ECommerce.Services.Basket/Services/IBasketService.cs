﻿using ECommerce.Services.Basket.Dtos;
using ECommerce.Shared.Dtos;

namespace ECommerce.Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);

        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);

        Task<Response<bool>> Delete(string userId);
    }
}

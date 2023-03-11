using ECommerce.Services.Basket.Dtos;
using ECommerce.Shared.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerce.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }


        public async Task<Response<bool>> Delete(string userId)
        {
            Boolean status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Not Found", 404);

        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            RedisValue existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            Boolean status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}

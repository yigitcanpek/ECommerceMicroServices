using ECommerce.Shared.Dtos;
using System.Reflection;

namespace ECommerce.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Entities.Discount>>> GetlAll();

        Task<Response<Entities.Discount>> GetlById(int id);

        Task<Response<NoContent>> Save(Entities.Discount discount);

        Task<Response<NoContent>> Update(Entities.Discount discount);

        Task<Response<NoContent>> Delete(int id);

        Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userId);


    }
}

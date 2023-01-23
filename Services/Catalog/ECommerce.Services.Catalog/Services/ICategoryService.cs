using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;
using ECommerce.Shared.Dtos;

namespace ECommerce.Services.Catalog.Services
{
    internal interface  ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(Category category);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}

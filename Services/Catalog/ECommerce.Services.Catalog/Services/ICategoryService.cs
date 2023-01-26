using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Models;
using ECommerce.Shared.Dtos;

namespace ECommerce.Services.Catalog.Services
{
    public interface  ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();
        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}

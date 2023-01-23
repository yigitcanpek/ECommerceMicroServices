using ECommerce.Services.Catalog.Dtos;
using ECommerce.Shared.Dtos;
using System.Threading.Tasks;

namespace ECommerce.Services.Catalog.Services
{
    internal interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetById(string id);
        Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}

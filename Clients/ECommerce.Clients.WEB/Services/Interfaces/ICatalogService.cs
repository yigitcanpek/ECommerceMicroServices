using ECommerce.Clients.WEB.Models.CatalogViewModels;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CategoryViewModel>> GetAllCategoriesAsync();

        Task<List<CourseViewModel>> GetAllCourseAsync();
        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);
        Task<CourseViewModel> GetByCourseId(string courseId);
        

        Task<bool> UpdateCourseAsync(CourseUpdateViewModel courseUpdateViewModel);
        Task<bool> AddCourseAsync(CourseCreateInputViewModel courseCreateInputViewModel);
        Task<bool> DeleteCourseAsync(string courseId);

        

    }
}

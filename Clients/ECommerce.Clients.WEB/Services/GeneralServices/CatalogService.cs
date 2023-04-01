using ECommerce.Clients.WEB.Models.CatalogViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> AddCourseAsync(CourseCreateInputViewModel courseCreateInputViewModel)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync<CourseCreateInputViewModel>("courses",courseCreateInputViewModel);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"courses/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            Task<HttpResponseMessage> response = _client.GetAsync("categories");
            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            Response<List<CategoryViewModel>> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            // http:localhost:5000/services/catalog/services
            Task<HttpResponseMessage> response = _client.GetAsync("courses");
            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            Response<List<CourseViewModel>> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;

        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            Task<HttpResponseMessage> response = _client.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            Response<List<CourseViewModel>> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            Task<HttpResponseMessage> response = _client.GetAsync($"courses/{courseId}");
            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            Response<CourseViewModel> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateViewModel courseUpdateViewModel)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync<CourseUpdateViewModel>("courses", courseUpdateViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}

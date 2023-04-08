using ECommerce.Clients.WEB.Helpers;
using ECommerce.Clients.WEB.Models.CatalogViewModels;
using ECommerce.Clients.WEB.Models.PhotoStocks;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;
        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> AddCourseAsync(CourseCreateInputViewModel courseCreateInputViewModel)
        {
            PhotoViewModel resultPhotoService = await _photoStockService.UploadPhoto(courseCreateInputViewModel.PhotoFormFile);
            if (resultPhotoService != null) 
            {
                courseCreateInputViewModel.Picture = resultPhotoService.Url;
            }


            HttpResponseMessage response = await _client.PostAsJsonAsync<CourseCreateInputViewModel>("courses",courseCreateInputViewModel);


            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"courses?Id={courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoriesAsync()
        {
            Task<HttpResponseMessage> response = _client.GetAsync("categories");
           

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

            responseSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return responseSuccess.Data;

        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            Task<HttpResponseMessage> response =   _client.GetAsync($"courses/GetAllByUserId/{userId}");
            

            Response<List<CourseViewModel>> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            responseSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return  responseSuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            Task<HttpResponseMessage> response = _client.GetAsync($"courses/{courseId}");
            

            Response<CourseViewModel> responseSuccess = await response.Result.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            if (!response.IsCompletedSuccessfully)
            {
                return null;
            }

            return responseSuccess.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateViewModel courseUpdateViewModel)
        {
            PhotoViewModel resultPhotoService = await _photoStockService.UploadPhoto(courseUpdateViewModel.PhotoFormFile);
            if (resultPhotoService != null)
            {

                _photoStockService.DeletePhoto(courseUpdateViewModel.Picture);
                courseUpdateViewModel.Picture = resultPhotoService.Url;
            }

            HttpResponseMessage response = await _client.PutAsJsonAsync<CourseUpdateViewModel>("courses", courseUpdateViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}

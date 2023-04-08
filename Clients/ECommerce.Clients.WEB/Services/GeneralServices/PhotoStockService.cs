using ECommerce.Clients.WEB.Models.PhotoStocks;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Dtos;

namespace ECommerce.Clients.WEB.Services.GeneralServices
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpclient;

        public PhotoStockService(HttpClient httpclient)
        {
            _httpclient = httpclient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            HttpResponseMessage response = await _httpclient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            // photo name going to = 137621567a12321.jps
            string randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using MemoryStream ms = new MemoryStream();

            await photo.CopyToAsync(ms);
            MultipartFormDataContent multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()),"photo",randomFilename);

            HttpResponseMessage response = await _httpclient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            Response<PhotoViewModel> responsesuccess = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();
            return responsesuccess.Data;
            
        }
    }
}

using ECommerce.Clients.WEB.Models.PhotoStocks;

namespace ECommerce.Clients.WEB.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile photo);
        Task<Boolean> DeletePhoto(string photoUrl);


    }
}

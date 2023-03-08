using Ecommerce.Services.PhotoStock.Dtos;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo!=null&& photo.Length>0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

                using(FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream,cancellationToken);
                }
                //http://www.photostock.api.com/photos/asdada.jpg
                string returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url= returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
        }
    }
}

using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Clients.WEB.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CourseController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }
    }
}

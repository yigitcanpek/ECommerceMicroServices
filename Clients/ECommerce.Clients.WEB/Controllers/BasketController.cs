using ECommerce.Clients.WEB.Models.BaskesViewModels;
using ECommerce.Clients.WEB.Models.CatalogViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Clients.WEB.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;

        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            CourseViewModel course = await _catalogService.GetByCourseId(courseId);

            BasketItemViewModel basketItemViewModel = new BasketItemViewModel{ CourseId=course.Id ,CourseName = course.Name,Price=course.Price};

            await _basketService.AddBasketItem(basketItemViewModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            await _basketService.RemoveBasketItem(courseId);
            return RedirectToAction(nameof(Index));
        }

    }
}

using ECommerce.Clients.WEB.Models.CatalogViewModels;
using ECommerce.Clients.WEB.Services.Interfaces;
using ECommerce.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

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
            List<CourseViewModel> courseViewModels = await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId);
            return View(courseViewModels);
        }

        public async Task<IActionResult> CreateCourse()
        {
            List<CategoryViewModel> categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseCreateInputViewModel courseCreateInputViewModel)
        {
            List<CategoryViewModel> categories = await _catalogService.GetAllCategoriesAsync();
            if (ModelState.IsValid)
            {
                return View();
            }

            courseCreateInputViewModel.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.AddCourseAsync(courseCreateInputViewModel);
            return RedirectToAction(nameof(Index));

        }

        
        public async Task<IActionResult> CourseUpdate(string id)
        {
            CourseViewModel course = await _catalogService.GetByCourseId(id);
            List<CategoryViewModel> categories = await _catalogService.GetAllCategoriesAsync();
            

            if (course == null)
            {
                RedirectToAction(nameof(Index));
            }

            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);
            

            
                CourseUpdateViewModel courseUpdateViewModel = new()
                {
                    Id = course.Id,
                    Name = course.Name,
                    CategoryId = course.CategoryId,
                    Feature = course.Feature,
                    Price = course.Price,
                    UserId = course.UserId,
                    Description = course.Description,
                    Picture= course.Picture,
                };

            
            return View(courseUpdateViewModel);
           
        }

        [HttpPost]
        public async Task<IActionResult> CourseUpdate(CourseUpdateViewModel courseUpdateViewModel)
        {
            List<CategoryViewModel> categories = await _catalogService.GetAllCategoriesAsync();
            ViewBag.categoryList = new SelectList(categories,"Id","Name",courseUpdateViewModel.Id);
            if (!ModelState.IsValid)
            {
                var modelStateErrors = ModelState.Where(x => x.Value.Errors.Any()).Select(x => new { x.Key, x.Value });
                return View();
            }
            
            await _catalogService.UpdateCourseAsync(courseUpdateViewModel);
            return RedirectToAction(nameof(Index));

        }

        
        public async Task<IActionResult> CourseDelete(string id)
        {
            await _catalogService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

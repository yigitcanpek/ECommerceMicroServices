using ECommerce.Services.Catalog.Dtos;
using ECommerce.Services.Catalog.Services;
using ECommerce.Shared.ControllerBases;
using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Services.Catalog.Controllers
{
    
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response<List<CategoryDto>> response = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Response<CategoryDto> response = await _categoryService.GetByIdAsync(id);
            return CreateActionResultInstance(response);  
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            Response<CategoryDto> response = await _categoryService.CreateAsync(categoryDto);
            return CreateActionResultInstance(response); 
        }
    }
}

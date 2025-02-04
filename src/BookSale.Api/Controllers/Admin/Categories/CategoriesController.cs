using BookSale.Api.Params;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Services.Admin.Categoris;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSale.Api.Controllers.Admin.Categories
{
    [Route("v1")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("admin/categories")]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoriesRequestDto categoriesDto)
        {
            var category = await _categoryService.CreateCategoryAsync(categoriesDto);
            return StatusCode(201,  category);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategoryAsync()
        {
            var category = await _categoryService.getAllCategoryAsync();
            return StatusCode(200, category);
        }

        [HttpPut("admin/categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync(CategoryyIdParam param, [FromBody] UpdateCategoriesDto updateCategories)
        {
            var category = await _categoryService.UpdateCategoryAsync(int.Parse(param.Id), updateCategories);
            return StatusCode(200, category);
        }

        [HttpDelete("admin/categories/{id}")]
        public async Task<IActionResult> GetCategoryAsync(CategoryyIdParam param)
        {
            await _categoryService.DeleteCategoryAsync(int.Parse(param.Id));
            return NoContent();
        }
    }
}

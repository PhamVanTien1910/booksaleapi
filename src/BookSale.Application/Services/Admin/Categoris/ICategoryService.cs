using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Categoris
{
    public interface ICategoryService
    {
        Task<CategoriesResponseDto> CreateCategoryAsync(CategoriesRequestDto categoriesDto);
        Task<CategoriesResponseDto> UpdateCategoryAsync(int id, UpdateCategoriesDto updateCategories);
        Task<List<CategoriesResponseDto>> getAllCategoryAsync();
        Task DeleteCategoryAsync(int id);
    }
}

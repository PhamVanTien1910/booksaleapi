using AutoMapper;
using dotnet_boilerplate.Domain.Entities;
using BookSale.Application.Dtos;
using BookSale.Application.Dtos.Request;
using BookSale.Application.Exceptions;
using BookSale.Application.Repositories;
using BookSale.Application.Services.CurrentUser;
using BookSale.Domain.Caching;
using BookSale.Domain.Entities;
using BookSale.Domain.Specifications.Authors;
using BookSale.Domain.Specifications.Categories;
using BookSale.Domain.Specifications.Users;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Services.Admin.Categoris
{
    public class CategoryService : ICategoryService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CategoryService(ICurrentUserService currentUserService, IUnitOfWork unitOfWork,
                               ICategoriesRepository categoriesRepository, IUserRepository userRepository, 
                               IMapper mapper,ICacheService cacheService )
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _categoriesRepository = categoriesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<CategoriesResponseDto> CreateCategoryAsync(CategoriesRequestDto categoriesDto)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if(await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to create information");
            }
             var category = _mapper.Map<Categories>(categoriesDto);
             await _categoriesRepository.AddAsync(category);
             await _unitOfWork.SaveChangesAsync();
             return _mapper.Map<CategoriesResponseDto>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to delete information");
            }
            var targetCategoryIdSpecification = new CategoryIdSpecificationcs(id);
            var category = await _categoriesRepository.FristOrDefaultAsync(targetCategoryIdSpecification) ?? throw new CategoryIdNotFoundException();
             _categoriesRepository.Delete(category);
            _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<CategoriesResponseDto>> getAllCategoryAsync()
        {
            var cacheKey = "Categories";
            var cacheCategory = await _cacheService.GetAsync<List<CategoriesResponseDto>>(cacheKey);
            if (cacheCategory != null)
            {
                return cacheCategory;
            }
            var catagoryAll = await _categoriesRepository.ToListAsync();
            await _cacheService.SetAsync(cacheKey, catagoryAll, TimeSpan.FromHours(10));
            return _mapper.Map<List<CategoriesResponseDto>>(catagoryAll);
        }

        public async Task<CategoriesResponseDto> UpdateCategoryAsync(int id, UpdateCategoriesDto updateCategories)
        {
            var userId = _currentUserService.UserId;
            var currentIdUserSpecification = new UserIdSpecification(userId);
            var roleIdUserSpecification = new UserRoleIdSpecification(Domain.Enums.RoleEnums.Admin);
            if (await _userRepository.ExistsAsync(currentIdUserSpecification.And(roleIdUserSpecification)) == false)
            {
                throw new AuthorizationException("The user is not authorized to update information");
            }
            var targetCategoryIdSpecification = new CategoryIdSpecificationcs(id);
            var category = await _categoriesRepository.FristOrDefaultAsync(targetCategoryIdSpecification) ?? throw new CategoryIdNotFoundException();
            _mapper.Map(updateCategories, category);
            _categoriesRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
            var cacheKey = "Categories";
            await _cacheService.RemoveAsync(cacheKey);
            return _mapper.Map<CategoriesResponseDto>(category);
        }
    }
}

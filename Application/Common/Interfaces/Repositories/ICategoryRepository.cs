using Application.Common.DTOs;
using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync(GetAllCategoriesQuery getAllCategoriesQuery);
        Task<List<CategoryDto>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery);
        Task<List<CategoryDto>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery);
        Task<CategoryDto> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery);
        Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand);
        Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand);
        Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand);
    }
}
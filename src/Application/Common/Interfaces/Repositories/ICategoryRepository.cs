using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync(GetAllCategoriesQuery getAllCategoriesQuery);
        Task<List<Category>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery);
        Task<List<Category>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery);
        Task<Category> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery);
        Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand);
        Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand);
        Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand);
    }
}
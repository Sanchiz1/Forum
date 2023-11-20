using Application.Common.DTOs;
using Application.Common.ViewModels;
using Application.UseCases.Category.Commands;
using Application.UseCases.Category.Queries;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery);
        Task<List<CategoryDto>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery);
        Task<CategoryDto> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery);
        Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand);
        Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand);
        Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand);
    }
}
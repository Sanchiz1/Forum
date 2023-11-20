using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Category.Commands;
using Application.UseCases.Category.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<CategoryDto>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery)
        {
            throw new NotImplementedException();
        }
        public Task<List<CategoryDto>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery)
        {
            throw new NotImplementedException();
        }

        public Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand)
        {
            throw new NotImplementedException();
        }
    }
}

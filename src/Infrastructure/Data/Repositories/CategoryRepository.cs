using Application.Common.Interfaces.Repositories;
using Application.UseCases.Categories.Commands;
using Application.UseCases.Categories.Queries;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _dapperContext;

        public CategoryRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public async Task<List<Category>> GetAllCategoriesAsync(GetAllCategoriesQuery getAllCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT * FROM Categories
                ORDER BY Title";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<Category>(query, getAllCategoriesQuery)).ToList();

            return result;
        }
        public async Task<List<Category>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT * FROM Categories
                WHERE Title LIKE '{getCategoriesQuery.Search}'
                ORDER BY Title
                OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<Category>(query, getCategoriesQuery)).ToList();

            return result;
        }
        public async Task<List<Category>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT Categories.Id, Categories.Title FROM Post_Category
                JOIN Categories ON Categories.Id = Post_Category.Category_Id
                WHERE Post_Category.Post_Id = @Post_Id";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<Category>(query, getPostCategoriesQuery)).ToList();

            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery)
        {
            Category result = null;

            string query = $@"SELECT * FROM Categories WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<Category>(query, getCategoryByIdQuery)).FirstOrDefault();

            return result;
        }

        public async Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand)
        {
            string query = $@"INSERT INTO Categories (Title) VALUES(@Title)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, createCategoryCommand);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand)
        {
            string query = $@"UPDATE Categories SET Title = @Title WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updateCategoryCommand);
        }

        public async Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand)
        {
            string query = $@"DELETE FROM Categories WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, deleteCategoryCommand);
        }
    }
}

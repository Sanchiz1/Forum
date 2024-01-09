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
        private readonly ILogger _logger;

        public CategoryRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<List<Category>> GetAllCategoriesAsync(GetAllCategoriesQuery getAllCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT * FROM Categories
                ORDER BY Title";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Category>(query, getAllCategoriesQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting categories");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting categories");
                throw;
            }

            return result;
        }
        public async Task<List<Category>> GetCategoriesAsync(GetCategoriesQuery getCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT * FROM Categories
                WHERE Title LIKE '{getCategoriesQuery.Search}'
                ORDER BY Title
                OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Category>(query, getCategoriesQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting categories");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting categories");
                throw;
            }

            return result;
        }
        public async Task<List<Category>> GetPostCategoriesAsync(GetPostCategoriesQuery getPostCategoriesQuery)
        {
            List<Category> result = null;

            string query = $@"SELECT Categories.Id, Categories.Title FROM Post_Category
                JOIN Categories ON Categories.Id = Post_Category.Category_Id
                WHERE Post_Category.Post_Id = @Post_Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Category>(query, getPostCategoriesQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting post categories");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting post categories");
                throw;
            }

            return result;
        }

        public async Task<Category> GetCategoryByIdAsync(GetCategoryByIdQuery getCategoryByIdQuery)
        {
            Category result = null;

            string query = $@"SELECT * FROM Categories WHERE Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Category>(query, getCategoryByIdQuery)).FirstOrDefault();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting category by id");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting category by id");
                throw;
            }

            return result;
        }

        public async Task CreateCategoryAsync(CreateCategoryCommand createCategoryCommand)
        {
            string query = $@"INSERT INTO Categories (Title) VALUES(@Title)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, createCategoryCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating category");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating category");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(UpdateCategoryCommand updateCategoryCommand)
        {
            string query = $@"UPDATE Categories SET Title = @Title WHERE Id = @Id";
            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updateCategoryCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating category");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating category");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(DeleteCategoryCommand deleteCategoryCommand)
        {
            string query = $@"DELETE FROM Categories WHERE Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, deleteCategoryCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting category");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting category");
                throw;
            }
        }
    }
}

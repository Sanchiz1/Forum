using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public CommentRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<List<Comment>> GetCommentsAsync(GetCommentsQuery getCommentsQuery)
        {
            List<Comment> result = null;
            string query = $"SELECT Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, " +
                $"users.Username as User_Username, " +
                $"Count(DISTINCT Comment_Likes.User_Id) as Likes, " +
                $"Count(DISTINCT Replies.Id) as Replies, " +
                $"CASE WHEN EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Likes.Comment_Id = Comments.Id AND User_Id = @User_Id) then 1 ELSE 0 END AS Liked " +
                $"FROM Comments " +
                $"INNER JOIN Users ON Users.Id = Comments.User_Id " +
                $"LEFT JOIN Comment_Likes ON Comment_Likes.Comment_Id = Comments.Id " +
                $"LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id " +
                $"WHERE Comments.Date < @user_timestamp AND Comments.Post_Id = @post_id " +
                $"GROUP BY Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, users.Username " +
                $"ORDER BY {getCommentsQuery.Order} DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Comment>(query, getCommentsQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting comments");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting comment by id");
                throw;
            }

            return result;
        }
        public async Task<Comment> GetCommentByIdAsync(GetCommentByIdQuery getCommentByIdQuery)
        {
            Comment result = null;

            string query = "SELECT Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, " +
                $"users.Username as User_Username, " +
                $"Count(DISTINCT Comment_Likes.User_Id) as Likes, " +
                $"Count(DISTINCT Replies.Id) as Replies, " +
                $"CASE WHEN EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Likes.Comment_Id = Comments.Id AND User_Id = @User_Id) then 1 ELSE 0 END AS Liked " +
                $"FROM Comments " +
                $"INNER JOIN Users ON Users.Id = Comments.User_Id " +
                $"LEFT JOIN Comment_Likes ON Comment_Likes.Comment_Id = Comments.Id " +
                $"LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id " +
                $"WHERE Comments.Id = @Id " +
                $"GROUP BY Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, users.Username";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Comment>(query, getCommentByIdQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting comment by id");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting comment by id");
                throw;
            }

            return result;
        }
        public async Task CreateCommentAsync(CreateCommentCommand createCommentCommand)
        {
            string query = $"INSERT INTO Comments (Text, User_Id, Post_Id) VALUES(@Text, @User_Id, @Post_Id)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, createCommentCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating comment");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating comment");
                throw;
            }
        }
        public async Task UpdateCommentAsync(UpdateCommentCommand updateCommentCommand)
        {
            string query = $"UPDATE Comments SET Text = @Text WHERE Id = @Id";
            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updateCommentCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating comment");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating comment");
                throw;
            }
        }
        public async Task DeleteCommentAsync(DeleteCommentCommand deleteCommentCommand)
        {
            string query = $"DELETE FROM Comments WHERE Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, deleteCommentCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting comment");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting comment");
                throw;
            }
        }
        public async Task LikeCommentAsync(LikeCommentCommand likeCommentCommand)
        {
            string query = $"IF EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Id = @Comment_Id AND User_Id = @User_Id)" +
                $" BEGIN DELETE FROM Comment_Likes WHERE Comment_Id = @Comment_Id AND User_Id = @User_Id END" +
                $" ELSE BEGIN INSERT INTO Comment_Likes (Comment_Id, User_Id) VALUES(@Comment_Id, @User_Id) END GO";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, likeCommentCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Liking comment");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Liking comment");
                throw;
            }
        }
    }
}

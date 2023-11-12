using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
using Dapper;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public PostRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<List<Post>> GetPostsAsync(GetPostsQuery getPostsQuery)
        {
            List<Post> result = null;
            string query = $"SELECT Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id," +
                    $" users.Username as User_Username," +
                    $" Count(DISTINCT Post_Likes.User_Id) as Likes," +
                    $" Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments, " +
                    $" CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked" +
                    $" FROM Posts " +
                    $"  INNER JOIN Users ON Users.Id = Posts.User_Id" +
                    $"  LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Comments ON Comments.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id" +
                    $" WHERE Posts.Date < @user_timestamp" +
                    $" GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id, users.Username" +
                    $" ORDER BY Posts.Date DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Post>(query, getPostsQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting posts");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting posts");
                throw;
            }

            return result;
        }
        public async Task<Post> GetPostByIdAsync(GetPostByIdQuery getPostByIdQuery)
        {
            Post result = null;
            string query = $"SELECT Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id," +
                    $" users.Username as User_Username," +
                    $" Count(DISTINCT Post_Likes.User_Id) as Likes," +
                    $" Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments, " +
                    $" CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked" +
                    $"  FROM Posts " +
                    $"  INNER JOIN Users ON Users.Id = Posts.User_Id" +
                    $"  LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Comments ON Comments.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id" +
                    $" WHERE Posts.Id = @id" +
                    $" GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id, users.Username";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Post>(query, getPostByIdQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting post by id");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting post by id");
                throw;
            }

            return result;
        }
        public async Task CreatePostAsync(CreatePostCommand createPostCommand)
        {
            string query = $"INSERT INTO Posts (Title, Text, User_Id) VALUES (@Title, @Text, @User_Id)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, createPostCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating post");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating post");
                throw;
            }
        }
        public async Task UpdatePostAsync(UpdatePostCommand updatePostCommand)
        {
            string query = $"UPDATE Posts set Text = @text WHERE Id = @id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updatePostCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating post");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating post");
                throw;
            }
        }
        public async Task DeletePostAsync(DeletePostCommand deletePostCommand)
        {
            string query = $"Delete FROM Posts WHERE Id = @id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, deletePostCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting post");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting post");
                throw;
            }
        }
        public async Task LikePostAsync(LikePostCommand likePostCommand)
        {
            string query = $"IF EXISTS (SELECT * FROM Post_Likes WHERE Post_Id = @post_id AND User_Id = @user_id)" +
                $" BEGIN DELETE FROM Post_Likes WHERE Post_Id = @post_id AND User_Id = @user_id END" +
                $" ELSE BEGIN INSERT INTO Post_Likes (Post_Id, User_Id) VALUES(@post_id, @user_id) END GO";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, likePostCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Liking post");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Liking post");
                throw;
            }
        }
    }
}

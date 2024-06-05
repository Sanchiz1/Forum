using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Posts.Commands;
using Application.UseCases.Posts.Queries;
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
    public class PostRepository : IPostRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public PostRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }

        public async Task<List<PostViewModel>> GetPostsAsync(GetPostsQuery getPostsQuery)
        {
            List<PostViewModel> result = null;
            string query = $@"SELECT users.Username as User_Username, 
                    Count(DISTINCT Post_Likes.User_Id) as Likes,
                    Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id
                    FROM Posts 
                    INNER JOIN Users ON Users.Id = Posts.User_Id
                    LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id
                    LEFT JOIN Post_Category ON Post_Category.Post_Id = Posts.Id 
                    LEFT JOIN Comments ON Comments.Post_Id = Posts.Id 
                    LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                    WHERE Posts.Date_Created < @User_Timestamp AND ({getPostsQuery.Categories.Length} = 0 OR Post_Category.Category_Id in @Categories) 
                    GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id, users.Username
                    ORDER BY {getPostsQuery.Order} DESC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<PostViewModel, Post, PostViewModel>(query, (postViewModel, post) =>
            {
                postViewModel.Post = post;

                return postViewModel;
            }, getPostsQuery, splitOn: "Id")).ToList();

            return result;
        }

        public async Task<List<PostViewModel>> GetSearchedPostsAsync(GetSearchedPostsQuery getSearchedPostsQuery)
        {
            List<PostViewModel> result = null;
            string query = $@"SELECT users.Username as User_Username,
                    Count(DISTINCT Post_Likes.User_Id) as Likes,
                    Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id
                    FROM Posts
                    INNER JOIN Users ON Users.Id = Posts.User_Id
                    LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id
                    LEFT JOIN Comments ON Comments.Post_Id = Posts.Id
                    LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                    WHERE (Posts.Title LIKE '%{getSearchedPostsQuery.Search}%' OR Posts.Text LIKE '%{getSearchedPostsQuery.Search}%') AND Posts.Date_Created < @User_Timestamp
                    GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id, users.Username
                    ORDER BY {getSearchedPostsQuery.Order} DESC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<PostViewModel, Post, PostViewModel>(query, (postViewModel, post) =>
            {
                postViewModel.Post = post;

                return postViewModel;
            }, getSearchedPostsQuery, splitOn: "Id")).ToList();

            return result;
        }

        public async Task<List<PostViewModel>> GetUserPostsAsync(GetUserPostsQuery getUserPostsQuery)
        {
            List<PostViewModel> result = null;
            string query = $@"SELECT Users.Username as User_Username,
                    Count(DISTINCT Post_Likes.User_Id) as Likes,
                    Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id
                    FROM Posts
                    INNER JOIN Users ON Users.Id = Posts.User_Id
                    LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id
                    LEFT JOIN Comments ON Comments.Post_Id = Posts.Id
                    LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                    WHERE Posts.Date_Created < @User_Timestamp AND users.Username = @Author_Username
                    GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id, users.Username
                    ORDER BY {getUserPostsQuery.Order} DESC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<PostViewModel, Post, PostViewModel>(query, (postViewModel, post) =>
            {
                postViewModel.Post = post;

                return postViewModel;
            }, getUserPostsQuery, splitOn: "Id")).ToList();

            return result;
        }
        public async Task<PostViewModel> GetPostByIdAsync(GetPostByIdQuery getPostByIdQuery)
        {
            PostViewModel result = null;
            string query = $@"SELECT users.Username as User_Username,
                    Count(DISTINCT Post_Likes.User_Id) as Likes,
                    Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id
                    FROM Posts
                    INNER JOIN Users ON Users.Id = Posts.User_Id
                    LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id
                    LEFT JOIN Comments ON Comments.Post_Id = Posts.Id
                    LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                    WHERE Posts.Id = @Id
                    GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date_Created, Posts.Date_Edited, Posts.User_Id, users.Username";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<PostViewModel, Post, PostViewModel>(query, (postViewModel, post) =>
            {
                postViewModel.Post = post;

                return postViewModel;
            }, getPostByIdQuery, splitOn: "Id")).FirstOrDefault();

            return result;
        }
        public async Task CreatePostAsync(CreatePostCommand createPostCommand)
        {
            string query = $@"INSERT INTO Posts (Title, Text, User_Id) VALUES (@Title, @Text, @Account_Id)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, createPostCommand);
        }
        public async Task UpdatePostAsync(UpdatePostCommand updatePostCommand)
        {
            string query = $@"UPDATE Posts set Text = @Text WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updatePostCommand);
        }
        public async Task AddPostCategoryAsync(AddPostCategoryCommand addPostCategoryCommand)
        {
            string query = $@"INSERT INTO Post_Category (Post_Id, Category_Id) VALUES (@Post_Id, @Category_Id)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, addPostCategoryCommand);
        }
        public async Task RemovePostCategoryAsync(RemovePostCategoryCommand removePostCategoryCommand)
        {
            string query = $@"DELETE FROM Post_Category WHERE Post_Id = @Post_Id AND Category_Id = @Category_Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, removePostCategoryCommand);
        }
        public async Task DeletePostAsync(DeletePostCommand deletePostCommand)
        {
            string query = $@"Delete FROM Posts WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, deletePostCommand);
        }
        public async Task LikePostAsync(LikePostCommand likePostCommand)
        {
            string query = $@"IF EXISTS (SELECT * FROM Post_Likes WHERE Post_Id = @Post_Id AND User_Id = @User_Id)
                BEGIN DELETE FROM Post_Likes WHERE Post_Id = @Post_Id AND User_Id = @User_Id END
                ELSE BEGIN INSERT INTO Post_Likes (Post_Id, User_Id) VALUES(@Post_Id, @User_Id) END";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, likePostCommand);
        }
    }
}

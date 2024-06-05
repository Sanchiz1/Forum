using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly DapperContext _dapperContext;

        public CommentRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public async Task<List<CommentViewModel>> GetCommentsAsync(GetCommentsQuery getCommentsQuery)
        {
            List<CommentViewModel> result = null;
            string query = $@"SELECT users.Username as User_Username,
                Count(DISTINCT Comment_Likes.User_Id) as Likes,
                Count(DISTINCT Replies.Id) as Replies,
                CAST(CASE WHEN EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Likes.Comment_Id = Comments.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                Comments.Id, Comments.Post_Id, Comments.Text, Comments.Date_Created, Comments.Date_Edited, Comments.User_Id
                FROM Comments
                INNER JOIN Users ON Users.Id = Comments.User_Id
                LEFT JOIN Comment_Likes ON Comment_Likes.Comment_Id = Comments.Id
                LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                WHERE Comments.Date_Created < @user_timestamp AND Comments.Post_Id = @post_id
                GROUP BY Comments.Id, Comments.Post_Id, Comments.Text, Comments.Date_Created, Comments.Date_Edited, Comments.User_Id, users.Username
                ORDER BY {getCommentsQuery.Order} DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<CommentViewModel, Comment, CommentViewModel>(query, (commentViewModel, comment) =>
            {
                commentViewModel.Comment = comment;

                return commentViewModel;
            }, getCommentsQuery, splitOn: "Id")).ToList();

            return result;
        }
        public async Task<CommentViewModel> GetCommentByIdAsync(GetCommentByIdQuery getCommentByIdQuery)
        {
            CommentViewModel result = null;

            string query = $@"SELECT users.Username as User_Username,
                Count(DISTINCT Comment_Likes.User_Id) as Likes,
                Count(DISTINCT Replies.Id) as Replies,
                CAST(CASE WHEN EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Likes.Comment_Id = Comments.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                Comments.Id, Comments.Post_Id, Comments.Text, Comments.Date_Created, Comments.Date_Edited, Comments.User_Id
                FROM Comments
                INNER JOIN Users ON Users.Id = Comments.User_Id
                LEFT JOIN Comment_Likes ON Comment_Likes.Comment_Id = Comments.Id
                LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id
                WHERE Comments.Id = @Id
                GROUP BY Comments.Id, Comments.Post_Id, Comments.Text, Comments.Date_Created, Comments.Date_Edited, Comments.User_Id, users.Username";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<CommentViewModel, Comment, CommentViewModel>(query, (commentViewModel, comment) =>
            {
                commentViewModel.Comment = comment;

                return commentViewModel;
            }, getCommentByIdQuery, splitOn: "Id")).FirstOrDefault();

            return result;
        }
        public async Task CreateCommentAsync(CreateCommentCommand createCommentCommand)
        {
            string query = $@"INSERT INTO Comments (Text, User_Id, Post_Id) VALUES(@Text, @User_Id, @Post_Id)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, createCommentCommand);
        }
        public async Task UpdateCommentAsync(UpdateCommentCommand updateCommentCommand)
        {
            string query = $@"UPDATE Comments SET Text = @Text WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updateCommentCommand);
        }
        public async Task DeleteCommentAsync(DeleteCommentCommand deleteCommentCommand)
        {
            string query = $@"DELETE FROM Comments WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, deleteCommentCommand);
        }
        public async Task LikeCommentAsync(LikeCommentCommand likeCommentCommand)
        {
            string query = $@"IF EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Id = @Comment_Id AND User_Id = @User_Id)
                BEGIN DELETE FROM Comment_Likes WHERE Comment_Id = @Comment_Id AND User_Id = @User_Id END
                ELSE BEGIN INSERT INTO Comment_Likes (Comment_Id, User_Id) VALUES(@Comment_Id, @User_Id) END";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, likeCommentCommand);
        }
    }
}

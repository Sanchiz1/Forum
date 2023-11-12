using Application.Common.Interfaces.Repositories;
using Application.UseCases.Comments.Commands;
using Application.UseCases.Comments.Queries;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
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
    public class ReplyRepository : IReplyRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly ILogger _logger;

        public ReplyRepository(DapperContext context, ILogger logger)
        {
            _dapperContext = context;
            _logger = logger;
        }
        public async Task<List<Reply>> GetRepliesAsync(GetRepliesQuery getRepliesQuery)
        {
            List<Reply> result = null;
            string query = $"SELECT Replies.Id, Replies.Text, Replies.Date, Replies.User_Id, Replies.Comment_Id, Replies.Reply_Id, " +
                    $" users.Username as User_Username, ReplyToUsers.Username as Reply_Username, " +
                    $" Count(DISTINCT Reply_Likes.User_Id) as Likes, " +
                    $" CASE WHEN EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Likes.Reply_Id = Replies.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked " +
                    $"FROM Replies " +
                    $" INNER JOIN Users ON Users.Id = Replies.User_Id " +
                    $" LEFT JOIN Replies ReplyToReplies ON ReplyToReplies.Id = Replies.Reply_Id " +
                    $" LEFT JOIN Users ReplyToUsers ON ReplyToUsers.Id = ReplyToReplies.User_Id " +
                    $" LEFT JOIN Reply_Likes ON Reply_Likes.Reply_Id = Replies.Id " +
                    $"WHERE Replies.Date < @user_timestamp AND Replies.Comment_Id = @comment_id " +
                    $"GROUP BY Replies.Id, Replies.Text, Replies.Date, Replies.Comment_Id, Replies.User_Id, users.Username, " +
                    $" ReplyToUsers.Username, ReplyToReplies.User_Id, Replies.Reply_Id " +
                    $"ORDER BY Date DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Reply>(query, getRepliesQuery)).ToList();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting replies");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting replies");
                throw;
            }

            return result;
        }
        public async Task<Reply> GetReplyByIdAsync(GetReplyByIdQuery getReplyByIdQuery)
        {
            Reply result = null;
            string query = $"SELECT Replies.Id as Id, Text, Date, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Replies.Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<Reply>(query, getReplyByIdQuery)).First();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Getting replies");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Getting replies");
                throw;
            }

            return result;
        }
        public async Task CreateReplyAsync(CreateReplyCommand createReplyCommand)
        {
            string query = $"INSERT INTO Replies (Text, User_Id, Comment_Id, Reply_Id) VALUES(@Text, @User_Id, @Comment_Id, @Reply_Id)";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, createReplyCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Creating reply");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating reply");
                throw;
            }
        }
        public async Task UpdateReplyAsync(UpdateReplyCommand updateReplyCommand)
        {
            string query = $"UPDATE Replies SET Text = @Text WHERE Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, updateReplyCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Updating reply");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Updating reply");
                throw;
            }
        }
        public async Task DeleteReplyAsync(DeleteReplyCommand deleteReplyCommand)
        {
            string query = $"DELETE FROM Replies WHERE Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, deleteReplyCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Deleting reply");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deleting reply");
                throw;
            }
        }
        public async Task LikeReplyAsync(LikeReplyCommand likeReplyCommand)
        {
            string query = $"IF EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Id = @Reply_Id AND User_Id = @User_Id)" +
                $" BEGIN DELETE FROM Reply_Likes WHERE Reply_Id = @Reply_Id AND User_Id = @User_Id END" +
                $" ELSE BEGIN INSERT INTO Reply_Likes (Reply_Id, User_Id) VALUES(@Reply_Id, @User_Id) END GO";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                await connection.ExecuteAsync(query, likeReplyCommand);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Liking reply");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Liking reply");
                throw;
            }
        }
    }
}

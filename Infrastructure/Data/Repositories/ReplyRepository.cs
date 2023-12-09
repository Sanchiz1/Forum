using Application.Common.DTOs;
using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        public async Task<List<ReplyViewModel>> GetRepliesAsync(GetRepliesQuery getRepliesQuery)
        {
            List<ReplyViewModel> result = null;
            string query = $"SELECT Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.User_Id, Replies.Comment_Id, Replies.Reply_User_Id, " +
                    $" Users.Username as User_Username, ReplyToUsers.Username as Reply_Username, " +
                    $" Count(DISTINCT Reply_Likes.User_Id) as Likes, " +
                    $" CAST(CASE WHEN EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Likes.Reply_Id = Replies.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked " +
                    $"FROM Replies " +
                    $" INNER JOIN Users ON Users.Id = Replies.User_Id " +
                    $" LEFT JOIN Users ReplyToUsers ON ReplyToUsers.Id = Replies.Reply_User_Id " +
                    $" LEFT JOIN Reply_Likes ON Reply_Likes.Reply_Id = Replies.Id " +
                    $"WHERE Replies.Date_Created < @user_timestamp AND Replies.Comment_Id = @comment_id " +
                    $"GROUP BY Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.Comment_Id, Replies.User_Id, users.Username, " +
                    $" ReplyToUsers.Username, Replies.Reply_User_Id, Replies.Reply_User_Id " +
                    $"ORDER BY Date_Created ASC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<dynamic>(query, getRepliesQuery)).Select(item =>
                    new ReplyViewModel()
                    {
                        Reply = new ReplyDto()
                        {
                            Id = item.Id,
                            Text = item.Text,
                            Comment_Id = item.Comment_Id,
                            User_Id = item.User_Id,
                            Date_Created = item.Date_Created,
                            Date_Edited = item.Date_Edited,
                            Reply_User_Id = item.Reply_User_Id,
                        },
                        Reply_Username = item.Reply_Username,
                        User_Username = item.User_Username,
                        Likes = item.Likes,
                        Liked = item.Liked
                    }
                ).ToList();
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
        public async Task<ReplyViewModel> GetReplyByIdAsync(GetReplyByIdQuery getReplyByIdQuery)
        {
            ReplyViewModel result = null;
            string query = $"SELECT Replies.Id as Id, Text, Date_Created, Date_Edited, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Replies.Id = @Id";

            try
            {
                using var connection = _dapperContext.CreateConnection();
                result = (await connection.QueryAsync<dynamic>(query, getReplyByIdQuery)).Select(item =>
                     new ReplyViewModel()
                     {
                         Reply = new ReplyDto()
                         {
                             Id = item.Id,
                             Text = item.Text,
                             Comment_Id = item.Comment_Id,
                             User_Id = item.User_Id,
                             Date_Created = item.Date_Created,
                             Date_Edited = item.Date_Edited,
                             Reply_User_Id = item.Reply_User_Id,
                         },
                         Reply_Username = item.Reply_Username,
                         User_Username = item.User_Username,
                         Likes = item.Likes,
                         Liked = item.Liked
                     }
                ).First();
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
            string query = $"INSERT INTO Replies (Text, User_Id, Comment_Id, Reply_User_Id) VALUES(@Text, @User_Id, @Comment_Id, @Reply_User_Id)";

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
                $" ELSE BEGIN INSERT INTO Reply_Likes (Reply_Id, User_Id) VALUES(@Reply_Id, @User_Id) END";

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

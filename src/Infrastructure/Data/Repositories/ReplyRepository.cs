using Application.Common.Interfaces.Repositories;
using Application.Common.ViewModels;
using Application.UseCases.Replies.Commands;
using Application.UseCases.Replies.Queries;
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

            string query = $@"SELECT Users.Username as User_Username, ReplyToUsers.Username as Reply_Username,
                    Count(DISTINCT Reply_Likes.User_Id) as Likes,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Likes.Reply_Id = Replies.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.User_Id, Replies.Comment_Id, Replies.Reply_User_Id
                    FROM Replies
                    INNER JOIN Users ON Users.Id = Replies.User_Id
                    LEFT JOIN Users ReplyToUsers ON ReplyToUsers.Id = Replies.Reply_User_Id
                    LEFT JOIN Reply_Likes ON Reply_Likes.Reply_Id = Replies.Id
                    WHERE Replies.Date_Created < @user_timestamp AND Replies.Comment_Id = @comment_id
                    GROUP BY Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.Comment_Id, Replies.User_Id, users.Username,
                    ReplyToUsers.Username, Replies.Reply_User_Id, Replies.Reply_User_Id
                    ORDER BY Date_Created ASC OFFSET @Offset ROWS FETCH NEXT @Next ROWS ONLY";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<ReplyViewModel, Reply, ReplyViewModel>(query, (replyViewModel, reply) =>
            {
                replyViewModel.Reply = reply;

                return replyViewModel;
            }, getRepliesQuery, splitOn: "Id")).ToList();

            return result;
        }
        public async Task<ReplyViewModel> GetReplyByIdAsync(GetReplyByIdQuery getReplyByIdQuery)
        {
            ReplyViewModel result = null;
            string query = $@"SELECT
                    Users.Username as User_Username, ReplyToUsers.Username as Reply_Username,
                    Count(DISTINCT Reply_Likes.User_Id) as Likes,
                    CAST(CASE WHEN EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Likes.Reply_Id = Replies.Id AND User_Id = @User_Id) THEN 1 ELSE 0 END AS BIT) AS Liked,
                    Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.User_Id, Replies.Comment_Id, Replies.Reply_User_Id
                    FROM Replies
                    INNER JOIN Users ON Users.Id = Replies.User_Id
                    LEFT JOIN Users ReplyToUsers ON ReplyToUsers.Id = Replies.Reply_User_Id
                    LEFT JOIN Reply_Likes ON Reply_Likes.Reply_Id = Replies.Id
                    WHERE Replies.Id = @Id
                    GROUP BY Replies.Id, Replies.Text, Replies.Date_Created, Replies.Date_Edited, Replies.Comment_Id, Replies.User_Id, users.Username,
                    ReplyToUsers.Username, Replies.Reply_User_Id, Replies.Reply_User_Id ";

            using var connection = _dapperContext.CreateConnection();
            result = (await connection.QueryAsync<ReplyViewModel, Reply, ReplyViewModel>(query, (replyViewModel, reply) =>
            {
                replyViewModel.Reply = reply;

                return replyViewModel;
            }, getReplyByIdQuery, splitOn: "Id")).FirstOrDefault();

            return result;
        }
        public async Task CreateReplyAsync(CreateReplyCommand createReplyCommand)
        {
            string query = $@"INSERT INTO Replies (Text, User_Id, Comment_Id, Reply_User_Id) VALUES(@Text, @User_Id, @Comment_Id, @Reply_User_Id)";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, createReplyCommand);
        }
        public async Task UpdateReplyAsync(UpdateReplyCommand updateReplyCommand)
        {
            string query = $@"UPDATE Replies SET Text = @Text WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, updateReplyCommand);
        }
        public async Task DeleteReplyAsync(DeleteReplyCommand deleteReplyCommand)
        {
            string query = $@"DELETE FROM Replies WHERE Id = @Id";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, deleteReplyCommand);
        }
        public async Task LikeReplyAsync(LikeReplyCommand likeReplyCommand)
        {
            string query = $@"IF EXISTS (SELECT * FROM Reply_Likes WHERE Reply_Id = @Reply_Id AND User_Id = @User_Id)
                BEGIN DELETE FROM Reply_Likes WHERE Reply_Id = @Reply_Id AND User_Id = @User_Id END
                ELSE BEGIN INSERT INTO Reply_Likes (Reply_Id, User_Id) VALUES(@Reply_Id, @User_Id) END";

            using var connection = _dapperContext.CreateConnection();
            await connection.ExecuteAsync(query, likeReplyCommand);
        }
    }
}

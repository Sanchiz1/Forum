using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models.Replies;
using Forum.Models.User;

namespace Forum.Data.Repositories.Implementations
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly DapperContext _dapperContext;

        public ReplyRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public List<Reply> GetReplies(int comment_id, int next, int offset, DateTime user_timestamp, string order = "Date")
        {
            string query = $"SELECT Reply.Id, Text, Date, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Comments INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Comment_Id = @comment_id AND Date < @user_timestamp ORDER BY Date DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            using var connection = _dapperContext.CreateConnection();

            var replies = connection.Query<Reply>(query, new { comment_id, next, offset, user_timestamp }).ToList();
            return replies;
        }
        public void CreateReply(ReplyInput comment)
        {
            string query = $"INSERT INTO Replies (Text, User_Id, Post_Id) VALUES(@Text, @User_Id, @Post_Id)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { comment });
        }
        public void UpdateReply(string Text, int Id)
        {
            string query = $"UPDATE Replies SET Text = @Text WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Text, Id });
        }
        public void DeleteReply(int Id)
        {
            string query = $"DELETE FROM Replies WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Id });
        }
    }
}

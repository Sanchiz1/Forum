using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models.Comments;
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
            try
            {
            string query = $"SELECT Replies.Id, Text, Date, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Comment_Id = @comment_id AND Date < @user_timestamp ORDER BY Date DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            using var connection = _dapperContext.CreateConnection();

            var replies = connection.Query<Reply>(query, new { comment_id, next, offset, user_timestamp }).ToList();
            return replies;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public Reply GetReplyById(int Id)
        {
            try {
            string query = $"SELECT Reply.Id, Text, Date, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Comments INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            var reply = connection.Query<Reply>(query, new { Id }).First();
            return reply;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public void CreateReply(ReplyInput reply)
        {
            try {
            string query = $"INSERT INTO Replies (Text, User_Id, Comment_Id) VALUES(@Text, @User_Id, @Comment_Id)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, reply);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateReply(string Text, int Id)
        {
            try {
            string query = $"UPDATE Replies SET Text = @Text WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Text, Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteReply(int Id)
        {
            try {
            string query = $"DELETE FROM Replies WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

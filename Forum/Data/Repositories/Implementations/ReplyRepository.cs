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
        public List<Reply> GetReplies(int comment_id, int next, int offset, DateTime user_timestamp, string order = "Date", int user_id = 0)
        {
            try
            {
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
            using var connection = _dapperContext.CreateConnection();

            var replies = connection.Query<Reply>(query, new { comment_id, next, offset, user_timestamp, user_id }).ToList();
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
            string query = $"SELECT Replies.Id as Id, Text, Date, User_Id, Comment_Id, users.Username as User_Username" +
               $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Replies.Id = @Id";
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
            string query = $"INSERT INTO Replies (Text, User_Id, Comment_Id, Reply_Id) VALUES(@Text, @User_Id, @Comment_Id, @Reply_Id)";
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

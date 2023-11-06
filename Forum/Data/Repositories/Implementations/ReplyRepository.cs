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
        public List<Reply> GetPostReplies(int post_id)
        {
            string query = $"SELECT Replies.Id, Text, Date, User_Id, Post_Id, Reply_Id, users.Username as User_Username" +
                $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Post_Id = @post_id AND Reply_Id IS NULL";
            using var connection = _dapperContext.CreateConnection();

            var replies = connection.Query<Reply>(query, new { post_id }).ToList();
            return replies;
        }
        public List<Reply> GetReplyReplies(int reply_id)
        {
            string query = $"SELECT Replies.Id, Text, Date, User_Id, Post_Id, Reply_Id, users.Username as User_Username" +
                $" FROM Replies INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Reply_Id = reply_id";
            using var connection = _dapperContext.CreateConnection();

            var replies = connection.Query<Reply>(query, new { reply_id }).ToList();
            return replies;
        }
    }
}

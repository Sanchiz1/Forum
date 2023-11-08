using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Models.Comments;
using Forum.Models.Replies;
using static System.Net.Mime.MediaTypeNames;

namespace Forum.Data.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DapperContext _dapperContext;

        public CommentRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public List<Comment> GetComments(int post_id, int next, int offset, DateTime user_timestamp, string order = "Date")
        {
            string query = $"SELECT Comment.Id, Text, Date, User_Id, Post_Id, users.Username as User_Username" +
               $" FROM Comments INNER JOIN Users ON Users.Id = Replies.User_Id WHERE Post_Id = @post_id AND Date < @user_timestamp ORDER BY {order} DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            using var connection = _dapperContext.CreateConnection();

            var comments = connection.Query<Comment>(query, new { post_id, next, offset, user_timestamp }).ToList();
            return comments;
        }
        public void CreateComment(CommentInput comment)
        {
            string query = $"INSERT INTO Comments (Text, User_Id, Post_Id) VALUES(@Text, @User_Id, @Post_Id)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { comment });
        }
        public void UpdateComment(string Text, int Id)
        {
            string query = $"UPDATE Comments SET Text = @Text WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Text, Id });
        }
        public void DeleteComment(int Id)
        {
            string query = $"DELETE FROM Comments WHERE Id = @Id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { Id });
        }
    }
}

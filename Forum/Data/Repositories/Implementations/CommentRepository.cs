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
        public List<Comment> GetComments(int post_id, int next, int offset, DateTime user_timestamp, string order = "Date", int user_id = 0)
        {
            try
            {
                string query = $"SELECT Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, " +
                    $"users.Username as User_Username, " +
                    $"Count(DISTINCT Comment_Likes.User_Id) as Likes, " +
                    $"Count(DISTINCT Replies.Id) as Replies, " +
                    $"CASE WHEN EXISTS (SELECT * FROM Comment_Likes WHERE Comment_Likes.Comment_Id = Comments.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked " +
                    $"FROM Comments " +
                    $"INNER JOIN Users ON Users.Id = Comments.User_Id " +
                    $"LEFT JOIN Comment_Likes ON Comment_Likes.Comment_Id = Comments.Id " +
                    $"LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id " +
                    $"WHERE Comments.Date < @user_timestamp AND Comments.Post_Id = @post_id " +
                    $"GROUP BY Comments.Id, Comments.Text, Comments.Date, Comments.User_Id, users.Username " +
                    $"ORDER BY Date DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
                using var connection = _dapperContext.CreateConnection();

                var comments = connection.Query<Comment>(query, new { post_id, next, offset, user_timestamp, user_id }).ToList();
                return comments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public Comment GetCommentById(int Id)
        {
            try
            {
                string query = $"SELECT Comments.Id, Text, Date, User_Id, Post_Id, users.Username as User_Username" +
                   $" FROM Comments INNER JOIN Users ON Users.Id = Comments.User_Id WHERE Comments.Id = @Id";
                using var connection = _dapperContext.CreateConnection();

                var comment = connection.Query<Comment>(query, new { Id }).First();
                return comment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public void CreateComment(CommentInput comment)
        {
            string query = $"INSERT INTO Comments (Text, User_Id, Post_Id) VALUES(@Text, @User_Id, @Post_Id)";

            try
            {
                using var connection = _dapperContext.CreateConnection();

                connection.Execute(query, comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateComment(string Text, int Id)
        {
            try
            {
                string query = $"UPDATE Comments SET Text = @Text WHERE Id = @Id";
                using var connection = _dapperContext.CreateConnection();

                connection.Execute(query, new { Text, Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteComment(int Id)
        {
            try
            {
                string query = $"DELETE FROM Comments WHERE Id = @Id";
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

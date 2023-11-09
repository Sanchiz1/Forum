using Dapper;
using Forum.Data.Repositories.Interfaces;
using Forum.Helpers;
using Forum.Models.Posts;
using Forum.Models.User;
using System;

namespace Forum.Data.Repositories.Implementations
{
    public class PostRepository : IPostRepository
    {
        private readonly DapperContext _dapperContext;

        public PostRepository(DapperContext context)
        {
            _dapperContext = context;
        }
        public List<Post> GetPosts(int user_id = 0)
        {
            try
            {
                string query = $"SELECT Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id," +
                    $" users.Username as User_Username," +
                    $" Count(DISTINCT Post_Likes.User_Id) as Likes," +
                    $" Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments, " +
                    $" CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked" +
                    $"  FROM Posts " +
                    $"  INNER JOIN Users ON Users.Id = Posts.User_Id" +
                    $"  LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Comments ON Comments.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id" +
                    $" GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id, users.Username";
                using var connection = _dapperContext.CreateConnection();

                var posts = connection.Query<Post>(query, new { user_id }).ToList();
                return posts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public List<Post> GetPagedSortedPosts(int next, int offset, DateTime user_timestamp, string order = "Date", int user_id = 0)
        {
            string query = $"SELECT Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id," +
                $" users.Username as User_Username," +
                $" Count(DISTINCT Post_Likes.User_Id) as Likes," +
                $" Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments, " +
                $" CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked" +
                $"FROM Posts " +
                $"  INNER JOIN Users ON Users.Id = Posts.User_Id" +
                $"  LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id " +
                $"  LEFT JOIN Comments ON Comments.Post_Id = Posts.Id " +
                $"  LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id" +
                $" WHERE Date < @user_timestamp" +
                $" GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id, users.Username," +
                $" ORDER BY {order} DESC OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY";
            using var connection = _dapperContext.CreateConnection();

            var posts = connection.Query<Post>(query, new { next, offset, user_timestamp, user_id }).ToList();
            return posts;
        }
        public Post GetPostById(int id, int user_id = 0)
        {
            try
            {
                string query = $"SELECT Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id," +
                    $" users.Username as User_Username," +
                    $" Count(DISTINCT Post_Likes.User_Id) as Likes," +
                    $" Count(DISTINCT Comments.Id) + Count(DISTINCT Replies.Id) as Comments, " +
                    $" CASE WHEN EXISTS (SELECT * FROM Post_Likes WHERE Post_Likes.Post_Id = Posts.Id AND User_Id = @user_id) then 1 ELSE 0 END AS Liked" +
                    $"  FROM Posts " +
                    $"  INNER JOIN Users ON Users.Id = Posts.User_Id" +
                    $"  LEFT JOIN Post_Likes ON Post_Likes.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Comments ON Comments.Post_Id = Posts.Id " +
                    $"  LEFT JOIN Replies ON Replies.Comment_Id = Comments.Id" +
                    $" WHERE Posts.Id = @id" +
                    $" GROUP BY Posts.Id, Posts.Title, Posts.Text, Posts.Date, Posts.User_Id, users.Username";
                using var connection = _dapperContext.CreateConnection();

            var post = connection.Query<Post>(query, new { id, user_id }).First();
            return post;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public void CreatePost(PostInput post)
        {
            string query = $"INSERT INTO Posts (Title, Text, User_Id) VALUES (@Title, @Text, @User_Id)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, post);
        }
        public void UpdatePost(string text, int id)
        {
            string query = $"UPDATE Posts set Text = @text WHERE Id = @id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { text, id });
        }
        public void DeletePost(int id)
        {
            string query = $"Delete FROM Posts WHERE Id = @id";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { id });
        }
        public void LikePost(int user_id, int post_id)
        {
            string query = $"IF EXISTS (SELECT * FROM Post_Likes WHERE Post_Id = @post_id AND User_Id = @user_id)" +
                $" BEGIN DELETE FROM Post_Likes WHERE Post_Id = @post_id AND User_Id = @user_id END" +
                $" ELSE BEGIN INSERT INTO Post_Likes (Post_Id, User_Id) VALUES(@post_id, @user_id) END GO";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, new { post_id, user_id});
        }
    }
}

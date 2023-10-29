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

        public List<Post> GetPosts()
        {
            string query = $"SELECT * FROM Posts";
            using var connection = _dapperContext.CreateConnection();

            var posts = connection.Query<Post>(query).ToList();
            return posts;
        }
    }
}

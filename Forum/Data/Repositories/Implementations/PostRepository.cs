﻿using Dapper;
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
            string query = $"SELECT Posts.Id, Title, Text, Date, User_Id, users.Username as User_Username FROM Posts INNER JOIN Users ON Users.Id = Posts.User_Id";
            using var connection = _dapperContext.CreateConnection();

            var posts = connection.Query<Post>(query).ToList();
            return posts;
        }
        public Post GetPostById(int id)
        {
            string query = $"SELECT Posts.Id, Title, Text, Date, User_Id, users.Username as User_Username FROM Posts INNER JOIN Users ON Users.Id = Posts.User_Id WHERE Posts.Id = @id";
            using var connection = _dapperContext.CreateConnection();

            var post = connection.Query<Post>(query, new { id }).First();
            return post;
        }
        public void CreatePost(PostInput post)
        {
            string query = $"INSERT INTO Posts (Title, Text, User_Id) VALUES (@Title, @Text, @User_Id)";
            using var connection = _dapperContext.CreateConnection();

            connection.Execute(query, post);
        }
    }
}

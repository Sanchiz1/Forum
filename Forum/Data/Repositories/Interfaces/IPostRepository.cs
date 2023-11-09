using Forum.Models.Posts;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public List<Post> GetPosts(int user_id = 0);
        public List<Post> GetPagedSortedPosts(int next, int offset, DateTime user_timestamp, string order = "Date", int user_id = 0);
        public Post GetPostById(int id, int user_id = 0);
        public void CreatePost(PostInput post);
        public void UpdatePost(string text, int id);
        public void DeletePost(int id);
        public void LikePost(int user_id, int post_id);
    }
}

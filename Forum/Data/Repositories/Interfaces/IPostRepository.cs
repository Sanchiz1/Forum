using Forum.Models.Posts;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public List<Post> GetPosts();
        public List<Post> GetPagedSortedPosts(int next, int offset, DateTime user_timestamp, string order = "Date");
        public Post GetPostById(int id);
        public void CreatePost(PostInput post);
        public void UpdatePost(string text, int id);
        public void DeletePost(int id);
    }
}

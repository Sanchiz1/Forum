using Forum.Models.Posts;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public List<Post> GetPosts();
        public Post GetPostById(int id);
        public void CreatePost(PostInput post);
        public void UpdatePost(string text, int id);
    }
}

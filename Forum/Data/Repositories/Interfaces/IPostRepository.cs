using Forum.Models.Posts;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public List<Post> GetPosts();
    }
}

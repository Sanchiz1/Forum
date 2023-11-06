using Forum.Models.Replies;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IReplyRepository
    {
        public List<Reply> GetPostReplies(int post_id);
        public List<Reply> GetReplyReplies(int reply_id);
    }
}

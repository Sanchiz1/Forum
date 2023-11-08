using Forum.Models.Replies;

namespace Forum.Data.Repositories.Interfaces
{
    public interface IReplyRepository
    {
        public List<Reply> GetReplies(int comment_id, int next, int offset, DateTime user_timestamp, string order = "Date");
        public Reply GetReplyById(int id);
        public void CreateReply(ReplyInput comment);
        public void UpdateReply(string Text, int Id);
        public void DeleteReply(int Id);
    }
}

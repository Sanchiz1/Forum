using Forum.Models.Comments;
using Forum.Models.Replies;

namespace Forum.Data.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        public List<Comment> GetComments(int post_id, int next, int offset, DateTime user_timestamp, string order = "Date");
        public void CreateComment(CommentInput comment);
        public void UpdateComment(string Text, int Id);
        public void DeleteComment(int Id);
    }
}

using capstone_backend.Models;

namespace capstone_backend.Repository.interfaces
{
    public interface ICommentRepo
    {
        IEnumerable<Comments> GetAllComments();
        Comments GetCommentById(int id);
        void AddComment(Comments comment);
        void UpdateComment(Comments comment);
        void DeleteComment(int id);
    }
}

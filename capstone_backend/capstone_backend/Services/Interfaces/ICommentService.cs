using capstone_backend.Models;

namespace capstone_backend.Services.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<Comments> GetAllComments();
        Comments GetCommentById(int id);
        void AddComment(Comments comment);
        void UpdateComment(Comments comment);
        void DeleteComment(int id);
    }
}

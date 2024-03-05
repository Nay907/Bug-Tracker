using capstone_backend.Models;
using capstone_backend.Repository.interfaces;
using capstone_backend.Services.Interfaces;

namespace capstone_backend.Services
{
    public class CommentService: ICommentService
    {
        private readonly ICommentRepo _commentRepo;

        public CommentService(ICommentRepo commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public IEnumerable<Comments> GetAllComments()
        {
            return _commentRepo.GetAllComments();
        }

        public Comments GetCommentById(int id)
        {
            return _commentRepo.GetCommentById(id);
        }

        public void AddComment(Comments comment)
        {
            _commentRepo.AddComment(comment);
        }

        public void UpdateComment(Comments comment)
        {
            _commentRepo.UpdateComment(comment);
        }

        public void DeleteComment(int id)
        {
            _commentRepo.DeleteComment(id);
        }

    }
}

using capstone_backend.Models;
using capstone_backend.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capstone_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Comments>> GetAllComments()
        {
            return Ok(_commentService.GetAllComments());
        }

        [HttpGet("{id}")]
        public ActionResult<Comments> GetCommentById(int id)
        {
            var comment = _commentService.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public ActionResult<Comments> AddComment(Comments comment)
        {
            _commentService.AddComment(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.CommentId }, comment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, Comments comment)
        {
            if (id != comment.CommentId)
            {
                return BadRequest();
            }
            _commentService.UpdateComment(comment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return NoContent();
        }
    }
}

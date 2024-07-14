using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using socialMedia.Models;
using socialMedia.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _repository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IRepository<Comment> repository, ILogger<CommentsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments([FromQuery] int postId)
        {
            var comments = await _repository.FindAsync(c => c.PostId == postId);
            var orderedComments = comments.OrderByDescending(c => c.CreatedAt).ToList();
            return Ok(orderedComments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _repository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> AddComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(comment);
            await _repository.SaveAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Update(comment);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var existingComment = await _repository.GetById(id);
            if (existingComment == null)
            {
                _logger.LogWarning("Comment with id {CommentId} not found", id);
                return NotFound();
            }

            await _repository.Delete(id);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}

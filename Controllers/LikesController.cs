using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LikesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes()
        {
            return await _context.Likes.Include(l => l.User).Include(l => l.Post).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Like>> AddLike(Like like)
        {
            _context.Likes.Add(like);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetLikes), new { userId = like.UserId, postId = like.PostId }, like);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLike([FromQuery] int userId, [FromQuery] int postId)
        {
            var like = await _context.Likes.FindAsync(userId, postId);
            if (like == null)
            {
                return NotFound();
            }

            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

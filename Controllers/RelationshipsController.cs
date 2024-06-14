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
    public class RelationshipsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelationshipsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        {
            return await _context.Relationships.Include(r => r.Follower).Include(r => r.Followed).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Relationship>> AddRelationship(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRelationships), new { followerUserId = relationship.FollowerUserId, followedUserId = relationship.FollowedUserId }, relationship);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRelationship([FromQuery] int followerUserId, [FromQuery] int followedUserId)
        {
            var relationship = await _context.Relationships.FindAsync(followerUserId, followedUserId);
            if (relationship == null)
            {
                return NotFound();
            }

            _context.Relationships.Remove(relationship);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

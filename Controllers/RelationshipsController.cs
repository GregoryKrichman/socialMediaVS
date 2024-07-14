using Microsoft.AspNetCore.Mvc;
using socialMedia.Models;
using socialMedia.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipsController : ControllerBase
    {
        private readonly IRepository<Relationship> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<RelationshipsController> _logger;

        public RelationshipsController(IRepository<Relationship> repository, IRepository<User> userRepository, ILogger<RelationshipsController> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relationship>>> GetRelationships()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> GetRelationship(int id)
        {
            var relationship = await _repository.GetById(id);
            if (relationship == null)
            {
                return NotFound();
            }
            return Ok(relationship);
        }

        [HttpPost]
        public async Task<ActionResult<Relationship>> AddRelationship([FromBody] Relationship relationship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var followerUser = await _userRepository.GetById(relationship.FollowerUserId);
            if (followerUser == null)
            {
                return BadRequest("Invalid Follower User ID");
            }

            
            var followedUser = await _userRepository.GetById(relationship.FollowedUserId);
            if (followedUser == null)
            {
                return BadRequest("Invalid Followed User ID");
            }

            _logger.LogInformation("Follower User: {User}", followerUser);
            _logger.LogInformation("Followed User: {User}", followedUser);

            await _repository.Add(relationship);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetRelationship), new { id = relationship.Id }, relationship);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRelationship([FromBody] Relationship relationship)
        {
            var existingRelationship = await _repository.FirstOrDefaultAsync(r =>
                r.FollowerUserId == relationship.FollowerUserId &&
                r.FollowedUserId == relationship.FollowedUserId);

            if (existingRelationship == null)
            {
                return NotFound();
            }

            await _repository.Delete(existingRelationship.Id);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}

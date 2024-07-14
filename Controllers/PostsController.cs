using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using socialMedia.Models;
using socialMedia.Repositories;
using Microsoft.Extensions.Logging;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IRepository<Post> repository, IRepository<User> userRepository, ILogger<PostsController> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts([FromQuery] int? userId)
        {
            if (userId.HasValue)
            {
                var user = await _userRepository.GetById(userId.Value);
                if (user == null)
                {
                    return BadRequest("Invalid user ID");
                }

                var posts = await _repository.FindAsync(p => p.UserId == userId.Value);
                var orderedPosts = posts.OrderByDescending(p => p.CreatedAt).ToList();
                return Ok(orderedPosts);
            }
            else
            {
                var posts = await _repository.GetAll();
                var orderedPosts = posts.OrderByDescending(p => p.CreatedAt).ToList();
                return Ok(orderedPosts);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _repository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetById(post.UserId);
            if (user == null)
            {
                return BadRequest("Invalid user ID");
            }

            await _repository.Add(post);
            await _repository.SaveAsync();
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Update(post);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _repository.Delete(id);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}

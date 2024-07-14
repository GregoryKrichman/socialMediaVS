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
    public class LikesController : ControllerBase
    {
        private readonly ICompositeKeyRepository<Like> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly ILogger<LikesController> _logger;

        public LikesController(ICompositeKeyRepository<Like> repository, IRepository<User> userRepository, IRepository<Post> postRepository, ILogger<LikesController> logger)
        {
            _repository = repository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Like>>> GetLikes([FromQuery] int postId)
        {
            var likes = await _repository.GetAll();
            return Ok(likes.Where(l => l.PostId == postId).ToList());
        }

        [HttpPost("toggle")]
        public async Task<ActionResult> ToggleLike([FromBody] Like like)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetById(like.UserId);
            var post = await _postRepository.GetById(like.PostId);

            if (user == null || post == null)
            {
                return BadRequest("User or Post not found");
            }

            var existingLike = await _repository.FirstOrDefaultAsync(l => l.UserId == like.UserId && l.PostId == like.PostId);
            if (existingLike != null)
            {
                await _repository.Delete(existingLike.UserId, existingLike.PostId);
                await _repository.SaveAsync();
                return Ok(new { liked = false });
            }
            else
            {
                like.User = user;
                like.Post = post;

                await _repository.Add(like);
                await _repository.SaveAsync();
                return Ok(new { liked = true });
            }
        }
    }
}

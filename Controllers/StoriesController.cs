using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using socialMedia.Models;
using socialMedia.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IRepository<Story> _repository;
        private readonly IWebHostEnvironment _env;

        public StoriesController(IRepository<Story> repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<Story>> AddStory([FromForm] IFormFile file, [FromForm] int userId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var story = new Story
            {
                Img = fileName,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.Add(story);
            return CreatedAtAction(nameof(GetStory), new { id = story.Id }, story);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Story>> GetStory(int id)
        {
            var story = await _repository.GetById(id);
            if (story == null)
            {
                return NotFound();
            }
            return Ok(story);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStory(int id, [FromBody] Story story)
        {
            if (id != story.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Update(story);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using socialMedia.Models;
using socialMedia.Repositories;
using socialMedia.Dtos;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IRepository<User> repository, IWebHostEnvironment env, ILogger<UsersController> logger)
        {
            _repository = repository;
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("find/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            _logger.LogInformation("Received request to get user by ID: {Id}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {Id}", id);
                return BadRequest("Invalid user ID");
            }

            var user = await _repository.GetById(id);
            if (user == null)
            {
                _logger.LogWarning("User not found for ID: {Id}", id);
                return NotFound();
            }

            if (!string.IsNullOrEmpty(user.ProfilePic))
            {
                user.ProfilePic = $"{Request.Scheme}://{Request.Host}/uploads/{user.ProfilePic}";
            }

            if (!string.IsNullOrEmpty(user.CoverPic))
            {
                user.CoverPic = $"{Request.Scheme}://{Request.Host}/uploads/{user.CoverPic}";
            }

            _logger.LogInformation("User found: {User}", user);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Received request to update user with ID: {Id}", id);

            if (id <= 0)
            {
                _logger.LogWarning("Invalid user ID: {Id}", id);
                return BadRequest("Invalid user ID");
            }

            var user = await _repository.GetById(id);
            if (user == null)
            {
                _logger.LogWarning("User not found for ID: {Id}", id);
                return NotFound("User not found");
            }

            
            _logger.LogInformation("UpdateUserDto received: {Dto}", updateUserDto);

            bool hasChanges = false;

            if (!string.IsNullOrEmpty(updateUserDto.Name) && updateUserDto.Name != user.Name)
            {
                user.Name = updateUserDto.Name;
                hasChanges = true;
            }

            if (!string.IsNullOrEmpty(updateUserDto.City) && updateUserDto.City != user.City)
            {
                user.City = updateUserDto.City;
                hasChanges = true;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Website) && updateUserDto.Website != user.Website)
            {
                user.Website = updateUserDto.Website;
                hasChanges = true;
            }

            if (updateUserDto.ProfilePic != null)
            {
                var profilePicPath = await SaveFileAsync(updateUserDto.ProfilePic);
                user.ProfilePic = profilePicPath;
                hasChanges = true;
            }

            if (updateUserDto.CoverPic != null)
            {
                var coverPicPath = await SaveFileAsync(updateUserDto.CoverPic);
                user.CoverPic = coverPicPath;
                hasChanges = true;
            }

            if (!hasChanges)
            {
                return BadRequest("No changes made");
            }

            await _repository.Update(user);
            _logger.LogInformation("User with ID: {Id} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _repository.Delete(id);
            return NoContent();
        }

        [HttpGet("suggestions")]
        public async Task<ActionResult<IEnumerable<User>>> GetSuggestions()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("onlineFriends")]
        public async Task<ActionResult<IEnumerable<User>>> GetOnlineFriends()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpGet("latestActivities")]
        public async Task<ActionResult<IEnumerable<User>>> GetLatestActivities()
        {
            return Ok(await _repository.GetAll());
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
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

            return fileName;
        }
    }
}

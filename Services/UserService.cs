using System;
using System.Threading.Tasks;
using socialMedia.Dtos;
using socialMedia.Models;
using socialMedia.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace socialMedia.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IRepository<User> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Token, string[] Errors)> RegisterUserAsync(RegisterDto registerDto)
        {
            bool success = false;
            string token = null;
            string[] errors = null;

            try
            {
                var user = new User
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    Name = registerDto.Name,
                    CoverPic = registerDto.CoverPic,
                    ProfilePic = registerDto.ProfilePic,
                    City = registerDto.City,
                    Website = registerDto.Website
                };

                await _userRepository.AddAsync(user);
                await _userRepository.SaveAsync();

                token = GenerateJwtToken(user);
                success = true;
            }
            catch (Exception ex)
            {
                errors = new[] { ex.Message };
            }

            return (success, token, errors);
        }

        public async Task<(bool Success, string Token, string[] Errors)> LoginUserAsync(LoginDto loginDto)
        {
            bool success = false;
            string token = null;
            string[] errors = null;

            try
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

                if (user == null)
                {
                    errors = new[] { "Invalid username or password" };
                }
                else
                {
                    token = GenerateJwtToken(user);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                errors = new[] { ex.Message };
            }

            return (success, token, errors);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

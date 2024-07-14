using System.Threading.Tasks;
using socialMedia.Dtos;

namespace socialMedia.Services
{
    public interface IUserService
    {
        Task<(bool Success, string Token, string[] Errors)> RegisterUserAsync(RegisterDto registerDto);
        Task<(bool Success, string Token, string[] Errors)> LoginUserAsync(LoginDto loginDto);
    }
}

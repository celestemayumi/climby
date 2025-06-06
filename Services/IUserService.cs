using climby.DTOs;
using climby.Models;

namespace climby.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<UserDto> CreateUserAsync(UserInfoDto dto);
        Task<UserDto> UpdateUserAsync(int id, UserInfoDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}

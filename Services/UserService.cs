using climby.Data;
using climby.DTOs;
using climby.Models;
using Microsoft.EntityFrameworkCore;

namespace climby.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.ID,
                    Name = u.Name,
                    Email = u.Email,
                    Country = u.Country,
                    City = u.City
                })
                .ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.ID,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                City = user.City
            };
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserDto> CreateUserAsync(UserInfoDto dto)
        {
            // Opcional: impedir cadastro com email repetido
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Este e-mail já está em uso.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Country = dto.Country,
                City = dto.City,
                Password = dto.Password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.ID,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                City = user.City
            };
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserInfoDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Country = dto.Country;
            user.City = dto.City;
            user.Password = dto.Password;

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.ID,
                Name = user.Name,
                Email = user.Email,
                Country = user.Country,
                City = user.City
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

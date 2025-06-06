using climby.Models;

namespace climby.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
        Task<bool> SaveChangesAsync();
    }
}

using climby.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace climby.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByFirebaseUidAsync(string firebaseUid);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}

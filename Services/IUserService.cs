using climby.DTOs;

namespace climby.Services
{
    public interface IUserService
    {
        Task<UserInfoDto?> GetUserByFirebaseUidAsync(string firebaseUid);
        Task<UserInfoDto> CreateUserAsync(string firebaseUid, UserInfoDto dto);
        Task<UserInfoDto?> UpdateUserAsync(string firebaseUid, UserInfoDto dto);
        Task<bool> DeleteUserAsync(string firebaseUid);
    }
}

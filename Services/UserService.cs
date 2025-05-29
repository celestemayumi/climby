using climby.DTOs;
using climby.Models;
using climby.Repositories;
using climby.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserInfoDto?> GetUserByFirebaseUidAsync(string firebaseUid)
    {
        var user = await _userRepository.GetByFirebaseUidAsync(firebaseUid);
        if (user == null) return null;
        return MapToDTO(user);
    }

    public async Task<UserInfoDto> CreateUserAsync(string firebaseUid, UserInfoDto dto)
    {
        // Validação básica
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Email))
            throw new ArgumentException("Email é obrigatório.");

        var existingUser = await _userRepository.GetByFirebaseUidAsync(firebaseUid);
        if (existingUser != null)
            throw new InvalidOperationException("Usuário já existe.");

        var user = new User
        {
            Firebase_uid = firebaseUid,
            Name = dto.Name!,
            Email = dto.Email!,
            Country = dto.Country ?? "",
            City = dto.City ?? ""
        };
        await _userRepository.CreateAsync(user);
        return MapToDTO(user);
    }

    public async Task<UserInfoDto?> UpdateUserAsync(string firebaseUid, UserInfoDto dto)
    {
        var user = await _userRepository.GetByFirebaseUidAsync(firebaseUid);
        if (user == null) return null;

        // Atualiza só os campos que vieram não nulos
        if (!string.IsNullOrWhiteSpace(dto.Name))
            user.Name = dto.Name;
        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.Email = dto.Email;
        if (dto.Country != null)
            user.Country = dto.Country;
        if (dto.City != null)
            user.City = dto.City;

        await _userRepository.UpdateAsync(user);

        return MapToDTO(user);
    }

    public async Task<bool> DeleteUserAsync(string firebaseUid)
    {
        var user = await _userRepository.GetByFirebaseUidAsync(firebaseUid);
        if (user == null) return false;

        await _userRepository.DeleteAsync(user);
        return true;
    }

    private UserInfoDto MapToDTO(User user)
    {
        return new UserInfoDto
        {
            Name = user.Name,
            Email = user.Email,
            Country = user.Country,
            City = user.City
        };
    }
}
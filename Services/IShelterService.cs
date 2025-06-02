using climby.DTOs;

namespace climby.Services
{
    public interface IShelterService
    {
        Task<IEnumerable<ShelterResponseDto>> GetAllByCityAsync(string city);
    }
}

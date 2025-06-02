using climby.DTOs;

namespace climby.Repositories
{
    public interface IShelterRepository
    {
        Task<IEnumerable<ShelterResponseDto>> GetAllByCityAsync(string city);
    }
}

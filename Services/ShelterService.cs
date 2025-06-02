using climby.DTOs;
using climby.Repositories;

namespace climby.Services
{
    public class ShelterService : IShelterService
    {
        private readonly IShelterRepository _shelterRepository;

        public ShelterService(IShelterRepository shelterRepository)
        {
            _shelterRepository = shelterRepository;
        }

        public async Task<IEnumerable<ShelterResponseDto>> GetAllByCityAsync(string city)
        {
            return await _shelterRepository.GetAllByCityAsync(city);
        }
    }
}

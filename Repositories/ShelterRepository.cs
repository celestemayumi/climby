using climby.Data;
using climby.DTOs;
using Microsoft.EntityFrameworkCore;

namespace climby.Repositories
{
    public class ShelterRepository : IShelterRepository
    {
        private readonly AppDbContext _context;

        public ShelterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShelterResponseDto>> GetAllByCityAsync(string city)
        {
            city = city.ToLower();

            return await _context.Shelters
                .Where(s => s.City != null && s.City.ToLower() == city)
                .Select(s => new ShelterResponseDto
                {
                    Name = s.Name,
                    Phone = s.Phone,
                    Adress = s.Adress,
                    AdressNumber = s.AdressNumber,
                    District = s.District,
                    IsFull = s.IsFull
                })
                .ToListAsync();
        }
    }
}

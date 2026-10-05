using CityTransport.Data;
using CityTransport.DTOs.Tariffs;
using Microsoft.EntityFrameworkCore;

namespace CityTransport.Services
{
    public class TariffService : ITariffService
    {
        private readonly AppDbContext _context; 

        public TariffService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TariffResponseDto>> GetActiveTariffsAsync()
        {
            var activeTariffs = await _context.Tariffs
                .Include(t => t.Routes) 
                .Where(t => t.IsActive) 
                .ToListAsync();

            return activeTariffs.Select(tariff => new TariffResponseDto
            {
                Id = tariff.Id,
                Name = tariff.Name,
                Price = tariff.Price,
                DurationMinutes = tariff.DurationMinutes,
                IsActive = tariff.IsActive,
                RouteNames = tariff.Routes?.Select(r => r.Name).ToList() ?? new()
            });
        }
    }
}

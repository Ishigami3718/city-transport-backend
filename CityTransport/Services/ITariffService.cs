using CityTransport.DTOs.Tariffs;

namespace CityTransport.Services
{
    public interface ITariffService
    {
        Task<IEnumerable<TariffResponseDto>> GetActiveTariffsAsync();

    }
}

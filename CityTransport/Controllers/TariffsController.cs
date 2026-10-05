using CityTransport.DTOs.Tariffs;
using CityTransport.DTOs.Tickets;
using CityTransport.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityTransport.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TariffsController : ControllerBase
    {
        private readonly ITariffService _tariffService;
        private readonly ILogger<TariffsController> _logger;

        public TariffsController(ITariffService tariffService, ILogger<TariffsController> logger)
        {
            _tariffService = tariffService;
            _logger = logger;
        }

        [HttpGet("active")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TariffResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TariffResponseDto>>> GetActiveTariffs()
        {
            try
            {
                var tariffs = await _tariffService.GetActiveTariffsAsync();
                return Ok(tariffs);
            }
         catch (Exception ex)
        {
                _logger.LogError(ex, "Error occurred while fetching active tariffs.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

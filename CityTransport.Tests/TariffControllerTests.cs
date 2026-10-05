using CityTransport.Controllers;
using CityTransport.DTOs.Tariffs;
using CityTransport.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityTransport.Tests
{
    public class TariffControllerTests
    {
        [Fact]
        public async Task GetActiveTariffs_ReturnsOk_WithListOfTariffs()
        {
            var mockService = new Mock<ITariffService>();
            var mockLogger = new Mock<ILogger<TariffsController>>();

            var fakeTariffs = new List<TariffResponseDto>
        {
            new TariffResponseDto { Id = Guid.NewGuid(), Name = "Standard", Price = 15.00m, DurationMinutes = 60, IsActive = true }
        };

            mockService.Setup(s => s.GetActiveTariffsAsync()).ReturnsAsync(fakeTariffs);

            var controller = new TariffsController(mockService.Object, mockLogger.Object);

            var result = await controller.GetActiveTariffs();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnTariffs = Assert.IsAssignableFrom<IEnumerable<TariffResponseDto>>(okResult.Value);
            Assert.Single(returnTariffs);
        }
    }
}

using CityTransport.Controllers;
using CityTransport.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityTransport.Tests
{
    public class AuthTests
    {
        [Fact]
        public async Task GetMyTickets_ReturnsUnauthorized_WhenUserClaimsAreMissing()
        {
            var mockService = new Mock<ITicketService>();
            var mockLogger = new Mock<ILogger<TicketsController>>();
            var controller = new TicketsController(mockService.Object, mockLogger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var result = await controller.GetMyTickets();

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}

using CityTransport.Entities;
using CityTransport.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CityTransport.Data;
using CityTransport.DTOs.Tariffs;
using CityTransport.Services;



namespace CityTransport.Tests
{
    public class TariffServiceTests
    {
        [Fact]
        public async Task GetActiveTariffsAsync_ReturnsOnlyActiveTariffs()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new AppDbContext(options);
            await context.Database.OpenConnectionAsync();
            await context.Database.EnsureCreatedAsync();

            context.Tariffs.Add(new Tariff
            {
                Id = Guid.NewGuid(),
                Name = "Active Tariff",
                Price = 20.00m,
                IsActive = true
            });

            context.Tariffs.Add(new Tariff
            {
                Id = Guid.NewGuid(),
                Name = "Old Tariff",
                Price = 10.00m,
                IsActive = false
            });

            await context.SaveChangesAsync();

            var service = new TariffService(context);

            var result = await service.GetActiveTariffsAsync();

            var tariffsList = result.ToList();
            Assert.Equal(3,tariffsList.Count);
            Assert.Equal("Загальний разовий", tariffsList[0].Name);
        }
    }
}

namespace CityTransport.DTOs.Tariffs
{
        public class TariffResponseDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public long DurationMinutes { get; set; }
            public bool IsActive { get; set; }

            // Передаємо список назв або ID пов'язаних маршрутів (або порожній список, якщо діє на всі)
            public List<string> RouteNames { get; set; } = new();
        }
}

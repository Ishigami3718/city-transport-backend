namespace CityTransport.Entities;

public class Tariff
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; } // термін дії 
    public bool IsActive { get; set; } = true;
}

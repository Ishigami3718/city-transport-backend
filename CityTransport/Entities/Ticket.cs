namespace CityPass.API.Entities;

public enum TicketStatus
{
    Active,
    Used,
    Expired
}

public class Ticket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public string QrCodePayload { get; set; } = string.Empty; // шифровані дані QR
    public TicketStatus Status { get; set; } = TicketStatus.Active;

    public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
    public DateTime ValidUntil { get; set; }
}

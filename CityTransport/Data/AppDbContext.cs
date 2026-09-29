using CityPass.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityPass.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Tariff> Tariffs => Set<Tariff>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // конфіг зв'язків та обмежень
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Ticket)
            .WithOne(t => t.Order)
            .HasForeignKey<Ticket>(t => t.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Transaction)
            .WithOne(tr => tr.Order)
            .HasForeignKey<Transaction>(tr => tr.OrderId);

        //seed Data
        modelBuilder.Entity<Tariff>().HasData(
            new Tariff { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Разовий квиток", Price = 15.00m, DurationMinutes = 60 },
            new Tariff { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Студентський разовий", Price = 7.50m, DurationMinutes = 60 }
        );
    }
}

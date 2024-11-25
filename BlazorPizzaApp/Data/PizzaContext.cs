using Microsoft.EntityFrameworkCore;

namespace BlazorPizzaApp;

public class PizzaContext : DbContext
{
    public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
    {
    }

    public DbSet<Pizza> Pizzas { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pizza>().Property(p=> p.GluttenFree).HasDefaultValue(true);

        modelBuilder.Entity<Pizza>().HasData(
            new Pizza { Id = 1, Name = "Margherita", Description = "Tomato, mozzarella, basil", Price = 5.99m, Vegetarian = false, GluttenFree = true },
            new Pizza { Id = 2, Name = "Pepperoni", Description = "Pepperoni, mozzarella, tomato", Price = 7.99m, Vegetarian = false, GluttenFree = true },
            new Pizza { Id = 3, Name = "Vegetarian", Description = "Peppers, onions, mushrooms, mozzarella, tomato", Price = 6.99m, Vegetarian = true, GluttenFree = true },
            new Pizza { Id = 4, Name = "Hawaiian", Description = "Ham, pineapple, mozzarella, tomato", Price = 8.99m, Vegetarian = false, GluttenFree = true }
        );
    }
}

using Microsoft.EntityFrameworkCore;

namespace BlazorPizzaApp;

public class PizzaService
{
    private readonly PizzaContext db;

    public PizzaService(PizzaContext db)
    {
        this.db = db;
    }
    public Pizza[] GetPizzas()
    {
        // var pizzas = new[]
        // {
        //     new Pizza { Id = 1, Name = "Margherita", Description = "Tomato, mozzarella, basil", Price = 5.99m, Vegetarian = true },
        //     new Pizza { Id = 2, Name = "Pepperoni", Description = "Pepperoni, mozzarella, tomato", Price = 7.99m, Vegetarian = false },
        //     new Pizza { Id = 3, Name = "Vegetarian", Description = "Peppers, onions, mushrooms, mozzarella, tomato", Price = 6.99m, Vegetarian = true },
        //     new Pizza { Id = 4, Name = "Hawaiian", Description = "Ham, pineapple, mozzarella, tomato", Price = 8.99m, Vegetarian = false },
        // };
        return db.Pizzas.ToArray();
    }

    public Pizza GetPizza(int pizzaId)
    {
        return db.Pizzas.Find(pizzaId)!;
    }

    public void AddPizza(Pizza pizza)
    {
        db.Pizzas.Add(pizza);
        db.SaveChanges();
    }

    public void UpdatePizza(Pizza pizza)
    {
        db.Pizzas.Update(pizza);
        db.SaveChanges();
    }

    public void DeletePizza(int pizzaId)
    {
        var pizza = db.Pizzas.Find(pizzaId);
        db.Pizzas.Remove(pizza!);
        db.SaveChanges();
    }
}

# CHAPTER 3: WORK WITH DATA

## Add Entity Framework and SqlLite to store and get pizzas

<details>
<summary>1. Add entity framework packages to the project</summary>

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

</details>
<details>
<summary>2. Add a database context. Create a PizzaStoreContext class that inheritance from EF class: DbContext. And create DbSet property of Pizza class.</summary>

```
using Microsoft.EntityFrameworkCore;

namespace BlazorPizzaApp;

public class PizzaStoreContext : DbContext
{
    public PizzaStoreContext(DbContextOptions<PizzaStoreContext> options) : base(options)
    {
    }

    public DbSet<Pizza> Pizzas { get; set; }
}
```

</details>
<details>
<summary>3. Add default data from PizzaService created previously, overriding the OnModelCreating method in PizzaStoreContext class </summary>

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pizza>().HasData(
            new Pizza { Id = 1, Name = "Margherita", Description = "Tomato, mozzarella, basil", Price = 5.99m, Vegetarian = false },
            new Pizza { Id = 2, Name = "Pepperoni", Description = "Pepperoni, mozzarella, tomato", Price = 7.99m, Vegetarian = false },
            new Pizza { Id = 3, Name = "Vegetarian", Description = "Peppers, onions, mushrooms, mozzarella, tomato", Price = 6.99m, Vegetarian = true },
            new Pizza { Id = 4, Name = "Hawaiian", Description = "Ham, pineapple, mozzarella, tomato", Price = 8.99m, Vegetarian = false }
        );
    }
```

</details>
<details>
<summary>4. Register PizzaStoreContext class in DI container as DBContext Service using SQLite provider</summary>

```
builder.Services.AddDbContext<PizzaStoreContext>(options =>
    options.UseSqlite("Data Source=pizza.db"));
```

</details>
<details>
<summary>5. Change PizzaService class to get all pizzas from database instead of the dummy values. Use Dependecy Injection pattern to inject PizzaStoreContext in the constructor</summary>

```
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
}
```

</details>

6. Run the application to check if pizzas are rendering

7. You should be able to see an error. No database or no tables exist. You need to create migrations.

## Migrations

To create your database schema, you need to use migrations of the entity framework.

1. Install the EF global cli tool: dotnet-ef 
`dotnet tool install --global dotnet-ef`
2. Inside project folder, run cli command to create the Initial Migration and check the result.
`dotnet ef migrations add InitialMigration -o Data/Migrations`
3. Run database update command to create the sqlLite database
`dotnet ef database update`
4. Run the application, and now you should be able to see the pizzas list.
<details>
<summary>5. Optional: Add the name field as mandatory in database.</summary>

`modelBuilder.Entity<Pizza>().Property(p => p.Name).IsRequired();`

</details>
6. Optional: Create a new migration and update the database. Use SQLite extension on Visual Studio Code to check the database file: https://marketplace.visualstudio.com/items?itemName=alexcvzz.vscode-sqlite
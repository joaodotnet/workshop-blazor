# CHAPTER 2 - Data Binding

## Handle Events

<details>
<summary>1. Add input text control with @onchange attribute and show the input value in below paragraph</summary>

```
<div>
    <p>Enter some text:</p>
    <input @onchange="OnChangeHandler">
    <p>@text</p>
</div> 
```

</details>
<details>
<summary>2. Use @bind attribute instead of @onchange attribute of the input control</summary>

```
 <div>
    <p>Enter some text:</p>
    <input @bind="text">
    <p>@text</p>
</div>
```

</details>
<details>
<summary>3. Add @bind:event attribute and bind to oninput html handler</summary>

```
<div>
    <p>Enter some text:</p>
    <input @bind="text" @bind:event="oninput">    
    <p>@text</p>
 </div> 
```

</details>
<details>
<summary>4. Add button to clear the paragraph with @onclick attribute</summary>

`<button @onclick="OnClearHandler">Clear</button>`

</details>
<details>
<summary>5. Implement a search function after every key pressed with  @bind:after attribute</summary>

```
<div>
    <p>Enter some text:</p>
    <input @bind="text" @bind:event="oninput" @bind:after="OnSearch">
    <button @onclick="OnClearHandler">Clear</button>
    <p>@text</p>
    <p>@result</p>
</div>

@code {
    string text = "";
    string result = "";
    private void OnChangeHandler(ChangeEventArgs e)
    {
        text = e.Value?.ToString()!;
    }
    private void OnClearHandler(MouseEventArgs e)
    {
        text = "";
    }

    async Task OnSearch()
    {
        result="Searching...";
        await Task.Delay(2000);
        result = $"Found {Random.Shared.Next(1, 100)} results";
    }
}
```

</details>

## Create New component (to list all pizzas)
<details>
<summary>1. In terminal create new component</summary>

`dotnet new razorcomponent -n Pizzas -o Components/Pages`

</details>
<details>
<summary>2.  Add the new page to the nav menu</summary>

```
 <div class="nav-item px-3">
    <NavLink class="nav-link" href="pizzas">
        <span class="bi bi-list-nested-nav-menu" aria-hidden="true"></span> Pizzas
    </NavLink>
</div>
```

</details>
<details>
<summary> 3. Create Pizza class with the following properties Id (int), Name (string), Description (string), Price (decimal), Vegetarian (bool)</summary>

```
public class Pizza
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool Vegetarian { get; set; }
}
```

</details>
<details>
<summary>4. In Pizzas.razor add a field for the list of pizzas items in the @code block.</summary>

```
@code {
    private Pizza[] pizzas = [];
}
```

</details>
<details>
<summary>5. Render an HTML table list of all the pizzas using a foreach loop.</summary>

```
<table class="table">
        <thead>
            <tr>
                <th>Nome</th>
                <th>Descrição</th>
                <th>Vegetariano</th>
                <th>Preço</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var pizza in pizzas)
            {
                <tr>
                    <td>@pizza.Name</td>
                    <td>@pizza.Description</td>
                    <td>@(pizza.Vegetarian ? "Sim" : "Não")</td>
                    <td>@pizza.Price.ToString("C")</td>
                </tr>
            }
        </tbody>
    </table>
```

</details>
<details>
<summary>6. Create PizzaService class to fetch all pizzas (for now, just return a array of Pizza object with dummy values)</summary>

```
public class PizzaService
{
    public Pizza[] GetPizzas()
    {
        var pizzas = new[]
        {
             new Pizza { Id = 1, Name = "Margherita", Description = "Tomato, mozzarella, basil", Price = 5.99m, Vegetarian = true },
             new Pizza { Id = 2, Name = "Pepperoni", Description = "Pepperoni, mozzarella, tomato", Price = 7.99m, Vegetarian = false },
             new Pizza { Id = 3, Name = "Vegetarian", Description = "Peppers, onions, mushrooms, mozzarella, tomato", Price = 6.99m, Vegetarian = true },
             new Pizza { Id = 4, Name = "Hawaiian", Description = "Ham, pineapple, mozzarella, tomato", Price = 8.99m, Vegetarian = false },
        };
        return pizzas;
    }
}
```

</details>
<details>
<summary>7. Register PizzaService in Dependecy Injector Container (DI) in Program.cs as Scopped Service</summary>

`builder.Services.AddScoped<PizzaService>();`

</details>
<details>
<summary>8. Inject PizzaService in Pizzas.razor and fetch all pizzas on OnInitialized method and set into pizzas field. After this step you should see all pizzas in page</summary>

```
@page "/pizzas"
@rendermode InteractiveServer
@inject PizzaService PizzaService

...

protected override void OnInitialized()
    {
        pizzas = PizzaService.GetPizzas();
    }
```

</details>
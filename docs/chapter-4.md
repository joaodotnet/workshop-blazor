# CHAPTER 4: Forms & Validation

## Add Form

<details>
<summary>1. In Pizzas.razor component, below the table, add a new section to add a pizza item to the list, using the EditForm built-in component: https://learn.microsoft.com/en-us/aspnet/core/blazor/forms/ </summary>

```
<hr>
<h3>Adicionar Pizza</h3>
<div class="row">
    <div class="col-md-6">
        <EditForm Model="newPizza" OnValidSubmit="AddPizza" FormName="pizzaForm">
            <DataAnnotationsValidator />
            <ValidationSummary />

            <div class="mb-3">
                <label for="name" class="form-label">Name:</label>
                <InputText id="name" @bind-Value="newPizza.Name" class="form-control" />
                <ValidationMessage For="() => newPizza.Name" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="description" class="form-label">Description:</label>
                <InputText id="description" @bind-Value="newPizza.Description" class="form-control" />
                <ValidationMessage For="() => newPizza.Description" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="price" class="form-label">Price:</label>
                <InputNumber id="price" @bind-Value="newPizza.Price" class="form-control" />
                <ValidationMessage For="() => newPizza.Price" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="vegetarian" class="form-label">Vegetarian:</label>
                <InputCheckbox id="vegetarian" @bind-Value="newPizza.Vegetarian" class="form-check-input" />
                <ValidationMessage For="() => newPizza.Vegetarian" class="text-danger" />
            </div>
            <button type="submit" class="btn btn-primary">Adicionar</button>
        </EditForm>
    </div>
</div>
```

</details>

<details>
<summary>2. Bind the EditForm (Model attribute) to a new filed called newpizza from Pizza class</summary>

```
@code {
    [SupplyParameterFromForm]
    protected Pizza newPizza { get; set; } = new();
}
```

</details>
<details>
<summary>3. On EditForm component add OnValidSubmit event handler and call a new method on PizzaService class to add the pizza to database.</summary>

```
// Pizzas.razor
protected void AddPizza()
    {
        PizzaService.AddPizza(newPizza);
        newPizza = new();       
        pizzas = PizzaService.GetPizzas();
    }
//PizzaService.cs

public void AddPizza(Pizza pizza)
    {
        db.Pizzas.Add(pizza);
        db.SaveChanges();
    }
```

</details>
<details>
<summary>4. In Pizzas.razor component add new column to the pizzas html table. For each row add a button that have @onclick event that passes the pizza.Id as parameter. Implement that event handler to show the selected pizza values into the new section below.</summary>

```
<td><button @onclick="() => OnEditHandler(pizza.Id)">Editar</button></td>

@code{
private void OnEditHandler(int pizzaId)
    {
        newPizza = pizzas.FirstOrDefault(p => p.Id == pizzaId)!;
    }
}
```

</details>
<details>
<summary>5. Change AddPizza event handler to call a new method on PizzaService to update the pizza selected. Change the labels (add button, and the section title according if is a new item or a edit item)</summary>

```
<hr>
<h3>@title</h3>
<div class="row">
    <div class="col-md-6">
        <EditForm Model="newPizza" OnValidSubmit="UpsertPizza">
            <DataAnnotationsValidator />
            <ValidationSummary />

            <div class="mb-3">
                <label for="name" class="form-label">Name:</label>
                <InputText id="name" @bind-Value="newPizza.Name" class="form-control" />
                <ValidationMessage For="() => newPizza.Name" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="description" class="form-label">Description:</label>
                <InputText id="description" @bind-Value="newPizza.Description" class="form-control" />
                <ValidationMessage For="() => newPizza.Description" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="price" class="form-label">Price:</label>
                <InputNumber id="price" @bind-Value="newPizza.Price" class="form-control" />
                <ValidationMessage For="() => newPizza.Price" class="text-danger" />
            </div>
            <div class="mb-3">
                <label for="vegetarian" class="form-label">Vegetarian:</label>
                <InputCheckbox id="vegetarian" @bind-Value="newPizza.Vegetarian" class="form-check-input" />
                <ValidationMessage For="() => newPizza.Vegetarian" class="text-danger" />
            </div>
            <button type="submit" class="btn btn-primary">@buttonText</button>
            <button type="button" class="btn btn-secondary" @onclick="ResetForm">Cancel</button>
        </EditForm>
    </div>
</div>

@code{

private string title = "Adicionar Pizza";
private string buttonText = "Adicionar";

protected void UpsertPizza()
    {
        if(newPizza.Id == 0)
        {
            PizzaService.AddPizza(newPizza);
            newPizza = new();
        }
        else
        {
            PizzaService.UpdatePizza(newPizza);
            newPizza = new();
        }
        pizzas = PizzaService.GetPizzas();
        title = "Adicionar Pizza";
        buttonText = "Adicionar";
    }
}

//PizzaService.cs

public void UpdatePizza(Pizza pizza)
    {
        db.Pizzas.Update(pizza);
        db.SaveChanges();
    }
```

</details>
<details>
<summary>6. Add a new column to the table to add a delete pizza item. Implement all the logic to delete a pizza item.</summary>

```
<td><button @onclick="() => OnDeleteHandler(pizza.Id)">Apagar</button></td>

@code{
 private void OnDeleteHandler(int pizzaId)
    {
        PizzaService.DeletePizza(pizzaId);
        pizzas = PizzaService.GetPizzas();
    }
}

//PizzaService.cs
public void DeletePizza(int pizzaId)
    {
        var pizza = db.Pizzas.Find(pizzaId);
        db.Pizzas.Remove(pizza!);
        db.SaveChanges();
    }
```

</details>
<details>
<summary>7. Implement reset/cancel button on the EditForm component.</summary>

```
   <button type="button" class="btn btn-secondary" @onclick="ResetForm">Cancel</button>
</EditForm>

@code {
private void ResetForm(MouseEventArgs e)
    {
        newPizza = new();
        title = "Adicionar Pizza";
        buttonText = "Adicionar";
    }
}
```

</details>

## Add additional validation

<details>
<summary>1. Add new validation to the Editform to not allow to save a new item without a name and the price must be between 1 and 100</summary>

```
public class Pizza
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Display(Name = "Descrição")]
    public string? Description { get; set; }
    [Required]
    [Range(1,double.MaxValue)]
    public decimal Price { get; set; }
    [Display(Name = "Vegetariano")]
    public bool Vegetarian { get; set; }
}
```

</details>

## Optional: Use QuickGrid component and pagination

<details>
<summary>1. Replace the HTML table with QuickGrid built-in Component: https://learn.microsoft.com/en-us/aspnet/core/blazor/components/quickgrid</summary>

```
<QuickGrid Class="table" Items="pizzas">
        <PropertyColumn Property="p => p.Name" Sortable="true" />
        <PropertyColumn Property="p => p.Description" />
        <PropertyColumn Property="p => p.Vegetarian" />
        <PropertyColumn Property="p => p.Price" Sortable="true" />
        <TemplateColumn Context="p">
            <button class="btn btn-primary" @onclick="() => OnEditHandler(p.Id)">Editar</button>
        </TemplateColumn>
        <TemplateColumn Context="p">
            <button class="btn btn-danger" @onclick="() => OnDeleteHandler(p.Id)">Apagar</button>
        </TemplateColumn>
    </QuickGrid>

@code{
private IQueryable<Pizza> pizzas = null!;

protected override void OnInitialized()
    {
        pizzas = PizzaService.GetPizzas().AsQueryable();
    }
}
```

</details>
<details>
<summary>2. Add pagination to the QuickGrid to the component</summary>

```
<QuickGrid Class="table" Items="pizzas" Pagination="@pagination">
        <PropertyColumn Property="p => p.Name" Sortable="true" />
        <PropertyColumn Property="p => p.Description" />
        <PropertyColumn Property="p => p.Vegetarian" />
        <PropertyColumn Property="p => p.Price" Sortable="true" />
        <TemplateColumn Context="p">
            <button class="btn btn-primary" @onclick="() => OnEditHandler(p.Id)">Editar</button>
        </TemplateColumn>
        <TemplateColumn Context="p">
            <button class="btn btn-danger" @onclick="() => OnDeleteHandler(p.Id)">Apagar</button>
        </TemplateColumn>
    </QuickGrid>
    <Paginator State="@pagination" />

@code{
private PaginationState pagination = new() { ItemsPerPage = 5 };
}
```

</details>
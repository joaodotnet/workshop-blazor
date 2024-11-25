using System.ComponentModel.DataAnnotations;

namespace BlazorPizzaApp;

public class Pizza
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Display(Name = "Descrição")]
    public string? Description { get; set; }
    [Required]
    [Range(1,100)]
    public decimal Price { get; set; }
    [Display(Name = "Vegetariano")]
    public bool Vegetarian { get; set; }
    public bool GluttenFree { get; set; }
}

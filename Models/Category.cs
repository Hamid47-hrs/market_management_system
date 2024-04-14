using System.ComponentModel.DataAnnotations;

namespace market_management_system.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";
    public string? Description { get; set; }

    // * Navigation Property for EF Core.
    public List<Product>? Products { get; set; }
}

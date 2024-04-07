using System.ComponentModel.DataAnnotations;

namespace market_management_system.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public double Price { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
};

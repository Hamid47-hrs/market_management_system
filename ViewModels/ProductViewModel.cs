using market_management_system.Models;

namespace market_management_system.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = [];
        public Product Product { get; set; } = new();
    }
};

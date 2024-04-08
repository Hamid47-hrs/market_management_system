using market_management_system.Models;

namespace market_management_system.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = [];
    }
};

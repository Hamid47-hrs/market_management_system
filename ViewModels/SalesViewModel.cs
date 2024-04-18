using System.ComponentModel.DataAnnotations;
using CoreBusiness;
using market_management_system.ViewModels.Validations;

namespace market_management_system.ViewModels
{
    public class SalesViewModel
    {
        public int SelectedCategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = [];

        public int SelectedProductId { get; set; }

        [Display(Name = "Quantity")]
        [SalesViewModel_EnsureProperQuantity]
        public int SellQuantity { get; set; }
    }
};

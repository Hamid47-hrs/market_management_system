using market_management_system.Models;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = CategoriesRepository.ReadCategories()
            };

            return View(salesViewModel);
        }

        public IActionResult SalesProductPartial(int productId)
        {
            var product = ProductsRepository.ReadProductById(productId);

            return PartialView("_ProductDetails", product);
        }
    }
};

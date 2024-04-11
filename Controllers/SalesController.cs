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

        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            var product = ProductsRepository.ReadProductById(salesViewModel.SelectedProductId);

            if (ModelState.IsValid)
            {
                // var newProduct = ProductsRepository.ReadProductById(salesViewModel.SelectedProductId);
                if (product != null && product.Quantity != null)
                {
                    TransactionsRepository.Add(
                        salesViewModel.SelectedProductId,
                        product.Name,
                        product.Price,
                        product.Quantity,
                        salesViewModel.SellQuantity,
                        "Cashier_1"
                    );

                    product.Quantity -= salesViewModel.SellQuantity;
                    ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, product);
                }
            }

            // ! If (!ModelState.IsValid)
            salesViewModel.SelectedCategoryId =
                (product?.CategoryId == null) ? 0 : product.CategoryId;
            salesViewModel.Categories = CategoriesRepository.ReadCategories();

            return View("Index", salesViewModel);
        }
    }
};

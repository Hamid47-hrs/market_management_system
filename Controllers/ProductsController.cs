using market_management_system.Models;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductsRepository.ReadProducts(loadCategory: true);

            return View(products);
        }

        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel
            {
                Categories = CategoriesRepository.ReadCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories = CategoriesRepository.ReadCategories();
                return View(productViewModel);
            }

            ProductsRepository.CreateProduct(productViewModel.Product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string? id)
        {
            int productId = int.Parse(id ?? "");

            var productViewModel = new ProductViewModel
            {
                Product = ProductsRepository.ReadProductById(productId) ?? new Product(),
                Categories = CategoriesRepository.ReadCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories = CategoriesRepository.ReadCategories();
                return View(productViewModel);
            }

            ProductsRepository.UpdateProduct(productViewModel.Product.Id, productViewModel.Product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string? id)
        {
            int productId = int.Parse(id ?? "");
            ProductsRepository.DeleteProduct(productId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = ProductsRepository.GetProductsByCategoryId(categoryId);

            return PartialView("_Products", products);
        }
    }
};

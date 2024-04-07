using market_management_system.Models;
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
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            ProductsRepository.CreateProduct(product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string? id)
        {
            int productId = int.Parse(id ?? "");
            Product product = ProductsRepository.ReadProductById(productId)!;

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            ProductsRepository.UpdateProduct(product.Id, product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string? id)
        {
            int productId = int.Parse(id ?? "");
            ProductsRepository.DeleteProduct(productId);

            return RedirectToAction(nameof(Index));
        }
    }
};

using market_management_system.Models;
using market_management_system.Plugins.Plugin.DataStore.SQL;
using market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers;

public class ProductsController : Controller
{
    private readonly ProductSQLRepository productRepository;
    private readonly CategorySQLRepository categoryRepository;

    public ProductsController(MarketContext db)
    {
        productRepository = ProductSQLRepository.GetInstance(db);
        categoryRepository = CategorySQLRepository.GetInstance(db);
    }

    public IActionResult Index()
    {
        var products = productRepository.ReadProducts(loadCategory: true);

        return View(products);
    }

    public IActionResult Add()
    {
        var productViewModel = new ProductViewModel
        {
            Categories = categoryRepository.ReadCategories()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            productViewModel.Categories = categoryRepository.ReadCategories();
            return View(productViewModel);
        }

        productRepository.CreateProduct(productViewModel.Product);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(string? id)
    {
        int productId = int.Parse(id ?? "");

        var productViewModel = new ProductViewModel
        {
            Product = productRepository.ReadProductById(productId) ?? new Product(),
            Categories = categoryRepository.ReadCategories()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            productViewModel.Categories = categoryRepository.ReadCategories();
            return View(productViewModel);
        }

        productRepository.UpdateProduct(productViewModel.Product.Id, productViewModel.Product);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(string? id)
    {
        int productId = int.Parse(id ?? "");
        productRepository.DeleteProduct(productId);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = productRepository.ReadProductsByCategoryId(categoryId);

        return PartialView("_Products", products);
    }
}

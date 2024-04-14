using market_management_system.Models;
using market_management_system.Plugins.Plugin.DataStore.SQL;
using market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers;

public class SalesController : Controller
{
    private readonly ProductSQLRepository productRepository;
    private readonly CategorySQLRepository categoryRepository;
    private readonly TransactionSQLRepository transactionRepository;

    public SalesController(MarketContext db)
    {
        productRepository = ProductSQLRepository.GetInstance(db);
        categoryRepository = CategorySQLRepository.GetInstance(db);
        transactionRepository = TransactionSQLRepository.GetInstance(db);
    }

    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel
        {
            Categories = categoryRepository.ReadCategories()
        };

        return View(salesViewModel);
    }

    public IActionResult SalesProductPartial(int productId)
    {
        var product = productRepository.ReadProductById(productId);

        return PartialView("_ProductDetails", product);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        var product = productRepository.ReadProductById(salesViewModel.SelectedProductId);

        if (ModelState.IsValid)
        {
            // var newProduct = productRepository.ReadProductById(salesViewModel.SelectedProductId);
            if (product != null && product.Quantity != null)
            {
                transactionRepository.Add(
                    salesViewModel.SelectedProductId,
                    product.Name,
                    product.Price,
                    product.Quantity,
                    salesViewModel.SellQuantity,
                    "Cashier_1"
                );

                product.Quantity -= salesViewModel.SellQuantity;
                productRepository.UpdateProduct(salesViewModel.SelectedProductId, product);
            }
        }

        // ! If (!ModelState.IsValid)
        salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId;
        salesViewModel.Categories = categoryRepository.ReadCategories();

        return View("Index", salesViewModel);
    }
}

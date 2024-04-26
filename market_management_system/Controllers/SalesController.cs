using market_management_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCase;

namespace market_management_system.Controllers
{
    [Authorize]
    public class SalesController : Controller
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewProductUseCase viewProductUseCase;
        private readonly IUpdateProductUseCase updateProductUseCase;
        private readonly IAddTransactionUseCase addTransactionUseCase;

        public SalesController(
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewProductUseCase viewProductUseCase,
            IUpdateProductUseCase updateProductUseCase,
            IAddTransactionUseCase addTransactionUseCase
        )
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewProductUseCase = viewProductUseCase;
            this.updateProductUseCase = updateProductUseCase;
            this.addTransactionUseCase = addTransactionUseCase;
        }

        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(salesViewModel);
        }

        public IActionResult SalesProductPartial(int productId)
        {
            var product = viewProductUseCase.Execute(productId);

            return PartialView("_ProductDetails", product);
        }

        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            var product = viewProductUseCase.Execute(salesViewModel.SelectedProductId);

            if (ModelState.IsValid)
            {
                // var newProduct = ProductsRepository.ReadProductById(salesViewModel.SelectedProductId);
                if (product != null && product.Quantity != null)
                {
                    addTransactionUseCase.Execute(
                        salesViewModel.SelectedProductId,
                        product.Name,
                        product.Price,
                        product.Quantity,
                        salesViewModel.SellQuantity,
                        "Cashier_1"
                    );

                    product.Quantity -= salesViewModel.SellQuantity;
                    updateProductUseCase.Execute(salesViewModel.SelectedProductId, product);
                }
            }

            // ! If (!ModelState.IsValid)
            salesViewModel.SelectedCategoryId =
                (product?.CategoryId == null) ? 0 : product.CategoryId;
            salesViewModel.Categories = viewCategoriesUseCase.Execute();

            return View("Index", salesViewModel);
        }
    }
};

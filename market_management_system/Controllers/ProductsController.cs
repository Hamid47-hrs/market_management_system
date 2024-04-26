using CoreBusiness;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

namespace market_management_system.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICreateProductUseCase createProductUseCase;
        private readonly IViewProductsUseCase viewProductsUseCase;
        private readonly IViewProductUseCase viewProductUseCase;
        private readonly IViewProductsByCategoryUseCase viewProductsByCategoryUseCase;
        private readonly IUpdateProductUseCase updateProductUseCase;
        private readonly IDeleteProductUseCase deleteProductUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;

        public ProductsController(
            ICreateProductUseCase createProductUseCase,
            IViewProductsUseCase viewProductsUseCase,
            IViewProductUseCase viewProductUseCase,
            IViewProductsByCategoryUseCase viewProductsByCategoryUseCase,
            IUpdateProductUseCase updateProductUseCase,
            IDeleteProductUseCase deleteProductUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase
        )
        {
            this.createProductUseCase = createProductUseCase;
            this.viewProductsUseCase = viewProductsUseCase;
            this.viewProductUseCase = viewProductUseCase;
            this.viewProductsByCategoryUseCase = viewProductsByCategoryUseCase;
            this.updateProductUseCase = updateProductUseCase;
            this.deleteProductUseCase = deleteProductUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
        }

        public IActionResult Index()
        {
            var products = viewProductsUseCase.Execute(loadCategory: true);

            return View(products);
        }

        public IActionResult Add()
        {
            var productViewModel = new ProductViewModel
            {
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories = viewCategoriesUseCase.Execute();
                return View(productViewModel);
            }

            createProductUseCase.Execute(productViewModel.Product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string? id)
        {
            int productId = int.Parse(id ?? "");

            var productViewModel = new ProductViewModel
            {
                Product = viewProductUseCase.Execute(productId) ?? new Product(),
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel.Categories = viewCategoriesUseCase.Execute();
                return View(productViewModel);
            }

            updateProductUseCase.Execute(productViewModel.Product.Id, productViewModel.Product);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string? id)
        {
            int productId = int.Parse(id ?? "");
            deleteProductUseCase.Execute(productId);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            var products = viewProductsByCategoryUseCase.Execute(categoryId);

            return PartialView("_Products", products);
        }
    }
};

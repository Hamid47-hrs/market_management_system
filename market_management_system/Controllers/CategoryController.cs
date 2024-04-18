using CoreBusiness;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;

namespace market_management_system.Controllers;

public class CategoryController : Controller
{
    private readonly ICreateCategoryUseCase createCategoryUseCase;
    private readonly IViewCategoriesUseCase viewCategoriesUseCase;
    private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
    private readonly IUpdateCategoryUseCase updateCategoryUseCase;
    private readonly IDeleteCategoryUseCase deleteCategoryUseCase;

    public CategoryController(
        ICreateCategoryUseCase createCategoryUseCase,
        IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
        IUpdateCategoryUseCase updateCategoryUseCase,
        IDeleteCategoryUseCase deleteCategoryUseCase
    )
    {
        this.createCategoryUseCase = createCategoryUseCase;
        this.viewCategoriesUseCase = viewCategoriesUseCase;
        this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
        this.updateCategoryUseCase = updateCategoryUseCase;
        this.deleteCategoryUseCase = deleteCategoryUseCase;
    }

    public IActionResult Index()
    {
        var categories = viewCategoriesUseCase.Execute();

        return View(categories);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        createCategoryUseCase.Execute(category);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit([FromRoute] string? id)
    {
        int categoryId = int.Parse(id ?? "");
        Category? category = viewSelectedCategoryUseCase.Execute(categoryId);

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        updateCategoryUseCase.Execute(category.Id, category);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(string? id)
    {
        int categoryId = int.Parse(id ?? "");
        deleteCategoryUseCase.Execute(categoryId);

        return RedirectToAction(nameof(Index));
    }
}

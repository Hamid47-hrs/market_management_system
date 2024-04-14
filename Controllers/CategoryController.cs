using market_management_system.Models;
using market_management_system.Plugins.Plugin.DataStore.SQL;
using market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers;

public class CategoryController : Controller
{
    private readonly CategorySQLRepository categoryRepository;

    public CategoryController(MarketContext db)
    {
        categoryRepository = CategorySQLRepository.GetInstance(db);
    }

    public IActionResult Index()
    {
        var categories = categoryRepository.ReadCategories();

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

        categoryRepository.CreateCategory(category);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit([FromRoute] string? id)
    {
        int categoryId = int.Parse(id ?? "");
        Category category = categoryRepository.ReadCategoryById(categoryId)!;

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        categoryRepository.UpdateCategory(category.Id, category);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(string? id)
    {
        int CategoryId = int.Parse(id ?? "");
        categoryRepository.DeleteCategory(CategoryId);

        return RedirectToAction(nameof(Index));
    }
}

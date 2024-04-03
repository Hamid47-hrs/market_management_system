using market_management_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.Controllers;

public class CategoryController : Controller
{
    public IActionResult Index()
    {
        var categories = CategoriesRepository.ReadCategories();
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

        CategoriesRepository.CreateCategory(category);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit([FromRoute] string? id)
    {
        int categoryId = int.Parse(id ?? "");
        Category category = CategoriesRepository.ReadCategoryById(categoryId)!;

        return View(category);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        CategoriesRepository.UpdateCategory(category.Id, category);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(string? id)
    {
        int CategoryId = int.Parse(id ?? "");
        CategoriesRepository.DeleteCategory(CategoryId);
        return RedirectToAction(nameof(Index));
    }
}

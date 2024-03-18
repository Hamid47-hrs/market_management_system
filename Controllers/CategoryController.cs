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

    public IActionResult Edit(string? id)
    {
        int CategoryId = int.Parse(id ?? "");
        Category category = CategoriesRepository.ReadCategoryById(CategoryId)!;

        return View(category);
    }
}

using market_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;

public class CategorySQLRepository
{
    private static CategorySQLRepository? _instance = null;
    private readonly MarketContext db;

    private CategorySQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public static CategorySQLRepository GetInstance(MarketContext db)
    {
        _instance ??= new CategorySQLRepository(db);

        return _instance;
    }

    public void CreateCategory(Category category)
    {
        db.Categories.Add(category);

        db.SaveChanges();
    }

    public IEnumerable<Category> ReadCategories()
    {
        return db.Categories.ToList();
    }

    public Category? ReadCategoryById(int categoryId)
    {
        return db.Categories.Find(categoryId);
    }

    public void UpdateCategory(int categoryId, Category category)
    {
        if (categoryId == category.Id)
        {
            var cat = db.Categories.Find(categoryId);

            if (cat != null)
            {
                cat.Name = category.Name;
                cat.Description = category.Description;

                db.SaveChanges();
            }
        }
    }

    public void DeleteCategory(int categoryId)
    {
        var category = db.Categories.Find(categoryId);

        if (category != null)
        {
            db.Categories.Remove(category);

            db.SaveChanges();
        }
    }
}

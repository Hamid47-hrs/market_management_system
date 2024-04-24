using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class CategorySQLRepository : ICategoryRepository
{
    private readonly MarketContext db;

    public CategorySQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public void CreateCategory(Category category)
    {
        db.Categories.Add(category);
        db.SaveChanges();
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
        if (categoryId != category.Id)
            return;

        var cat = db.Categories.Find(categoryId);

        if (cat != null)
        {
            cat.Name = category.Name;
            cat.Description = category.Description;
            db.SaveChanges();
        }
    }
}

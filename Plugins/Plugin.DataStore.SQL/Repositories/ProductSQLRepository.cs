using market_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;

public class ProductSQLRepository
{
    private static ProductSQLRepository? _instance = null;
    private readonly MarketContext db;

    private ProductSQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public static ProductSQLRepository GetInstance(MarketContext db)
    {
        _instance ??= new ProductSQLRepository(db);

        return _instance;
    }

    public void CreateProduct(Product product)
    {
        db.Products.Add(product);

        db.SaveChanges();
    }

    public IEnumerable<Product> ReadProducts(bool loadCategory = false)
    {
        if (!loadCategory)
            return db.Products.Include(x => x.Category).OrderBy(x => x.CategoryId).ToList();

        return db.Products.OrderBy(x => x.CategoryId).ToList();
    }

    public Product? ReadProductById(int productId, bool loadCategory = false)
    {
        if (!loadCategory)
            return db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);

        return db.Products.FirstOrDefault(x => x.Id == productId);
    }

    public IEnumerable<Product> ReadProductsByCategoryId(int categoryId)
    {
        return db.Products.Where(x => x.CategoryId == categoryId);
    }

    public void UpdateProduct(int productId, Product product)
    {
        if (productId == product.Id)
        {
            var prod = db.Products.Find(productId);

            if (prod != null)
            {
                prod.CategoryId = product.CategoryId;
                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;

                db.SaveChanges();
            }
        }
    }

    public void DeleteProduct(int productId)
    {
        var product = db.Categories.Find(productId);

        if (product != null)
        {
            db.Categories.Remove(product);

            db.SaveChanges();
        }
    }
}

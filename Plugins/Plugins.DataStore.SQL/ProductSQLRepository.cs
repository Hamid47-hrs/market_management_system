using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class ProductSQLRepository : IProductRepository
{
    private readonly MarketContext db;

    public ProductSQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public void CreateProduct(Product product)
    {
        db.Products.Add(product);
        db.SaveChanges();
    }

    public void DeleteProduct(int productId)
    {
        var product = db.Products.Find(productId);

        if (product != null)
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return db.Products.Where(x => x.CategoryId == categoryId).ToList();
    }

    public Product? ReadProductById(int productId, bool loadCategory = false)
    {
        if (loadCategory)
        {
            return db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == productId);
        }
        else
        {
            return db.Products.FirstOrDefault(x => x.Id == productId);
        }
    }

    public IEnumerable<Product> ReadProducts(bool loadCategory = false)
    {
        if (loadCategory)
        {
            return db.Products.Include(x => x.Category).OrderBy(x => x.Id).ToList();
        }
        else
        {
            return [.. db.Products.OrderBy(x => x.Id)];
        }
    }

    public void UpdateProduct(int productId, Product product)
    {
        if (productId == product.Id)
        {
            var prod = db.Products.Find(productId);

            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.CategoryId = product.CategoryId;

                db.SaveChanges();
            }
        }
    }
}

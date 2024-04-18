using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces;

public interface IProductRepository
{
    void CreateProduct(Product product);
    IEnumerable<Product> ReadProducts(bool loadCategory = false);
    Product? ReadProductById(int productId, bool loadCategory = false);
    void UpdateProduct(int productId, Product product);
    void DeleteProduct(int productId);
    IEnumerable<Product> GetProductsByCategoryId(int categoryId);
}

using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class ViewProductUseCase : IViewProductUseCase
{
    private readonly IProductRepository productRepository;

    public ViewProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public Product? Execute(int productId)
    {
        return productRepository.ReadProductById(productId);
    }
}

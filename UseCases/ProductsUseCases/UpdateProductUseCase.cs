using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class UpdateProductUseCase : IUpdateProductUseCase
{
    private readonly IProductRepository productRepository;

    public UpdateProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public void Execute(int productId, Product product)
    {
        productRepository.UpdateProduct(productId, product);
    }
}

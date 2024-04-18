using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class CreateProductUseCase : ICreateProductUseCase
{
    private readonly IProductRepository productRepository;

    public CreateProductUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public void Execute(Product product)
    {
        productRepository.CreateProduct(product);
    }
}

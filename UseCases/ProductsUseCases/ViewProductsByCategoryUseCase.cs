using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class ViewProductsByCategoryUseCase : IViewProductsByCategoryUseCase
{
    private readonly IProductRepository productRepository;

    public ViewProductsByCategoryUseCase(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public IEnumerable<Product> Execute(int categoryId)
    {
        return productRepository.GetProductsByCategoryId(categoryId);
    }
}

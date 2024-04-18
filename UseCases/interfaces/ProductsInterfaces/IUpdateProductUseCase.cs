using CoreBusiness;

namespace UseCases.ProductsUseCases;

public interface IUpdateProductUseCase
{
    void Execute(int productId, Product product);
}

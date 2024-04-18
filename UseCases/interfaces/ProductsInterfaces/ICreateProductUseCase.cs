using CoreBusiness;

namespace UseCases.ProductsUseCases;

public interface ICreateProductUseCase
{
    void Execute(Product product);
}

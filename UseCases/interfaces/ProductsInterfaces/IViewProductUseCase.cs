using CoreBusiness;

namespace UseCases.ProductsUseCases;

public interface IViewProductUseCase
{
    Product? Execute(int productId, bool loadCategory = false);
}

using CoreBusiness;

namespace UseCases.CategoriesUseCases;

public interface IUpdateCategoryUseCase
{
    void Execute(int categoryId, Category category);
}

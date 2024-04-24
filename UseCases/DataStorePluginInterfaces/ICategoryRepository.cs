using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface ICategoryRepository
    {
        void CreateCategory(Category category);
        IEnumerable<Category> ReadCategories();
        Category? ReadCategoryById(int categoryId);
        void UpdateCategory(int categoryId, Category category);
        void DeleteCategory(int categoryId);
    }
}

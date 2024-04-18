using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class CategoriesInMemoryRepository : ICategoryRepository
    {
        private  List<Category> _categories =
    [
        new Category
        {
            Id = 1,
            Name = "test1",
            Description =
                "aLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        },
        new Category
        {
            Id = 2,
            Name = "test2",
            Description =
                "aLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        },
        new Category
        {
            Id = 3,
            Name = "test3",
            Description =
                "aLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        },
        new Category
        {
            Id = 4,
            Name = "test4",
            Description =
                "aLorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
        }
    ];

        public  void CreateCategory(Category category)
        {
            if (_categories != null && _categories.Count > 0)
            {
                var lastId = _categories.Max(item => item.Id);
                category.Id = lastId + 1;
            }
            else
            {
                category.Id = 1;
            }

            _categories ??= [];

            _categories.Add(category);
        }

        public  IEnumerable<Category> ReadCategories() => _categories;

        public  Category? ReadCategoryById(int id)
        {
            var category = _categories.FirstOrDefault(item => item.Id == id);

            if (category != null)
            {
                return new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
            }

            return null;
        }

        public  void UpdateCategory(int id, Category newCategory)
        {
            if (id != newCategory.Id)
                return;

            var categoryToUpdate = _categories.FirstOrDefault(item => item.Id == id);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = newCategory.Name;
                categoryToUpdate.Description = newCategory.Description;
            }
        }

        public  void DeleteCategory(int id)
        {
            var category = _categories.FirstOrDefault(item => item.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}

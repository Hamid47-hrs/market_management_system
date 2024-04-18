using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.CategoriesUseCases
{
    public class CreateCategoryUseCase
    {
        private readonly ICategoryRepository categoryRepository;
        public CreateCategoryUseCase(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Eecute(Category cateogory)
        {
            categoryRepository.CreateCategory(cateogory);
        }
    }
}

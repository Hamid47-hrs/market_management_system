﻿namespace market_management_system.Models
{
    public class ProductsRepository
    {
        private static List<Product> _products =
        [
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "product1",
                Price = 11.11,
                Quantity = 2
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "product2",
                Price = 22.11,
                Quantity = 3
            },
            new Product
            {
                Id = 3,
                CategoryId = 2,
                Name = "product3",
                Price = 33.11,
                Quantity = 6
            },
            new Product
            {
                Id = 4,
                CategoryId = 3,
                Name = "product4",
                Price = 44.11,
                Quantity = 4
            },
            new Product
            {
                Id = 5,
                CategoryId = 3,
                Name = "product5",
                Price = 55.11,
                Quantity = 2
            },
            new Product
            {
                Id = 6,
                CategoryId = 3,
                Name = "product6",
                Price = 66.11,
                Quantity = 10
            },
        ];

        public static void CreateProduct(Product product)
        {
            var lastId = _products.Max(prod => prod.Id);
            product.Id = lastId + 1;
            _products.Add(product);
        }

        public static List<Product> ReadProducts(bool loadCategory = false)
        {
            if (!loadCategory)
                return _products;

            if (_products != null && _products.Count > 0)
            {
                _products.ForEach(prod =>
                {
                    if (prod.CategoryId > 0)
                        prod.Category = CategoriesRepository.ReadCategoryById(prod.CategoryId);
                });
            }
            return _products ?? new List<Product>();
        }

        public static Product? ReadProductById(int id, bool loadCategory = false)
        {
            var product = _products.FirstOrDefault(prod => prod.Id == id);

            if (product != null)
            {
                var product_ = new Product
                {
                    Id = product.Id,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                };

                if (loadCategory && product_.CategoryId > 0)
                    product_.Category = CategoriesRepository.ReadCategoryById(product_.CategoryId);

                return product_;
            }
            return null;
        }

        public static void UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return;

            var productToUpdate = _products.FirstOrDefault(prod => prod.Id == id);

            if (productToUpdate != null)
            {
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
            }
        }

        public static void DeleteProduct(int id)
        {
            var product = _products.FirstOrDefault(prod => prod.Id == id);

            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}

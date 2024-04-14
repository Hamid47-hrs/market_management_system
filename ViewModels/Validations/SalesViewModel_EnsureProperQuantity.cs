using System.ComponentModel.DataAnnotations;
using market_management_system.Models;
using market_management_system.Plugins.Plugin.DataStore.SQL;
using market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;

namespace market_management_system.ViewModels.Validations;

public class SalesViewModel_EnsureProperQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var serviceProvider =
            validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider
            ?? throw new InvalidOperationException("Cannot retrieve service provider.");
        var db = serviceProvider.GetRequiredService<MarketContext>();
        var productRepository = ProductSQLRepository.GetInstance(db);

        var salesViewModel = validationContext.ObjectInstance as SalesViewModel;

        if (salesViewModel != null)
        {
            if (salesViewModel.SellQuantity <= 0)
            {
                return new ValidationResult("The Quantity to sell must be grater than Zero.");
            }
            else
            {
                var product = productRepository.ReadProductById(salesViewModel.SelectedProductId);

                if (product != null)
                {
                    if (product.Quantity < salesViewModel.SellQuantity)
                    {
                        return new ValidationResult(
                            $"There is only {product.Quantity} {product.Name} left?!"
                        );
                    }
                }
                else
                {
                    return new ValidationResult("The Selected Product doesn't exist.");
                }
            }
        }

        return ValidationResult.Success;
    }
}

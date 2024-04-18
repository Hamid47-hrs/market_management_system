using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCase;

namespace market_management_system.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    private readonly IViewCashierTransactionsUseCase viewCashierTransactionsUseCase;

    public TransactionsViewComponent(IViewCashierTransactionsUseCase viewCashierTransactionsUseCase)
    {
        this.viewCashierTransactionsUseCase = viewCashierTransactionsUseCase;
    }

    public IViewComponentResult Invoke(string username)
    {
        var transactions = viewCashierTransactionsUseCase.Execute(DateTime.Now, username);

        return View(transactions);
    }
}

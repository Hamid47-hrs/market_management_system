using market_management_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string username)
    {
        var transactions = TransactionsRepository.ReadByDateAndCashier(DateTime.Now, username);

        return View(transactions);
    }
}

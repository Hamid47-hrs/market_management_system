using market_management_system.Models;
using market_management_system.Plugins.Plugin.DataStore.SQL;
using market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace market_management_system.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    private readonly TransactionSQLRepository transactionRepository;

    public TransactionsViewComponent(MarketContext db)
    {
        transactionRepository = TransactionSQLRepository.GetInstance(db);
    }

    public IViewComponentResult Invoke(string username)
    {
        var transactions = transactionRepository.ReadByDateAndCashier(DateTime.Now, username);

        return View(transactions);
    }
}

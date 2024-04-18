using CoreBusiness;
using market_management_system.ViewModels;
using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCase;

namespace market_management_system.Controllers;

public class TransactionsController : Controller
{
    private readonly ISearchTransactionsUseCase searchTransactionsUseCase;

    public TransactionsController(ISearchTransactionsUseCase searchTransactionsUseCase)
    {
        this.searchTransactionsUseCase = searchTransactionsUseCase;
    }

    public IActionResult Index()
    {
        TransactionsViewModel transactionsViewModel = new TransactionsViewModel();

        return View(transactionsViewModel);
    }

    public IActionResult Search(TransactionsViewModel transactionsViewModel)
    {
        var transactions = searchTransactionsUseCase.Execute(
            transactionsViewModel.CashierName ?? "",
            transactionsViewModel.StartDate,
            transactionsViewModel.EndDate
        );

        transactionsViewModel.Transactions = transactions;

        return View("Index", transactionsViewModel);
    }
}

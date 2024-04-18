using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCase;

public class SearchTransactionsUseCase : ISearchTransactionsUseCase
{
    private readonly ITransactionRepository transactionRepository;

    public SearchTransactionsUseCase(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> Execute(string cashier, DateTime startDate, DateTime endDate)
    {
        return transactionRepository.Search(cashier, startDate, endDate);
    }
}

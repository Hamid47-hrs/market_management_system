using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCase;

public class ViewCashierTransactionsUseCase : IViewCashierTransactionsUseCase
{
    private readonly ITransactionRepository transactionRepository;

    public ViewCashierTransactionsUseCase(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> Execute(DateTime date, string cashier)
    {
        return transactionRepository.ReadByDateAndCashier(date, cashier);
    }
}

using CoreBusiness;

namespace UseCases.TransactionsUseCase;

public interface IViewCashierTransactionsUseCase
{
    IEnumerable<Transaction> Execute(DateTime date, string cashier);
}

using CoreBusiness;

namespace UseCases.TransactionsUseCase;

public interface IViewCashierTransactionsUseCase
{
    IEnumerable<Transaction> Execute(string cashier, DateTime startDate, DateTime endDate);
}

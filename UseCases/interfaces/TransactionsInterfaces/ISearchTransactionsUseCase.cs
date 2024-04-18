using CoreBusiness;

namespace UseCases.TransactionsUseCase;

public interface ISearchTransactionsUseCase
{
    IEnumerable<Transaction> Execute(string cashier, DateTime startDate, DateTime endDate);
}

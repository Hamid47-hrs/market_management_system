using CoreBusiness;

namespace UseCases.TransactionsUseCase;

public interface ISearchTransactionsUseCase
{
    IEnumerable<Transaction> Execute(DateTime date, string cashier);
}

using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces;

public interface ITransactionRepository
{
    void Add(
        int productId,
        string productName,
        double price,
        int? qtyBefore,
        int qtySold,
        string cashier
    );
    IEnumerable<Transaction> ReadByDateAndCashier(DateTime date, string cashier);
    IEnumerable<Transaction> Search(string cashier, DateTime startDate, DateTime endDate);
}

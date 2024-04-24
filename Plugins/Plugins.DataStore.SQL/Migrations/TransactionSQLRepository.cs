using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class TransactionSQLRepository : ITransactionRepository
{
    private readonly MarketContext db;

    public TransactionSQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public void Add(
        int productId,
        string productName,
        double price,
        int? qtyBefore,
        int qtySold,
        string cashier
    )
    {
        var transaction = new Transaction()
        {
            ProductId = productId,
            ProductName = productName,
            TimeStamp = DateTime.Now,
            Price = price,
            QtyBefore = qtyBefore,
            QtySold = qtySold,
            CashierName = cashier
        };

        db.Transactions.Add(transaction);
        db.SaveChanges();
    }

    public IEnumerable<Transaction> ReadByDateAndCashier(DateTime date, string cashier)
    {
        if (string.IsNullOrWhiteSpace(cashier))
        {
            return db.Transactions.Where(x => x.TimeStamp == date.Date);
        }
        else
        {
            return db.Transactions.Where(x =>
                EF.Functions.Like(x.CashierName, $"%{cashier}%") && x.TimeStamp == date.Date
            );
        }
    }

    public IEnumerable<Transaction> Search(string cashier, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashier))
        {
            return db.Transactions.Where(x =>
                x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date
            );
        }
        else
        {
            return db.Transactions.Where(x =>
                EF.Functions.Like(x.CashierName, $"%{cashier}%")
                && x.TimeStamp >= startDate.Date
                && x.TimeStamp <= endDate.Date
            );
        }
    }
}

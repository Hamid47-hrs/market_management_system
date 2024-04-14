using market_management_system.Models;
using Microsoft.EntityFrameworkCore;

namespace market_management_system.Plugins.Plugin.DataStore.SQL.Repositories;

public class TransactionSQLRepository
{
    private static TransactionSQLRepository? _instance = null;
    private readonly MarketContext db;

    private TransactionSQLRepository(MarketContext db)
    {
        this.db = db;
    }

    public static TransactionSQLRepository GetInstance(MarketContext db)
    {
        _instance ??= new TransactionSQLRepository(db);

        return _instance;
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
        var transaction = new Transaction
        {
            ProductId = productId,
            ProductName = productName,
            TimeStamp = DateTime.Now,
            Price = price,
            QtyBefore = (qtyBefore == null) ? 0 : qtyBefore,
            QtySold = qtySold,
            CashierName = cashier
        };

        db.Transactions.Add(transaction);
        db.SaveChanges();
    }

    public IEnumerable<Transaction> ReadByDateAndCashier(DateTime date, string cashier)
    {
        if (string.IsNullOrWhiteSpace(cashier))
            return db.Transactions.Where(x => x.TimeStamp.Date <= date.Date);
        else
            return db.Transactions.Where(x =>
                EF.Functions.Like(x.CashierName, $"%{cashier}%") && x.TimeStamp.Date <= date.Date
            );
    }

    public IEnumerable<Transaction> Search(string cashier, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashier))
            return db.Transactions.Where(x =>
                x.TimeStamp.Date >= startDate.Date && x.TimeStamp <= endDate.Date
            );
        else
            return db.Transactions.Where(x =>
                EF.Functions.Like(x.CashierName, $"%{cashier}%")
                && x.TimeStamp.Date <= startDate.Date
                && x.TimeStamp.Date >= startDate.Date
            );
    }
}

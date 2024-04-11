namespace market_management_system.Models;

public static class TransactionsRepository
{
    private static List<Transaction> transactions = new List<Transaction>();

    public static IEnumerable<Transaction> ReadByDateAndCashier(DateTime date, string cashier)
    {
        if (string.IsNullOrWhiteSpace(cashier))
            return transactions.Where(x => x.TimeStamp.Date <= date);
        else
            return transactions.Where(x =>
                x.CashierName.ToLower().Contains(cashier.ToLower()) && x.TimeStamp.Date <= date
            );
    }

    public static IEnumerable<Transaction> Search(
        string cashier,
        DateTime startDate,
        DateTime endDate
    )
    {
        if (string.IsNullOrWhiteSpace(cashier))
            return transactions.Where(x =>
                x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date.AddDays(1).Date
            );
        else
            return transactions.Where(x =>
                x.CashierName.Contains(cashier, StringComparison.CurrentCultureIgnoreCase)
                && x.TimeStamp >= startDate.Date
                && x.TimeStamp <= endDate.Date.AddDays(1).Date
            );
    }

    public static void Add(
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
            TimeStamp = DateTime.Now,
            ProductId = productId,
            ProductName = productName,
            Price = price,
            QtyBefore = (qtyBefore == null) ? 0 : qtyBefore,
            QtySold = qtySold,
            CashierName = cashier
        };

        if (transactions != null && transactions.Count > 0)
        {
            int MaxId = transactions.Max(x => x.Id);
            transaction.Id = MaxId;
        }
        else
        {
            transaction.Id = 1;
        }

        transactions?.Add(transaction);
    }
}

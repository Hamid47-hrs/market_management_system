using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCase;

public class AddTransactionUseCase : IAddTransactionUseCase
{
    private readonly ITransactionRepository transactionRepository;

    public AddTransactionUseCase(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public void Execute(
        int productId,
        string productName,
        double price,
        int? qtyBefore,
        int qtySold,
        string cashier
    )
    {
        transactionRepository.Add(productId, productName, price, qtyBefore, qtySold, cashier);
    }
}

namespace UseCases.TransactionsUseCase;

public interface IAddTransactionUseCase
{
    void Execute(
        int productId,
        string productName,
        double price,
        int? qtyBefore,
        int qtySold,
        string cashier
    );
}

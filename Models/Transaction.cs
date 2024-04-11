namespace market_management_system.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public double Price { get; set; }
    public int? QtyBefore { get; set; }
    public int QtySold { get; set; }
    public string CashierName { get; set; } = "";
}

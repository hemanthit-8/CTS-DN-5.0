namespace SortingOrders;

/// <summary>
/// Represents a customer order to be sorted by total price.
/// </summary>
public class Order
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalPrice { get; set; }

    public Order(string orderId, string customerName, decimal totalPrice)
    {
        OrderId = orderId;
        CustomerName = customerName;
        TotalPrice = totalPrice;
    }

    public override string ToString() => $"[{OrderId}] {CustomerName} | Total: ${TotalPrice:N2}";
}

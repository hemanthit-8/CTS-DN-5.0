namespace InventoryManagement;

/// <summary>
/// Represents a single product held in the warehouse inventory.
/// </summary>
public class Product
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Product(string productId, string productName, int quantity, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString()
    {
        return $"[{ProductId}] {ProductName} | Qty: {Quantity} | Price: ${Price:N2}";
    }
}

namespace EcommerceSearch;

/// <summary>
/// A product as indexed for the e-commerce platform's search function.
/// </summary>
public class Product
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }

    public Product(string productId, string productName, string category)
    {
        ProductId = productId;
        ProductName = productName;
        Category = category;
    }

    public override string ToString() => $"[{ProductId}] {ProductName} ({Category})";
}

namespace InventoryManagement;

/// <summary>
/// Manages the warehouse inventory.
///
/// Data structure choice: Dictionary&lt;string, Product&gt; (a hash map).
/// Products are looked up, inserted, updated, and removed by ProductId far more
/// often than the full inventory is iterated in ProductId order, so a hash map
/// gives average O(1) performance for the three core operations instead of the
/// O(n) lookups a List/ArrayList would require.
/// </summary>
public class InventoryManager
{
    private readonly Dictionary<string, Product> _inventory = new();

    /// <summary>Adds a new product. Average O(1).</summary>
    public bool AddProduct(Product product)
    {
        if (_inventory.ContainsKey(product.ProductId))
        {
            Console.WriteLine($"Add failed: product '{product.ProductId}' already exists.");
            return false;
        }

        _inventory[product.ProductId] = product;
        return true;
    }

    /// <summary>Updates quantity and/or price for an existing product. Average O(1).</summary>
    public bool UpdateProduct(string productId, int? newQuantity = null, decimal? newPrice = null)
    {
        if (!_inventory.TryGetValue(productId, out var product))
        {
            Console.WriteLine($"Update failed: product '{productId}' not found.");
            return false;
        }

        if (newQuantity.HasValue) product.Quantity = newQuantity.Value;
        if (newPrice.HasValue) product.Price = newPrice.Value;
        return true;
    }

    /// <summary>Removes a product from inventory. Average O(1).</summary>
    public bool DeleteProduct(string productId)
    {
        return _inventory.Remove(productId);
    }

    /// <summary>Direct lookup by id. Average O(1).</summary>
    public Product? GetProduct(string productId)
    {
        _inventory.TryGetValue(productId, out var product);
        return product;
    }

    /// <summary>Returns every product currently in stock. O(n).</summary>
    public IEnumerable<Product> GetAllProducts() => _inventory.Values;

    public int Count => _inventory.Count;
}

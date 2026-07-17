namespace RetailInventory.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual List<Product> Products { get; set; } = new();
}

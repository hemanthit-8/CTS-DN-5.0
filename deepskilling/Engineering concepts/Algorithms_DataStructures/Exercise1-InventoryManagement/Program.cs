namespace InventoryManagement;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 1: Inventory Management System ===\n");

        var manager = new InventoryManager();

        // Add products
        manager.AddProduct(new Product("P001", "Wireless Mouse", 150, 799.00m));
        manager.AddProduct(new Product("P002", "Mechanical Keyboard", 80, 2499.00m));
        manager.AddProduct(new Product("P003", "USB-C Hub", 200, 1299.00m));

        Console.WriteLine("Inventory after adding products:");
        PrintInventory(manager);

        // Update a product
        manager.UpdateProduct("P002", newQuantity: 75, newPrice: 2399.00m);
        Console.WriteLine("\nInventory after updating P002 (quantity + price):");
        PrintInventory(manager);

        // Attempt duplicate add (demonstrates failure path)
        manager.AddProduct(new Product("P001", "Duplicate Mouse", 10, 99.00m));

        // Delete a product
        manager.DeleteProduct("P003");
        Console.WriteLine("\nInventory after deleting P003:");
        PrintInventory(manager);

        // Lookup
        var lookup = manager.GetProduct("P001");
        Console.WriteLine($"\nDirect lookup for P001: {lookup}");
    }

    private static void PrintInventory(InventoryManager manager)
    {
        foreach (var product in manager.GetAllProducts())
        {
            Console.WriteLine("  " + product);
        }
    }
}

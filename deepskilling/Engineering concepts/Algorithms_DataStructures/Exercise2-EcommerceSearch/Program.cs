namespace EcommerceSearch;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 2: E-commerce Platform Search Function ===\n");

        // Unsorted catalog, as products typically arrive (by insertion order, not name).
        var catalog = new[]
        {
            new Product("E101", "Yoga Mat", "Fitness"),
            new Product("E102", "Bluetooth Speaker", "Electronics"),
            new Product("E103", "Air Fryer", "Home"),
            new Product("E104", "Running Shoes", "Footwear"),
            new Product("E105", "Desk Lamp", "Home"),
            new Product("E106", "Noise Cancelling Headphones", "Electronics"),
            new Product("E107", "Cast Iron Skillet", "Home"),
            new Product("E108", "Hiking Backpack", "Outdoor"),
        };

        string target = "Desk Lamp";

        // --- Linear search: works directly on the unsorted catalog ---
        var (linearResult, linearComparisons) = SearchAlgorithms.LinearSearch(catalog, target);
        Console.WriteLine($"Linear search for \"{target}\":");
        Console.WriteLine($"  Result: {linearResult}");
        Console.WriteLine($"  Comparisons: {linearComparisons} (out of {catalog.Length} products)\n");

        // --- Binary search: requires the catalog sorted by name first ---
        var sortedCatalog = catalog.OrderBy(p => p.ProductName, StringComparer.OrdinalIgnoreCase).ToArray();
        Console.WriteLine("Catalog sorted by name for binary search:");
        foreach (var p in sortedCatalog) Console.WriteLine("  " + p);

        var (binaryResult, binaryComparisons) = SearchAlgorithms.BinarySearch(sortedCatalog, target);
        Console.WriteLine($"\nBinary search for \"{target}\":");
        Console.WriteLine($"  Result: {binaryResult}");
        Console.WriteLine($"  Comparisons: {binaryComparisons} (out of {sortedCatalog.Length} products)\n");

        // --- Search for something that doesn't exist, to show worst-case behavior ---
        string missing = "Drone";
        var (_, linearMiss) = SearchAlgorithms.LinearSearch(catalog, missing);
        var (_, binaryMiss) = SearchAlgorithms.BinarySearch(sortedCatalog, missing);
        Console.WriteLine($"Searching for a missing product (\"{missing}\"):");
        Console.WriteLine($"  Linear search comparisons: {linearMiss}");
        Console.WriteLine($"  Binary search comparisons: {binaryMiss}");
    }
}

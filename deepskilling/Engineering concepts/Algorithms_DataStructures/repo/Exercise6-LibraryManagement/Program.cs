namespace LibraryManagement;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 6: Library Management System ===\n");

        var catalog = new[]
        {
            new Book("B601", "Clean Code", "Robert C. Martin"),
            new Book("B602", "Introduction to Algorithms", "Cormen et al."),
            new Book("B603", "The Pragmatic Programmer", "Andrew Hunt"),
            new Book("B604", "Design Patterns", "Erich Gamma"),
            new Book("B605", "Refactoring", "Martin Fowler"),
            new Book("B606", "Effective Java", "Joshua Bloch"),
            new Book("B607", "The Mythical Man-Month", "Frederick Brooks"),
        };

        string target = "Refactoring";

        var (linearResult, linearComparisons) = SearchAlgorithms.LinearSearch(catalog, target);
        Console.WriteLine($"Linear search for \"{target}\":");
        Console.WriteLine($"  Result: {linearResult}");
        Console.WriteLine($"  Comparisons: {linearComparisons} (out of {catalog.Length} books)\n");

        var sortedCatalog = catalog.OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase).ToArray();
        Console.WriteLine("Catalog sorted by title for binary search:");
        foreach (var b in sortedCatalog) Console.WriteLine("  " + b);

        var (binaryResult, binaryComparisons) = SearchAlgorithms.BinarySearch(sortedCatalog, target);
        Console.WriteLine($"\nBinary search for \"{target}\":");
        Console.WriteLine($"  Result: {binaryResult}");
        Console.WriteLine($"  Comparisons: {binaryComparisons} (out of {sortedCatalog.Length} books)\n");

        string missing = "1984";
        var (_, linearMiss) = SearchAlgorithms.LinearSearch(catalog, missing);
        var (_, binaryMiss) = SearchAlgorithms.BinarySearch(sortedCatalog, missing);
        Console.WriteLine($"Searching for a book not in the catalog (\"{missing}\"):");
        Console.WriteLine($"  Linear search comparisons: {linearMiss}");
        Console.WriteLine($"  Binary search comparisons: {binaryMiss}");
    }
}

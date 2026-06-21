namespace SortingOrders;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 3: Sorting Customer Orders ===\n");

        var rawOrders = new[]
        {
            new Order("O301", "Aarav Mehta", 1250.00m),
            new Order("O302", "Priya Sharma", 320.50m),
            new Order("O303", "Rohan Gupta", 4899.99m),
            new Order("O304", "Sneha Iyer", 75.25m),
            new Order("O305", "Vikram Rao", 2100.00m),
            new Order("O306", "Ananya Das", 999.00m),
        };

        // Bubble Sort on a copy
        var bubbleOrders = (Order[])rawOrders.Clone();
        int bubbleComparisons = SortingAlgorithms.BubbleSort(bubbleOrders);
        Console.WriteLine("Bubble Sort result (highest value first):");
        PrintOrders(bubbleOrders);
        Console.WriteLine($"Comparisons made: {bubbleComparisons}\n");

        // Quick Sort on a fresh copy
        var quickOrders = (Order[])rawOrders.Clone();
        int quickComparisons = SortingAlgorithms.QuickSort(quickOrders);
        Console.WriteLine("Quick Sort result (highest value first):");
        PrintOrders(quickOrders);
        Console.WriteLine($"Comparisons made: {quickComparisons}");
    }

    private static void PrintOrders(Order[] orders)
    {
        foreach (var order in orders) Console.WriteLine("  " + order);
    }
}

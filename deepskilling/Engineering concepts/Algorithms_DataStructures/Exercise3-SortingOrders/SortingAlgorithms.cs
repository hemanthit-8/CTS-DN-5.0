namespace SortingOrders;

/// <summary>
/// Sorts orders by TotalPrice in descending order (highest-value orders first,
/// matching the scenario's goal of prioritizing high-value orders).
/// Both methods sort in place and return the number of comparisons made,
/// to make the performance difference between them visible.
/// </summary>
public static class SortingAlgorithms
{
    /// <summary>
    /// Bubble Sort: repeatedly steps through the array, swapping adjacent
    /// elements that are out of order. O(n^2) comparisons/swaps in the
    /// average and worst case; O(n) in the best case (already sorted)
    /// thanks to the early-exit flag below.
    /// </summary>
    public static int BubbleSort(Order[] orders)
    {
        int comparisons = 0;
        int n = orders.Length;

        for (int i = 0; i < n - 1; i++)
        {
            bool swapped = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                comparisons++;
                if (orders[j].TotalPrice < orders[j + 1].TotalPrice)
                {
                    (orders[j], orders[j + 1]) = (orders[j + 1], orders[j]);
                    swapped = true;
                }
            }
            if (!swapped) break; // already sorted, no need to continue
        }

        return comparisons;
    }

    /// <summary>
    /// Quick Sort: picks a pivot, partitions the array so larger values
    /// (for descending order) come before the pivot, then recurses on
    /// each side. O(n log n) on average; O(n^2) worst case for an
    /// already-sorted/adversarial input with this pivot strategy.
    /// </summary>
    public static int QuickSort(Order[] orders)
    {
        int comparisons = 0;
        QuickSortRecursive(orders, 0, orders.Length - 1, ref comparisons);
        return comparisons;
    }

    private static void QuickSortRecursive(Order[] orders, int low, int high, ref int comparisons)
    {
        if (low >= high) return;

        int pivotIndex = Partition(orders, low, high, ref comparisons);
        QuickSortRecursive(orders, low, pivotIndex - 1, ref comparisons);
        QuickSortRecursive(orders, pivotIndex + 1, high, ref comparisons);
    }

    private static int Partition(Order[] orders, int low, int high, ref int comparisons)
    {
        decimal pivot = orders[high].TotalPrice;
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            // Descending order: elements >= pivot go to the left side.
            if (orders[j].TotalPrice >= pivot)
            {
                i++;
                (orders[i], orders[j]) = (orders[j], orders[i]);
            }
        }

        (orders[i + 1], orders[high]) = (orders[high], orders[i + 1]);
        return i + 1;
    }
}

namespace EcommerceSearch;

/// <summary>
/// Search algorithms over a catalog of products, searching by ProductName.
/// Each method returns the matching Product (or null) and the number of
/// comparisons it made, so the two strategies can be compared directly.
/// </summary>
public static class SearchAlgorithms
{
    /// <summary>
    /// Scans the array from the start until a match is found.
    /// Works on data in ANY order. Best case O(1), average/worst case O(n).
    /// </summary>
    public static (Product? Result, int Comparisons) LinearSearch(Product[] products, string targetName)
    {
        int comparisons = 0;
        foreach (var product in products)
        {
            comparisons++;
            if (string.Equals(product.ProductName, targetName, StringComparison.OrdinalIgnoreCase))
            {
                return (product, comparisons);
            }
        }
        return (null, comparisons);
    }

    /// <summary>
    /// Repeatedly halves the search space by comparing against the middle element.
    /// REQUIRES the input array to be sorted by ProductName.
    /// Best case O(1), average/worst case O(log n).
    /// </summary>
    public static (Product? Result, int Comparisons) BinarySearch(Product[] sortedProducts, string targetName)
    {
        int comparisons = 0;
        int low = 0;
        int high = sortedProducts.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            comparisons++;
            int cmp = string.Compare(sortedProducts[mid].ProductName, targetName, StringComparison.OrdinalIgnoreCase);

            if (cmp == 0)
            {
                return (sortedProducts[mid], comparisons);
            }
            if (cmp < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return (null, comparisons);
    }
}

namespace LibraryManagement;

/// <summary>
/// Search algorithms for finding books by title.
/// (Exercise 2 used an iterative binary search; this one is implemented
/// recursively to show the alternative formulation of the same algorithm.)
/// </summary>
public static class SearchAlgorithms
{
    /// <summary>
    /// Scans the catalog from the start until a title match is found.
    /// Works regardless of catalog order. O(n) average/worst case.
    /// </summary>
    public static (Book? Result, int Comparisons) LinearSearch(Book[] books, string targetTitle)
    {
        int comparisons = 0;
        foreach (var book in books)
        {
            comparisons++;
            if (string.Equals(book.Title, targetTitle, StringComparison.OrdinalIgnoreCase))
            {
                return (book, comparisons);
            }
        }
        return (null, comparisons);
    }

    /// <summary>
    /// Recursive binary search. REQUIRES the input array to be sorted by Title.
    /// O(log n) average/worst case.
    /// </summary>
    public static (Book? Result, int Comparisons) BinarySearch(Book[] sortedBooks, string targetTitle)
    {
        int comparisons = 0;
        var result = BinarySearchRecursive(sortedBooks, targetTitle, 0, sortedBooks.Length - 1, ref comparisons);
        return (result, comparisons);
    }

    private static Book? BinarySearchRecursive(Book[] books, string targetTitle, int low, int high, ref int comparisons)
    {
        if (low > high) return null;

        int mid = low + (high - low) / 2;
        comparisons++;
        int cmp = string.Compare(books[mid].Title, targetTitle, StringComparison.OrdinalIgnoreCase);

        if (cmp == 0) return books[mid];
        if (cmp < 0) return BinarySearchRecursive(books, targetTitle, mid + 1, high, ref comparisons);
        return BinarySearchRecursive(books, targetTitle, low, mid - 1, ref comparisons);
    }
}

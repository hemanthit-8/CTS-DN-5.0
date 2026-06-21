# Exercise 2: E-commerce Platform Search Function

## Scenario
The platform's search needs to find a product by name quickly, even as the catalog grows to thousands or millions of items.

## Big O notation
Big O notation describes how the running time (or memory use) of an algorithm grows as the input size `n` grows, focusing on the dominant term and ignoring constant factors. It answers "how does this algorithm scale?" rather than "how many milliseconds does it take on my machine?" — which makes it possible to compare algorithms independent of hardware. Common classes, from fastest to slowest growth: O(1) constant, O(log n) logarithmic, O(n) linear, O(n log n) linearithmic, O(n²) quadratic.

### Best, average, and worst case for search
- **Best case**: the target is found immediately — the first element for linear search, or the middle element for binary search. Both are O(1) in the best case.
- **Average case**: the target is found somewhere in the middle of the search process. Linear search averages n/2 comparisons (O(n)); binary search averages close to its worst case, O(log n), because each step still halves the remaining space regardless of where the target sits.
- **Worst case**: the target is the last element checked, or absent entirely. Linear search makes n comparisons (O(n)); binary search makes ⌈log₂(n+1)⌉ comparisons (O(log n)).

## Implementation
- `Product.cs` — `ProductId`, `ProductName`, `Category`.
- `SearchAlgorithms.cs` — `LinearSearch` (works on data in any order) and `BinarySearch` (requires the array sorted by `ProductName` first), both returning the match and a comparison count for instructional purposes.
- `Program.cs` — builds an 8-product catalog, runs both searches for an existing product and a missing one, and prints the comparison counts side by side.

Run it:
```bash
dotnet run --project Exercise2-EcommerceSearch
```

## Time complexity comparison

| | Best case | Average case | Worst case | Precondition |
|---|---|---|---|---|
| Linear search | O(1) | O(n) | O(n) | None — works on unsorted data |
| Binary search | O(1) | O(log n) | O(log n) | Array must be sorted |

At catalog size n = 1,000,000, linear search can require up to 1,000,000 comparisons in the worst case, while binary search requires at most ⌈log₂(1,000,001)⌉ = 20 comparisons — a difference of five orders of magnitude.

## Which algorithm suits the platform, and why
Binary search is the better fit for a production e-commerce search-by-name feature: the catalog is large, read far more often than it changes (most browsing traffic is searches, not catalog edits), and a sorted index can be maintained incrementally. The one-time cost of sorting (O(n log n)) is amortized across the very large number of subsequent searches.

That said, two caveats matter in practice:
1. Binary search only helps for exact or prefix matches on the sort key. Real product search (fuzzy matching, multi-word queries, relevance ranking) is typically handled by a dedicated search index (e.g. Elasticsearch/inverted indexes) rather than binary search on a flat array — binary search is the right *building block* concept, not the literal production implementation.
2. If the catalog changes constantly (new products added every second) and reads are comparatively rare, the sorting overhead may outweigh the benefit, and a hash-based lookup (O(1) average, as used in Exercise 1) or linear search on a small dataset may be perfectly adequate.

# Exercise 6: Library Management System

## Scenario
Users need to search for books by title (or author), and the search needs to remain fast as the catalog grows.

## Linear search and binary search
- **Linear search** checks each book in sequence until a title match is found or the catalog is exhausted. It makes no assumptions about ordering, so it works on a freshly added, unsorted shelf of books, but its cost grows directly with the catalog size — O(n).
- **Binary search** repeatedly compares the target title against the middle book of the remaining range and discards the half that can't contain it, requiring the catalog to already be sorted by title. Each comparison eliminates roughly half the remaining search space, giving O(log n).

## Implementation
- `Book.cs` — `BookId`, `Title`, `Author`.
- `SearchAlgorithms.cs` — `LinearSearch` (iterative, any order) and `BinarySearch` (implemented **recursively** this time, to show the alternative formulation to Exercise 2's iterative version — functionally identical, same asymptotic complexity).
- `Program.cs` — builds a 7-book catalog, searches for an existing title and a missing one with both algorithms, printing comparison counts for each.

Run it:
```bash
dotnet run --project Exercise6-LibraryManagement
```

## Time complexity comparison

| | Average case | Worst case | Precondition |
|---|---|---|---|
| Linear search | O(n) | O(n) | None |
| Binary search | O(log n) | O(log n) | Catalog sorted by title |

In the demo, searching a 7-book catalog for "Refactoring" takes 5 comparisons with linear search but only 3 with binary search; for a missing title ("1984"), linear search exhausts all 7 comparisons while binary search still only needs 3. The gap widens sharply as the catalog grows — a library with 100,000 titles would need up to 100,000 comparisons in the linear worst case versus at most 17 for binary search.

## When to use each, based on data set size and order
- **Small catalogs** (a neighborhood library branch, a few hundred titles) or **catalogs that change constantly** (new arrivals shelved continuously, no time to re-sort) favor linear search — the O(n) cost is negligible at small n, and it avoids the overhead of maintaining sort order.
- **Large, relatively stable catalogs** (a university or national library system with hundreds of thousands of titles, sorted and re-indexed periodically) favor binary search — the upfront cost of keeping the catalog sorted pays for itself many times over across the volume of searches performed.
- A useful middle ground: keep an unsorted "new arrivals" list searched linearly, while the main catalog is sorted and indexed for binary search — new books move into the sorted catalog during a periodic re-index rather than requiring every insert to maintain sort order immediately.

# Exercise 3: Sorting Customer Orders

## Scenario
Customer orders need to be sorted by `TotalPrice` so high-value orders can be prioritized (e.g. fraud review, premium fulfillment, VIP support).

## Sorting algorithms overview
- **Bubble Sort**: repeatedly steps through the list, comparing and swapping adjacent elements that are out of order. After each full pass, the largest remaining unsorted element "bubbles" into its correct position. Simple to understand and implement, but inefficient on large inputs — O(n²) comparisons/swaps in the average and worst case.
- **Insertion Sort**: builds the sorted output one element at a time, taking each new element and inserting it into its correct position among the already-sorted elements. O(n²) worst case, but efficient (close to O(n)) on data that is already nearly sorted, and it sorts in place with minimal overhead — a common choice for small or mostly-sorted lists.
- **Quick Sort**: a divide-and-conquer algorithm that picks a pivot element, partitions the array so elements are arranged around it, then recursively sorts each partition. O(n log n) on average, which is why it's implemented alongside Bubble Sort here.
- **Merge Sort**: another divide-and-conquer algorithm — it recursively splits the array in half, sorts each half, then merges the two sorted halves back together. Guarantees O(n log n) in all cases (best, average, worst), at the cost of O(n) extra space for the merge step, unlike Quick Sort's in-place partitioning.

## Implementation
- `Order.cs` — `OrderId`, `CustomerName`, `TotalPrice`.
- `SortingAlgorithms.cs` — `BubbleSort` (with an early-exit flag for already-sorted input) and `QuickSort` (Lomuto partition scheme), both sorting descending by `TotalPrice` and returning a comparison count.
- `Program.cs` — sorts the same 6 sample orders with both algorithms and prints the comparison counts side by side; both produce identical, correctly ordered output.

Run it:
```bash
dotnet run --project Exercise3-SortingOrders
```

## Performance comparison

| | Best case | Average case | Worst case | Space | Stable? |
|---|---|---|---|---|---|
| Bubble Sort | O(n) (early exit on sorted input) | O(n²) | O(n²) | O(1) | Yes |
| Quick Sort | O(n log n) | O(n log n) | O(n²) (rare, poor pivot choices) | O(log n) (recursion stack) | No |

For 6 orders the difference is barely visible, but it grows fast: with 1,000 orders, Bubble Sort's worst case is on the order of 1,000,000 comparisons, while Quick Sort's average case is on the order of 1,000 × log₂(1,000) ≈ 10,000 — roughly two orders of magnitude fewer.

## Why Quick Sort is generally preferred over Bubble Sort
Quick Sort's O(n log n) average-case growth scales far better than Bubble Sort's O(n²) as the order volume increases — for a platform processing thousands of orders per batch, this is the difference between a sub-second sort and a sort that takes minutes. Quick Sort also tends to perform well in practice due to good cache locality from its in-place partitioning. Bubble Sort's only real advantages are conceptual simplicity and stability (it never reorders equal elements relative to each other); in production code, Bubble Sort is essentially never chosen over Quick Sort, Merge Sort, or a language's built-in sort (which is often a hybrid like Timsort or Introsort) — it mainly survives as a teaching tool because the swap-adjacent-elements idea is easy to visualize. The one caveat is Quick Sort's O(n²) worst case on adversarial or already-sorted input with a naive pivot choice (such as always picking the last element, as this implementation does); production-grade implementations typically use randomized or median-of-three pivot selection to make that worst case practically unreachable.

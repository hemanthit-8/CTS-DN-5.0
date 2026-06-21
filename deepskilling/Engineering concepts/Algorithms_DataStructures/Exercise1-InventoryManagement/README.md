# Exercise 1: Inventory Management System

## Scenario
A warehouse inventory system needs to store and retrieve product records efficiently as the catalog grows into the thousands or millions of SKUs.

## Why data structures and algorithms matter here
A warehouse system is read and written constantly: stock is added on receipt, decremented on every sale, and looked up on every order. If products were kept in an unordered list and every operation required scanning from the start, each add/update/delete/search would cost O(n) time. At inventory sizes in the tens of thousands, that turns a few operations per second into a noticeable lag, and at peak (e.g. a sale event) it can become a bottleneck that stalls checkout. Choosing the right data structure turns these operations from "search the whole warehouse" into "go directly to the shelf," which is the difference between a system that scales and one that doesn't.

## Candidate data structures
- **Array / ArrayList** — simple, cache-friendly, good for ordered iteration, but lookup/update/delete by ID is O(n) unless the array is kept sorted (and even then, insertion/deletion is O(n) due to shifting).
- **HashMap / Dictionary (chosen)** — keys products by `ProductId`, giving average O(1) add, update, delete, and lookup. This is the right fit because the system's dominant access pattern is "find/modify the product with this exact ID," not "find the 5th product in some order."
- **Balanced BST / sorted map** — useful if range queries (e.g. "all products priced between X and Y") or sorted iteration are common; trades O(1) average lookup for guaranteed O(log n) lookup plus ordering.

This project uses `Dictionary<string, Product>` in `InventoryManager.cs`, keyed on `ProductId`.

## Implementation
- `Product.cs` — the `Product` model (`ProductId`, `ProductName`, `Quantity`, `Price`).
- `InventoryManager.cs` — `AddProduct`, `UpdateProduct`, `DeleteProduct`, `GetProduct`, `GetAllProducts`.
- `Program.cs` — a runnable demo that exercises all operations, including a duplicate-add failure case.

Run it:
```bash
dotnet run --project Exercise1-InventoryManagement
```

## Time complexity analysis

| Operation | Complexity (average) | Complexity (worst case) | Why |
|---|---|---|---|
| Add | O(1) | O(n) | Hashing is O(1) on average; worst case occurs only on hash collisions/resizing, which is rare and amortized. |
| Update | O(1) | O(n) | Same as add — it's a hash lookup followed by an in-place field mutation. |
| Delete | O(1) | O(n) | Hash lookup plus bucket removal. |
| Lookup by ID | O(1) | O(n) | Direct hash-based access instead of scanning. |
| Iterate all products | O(n) | O(n) | Every entry must be visited once. |

For comparison, the same four operations on an unsorted `List<Product>` would be O(n) for add (if duplicate-checking), O(n) for update/delete/lookup (linear scan to find the matching ID), and O(n) to iterate — so the hash map is asymptotically better for every operation except raw iteration, where both are O(n).

## Optimization discussion
- **Composite indexes**: if the system also needs fast lookup by `ProductName` or `category`, a secondary `Dictionary<string, List<string>>` (name/category → product IDs) avoids falling back to a linear scan for those queries, at the cost of extra memory and the need to keep both structures in sync.
- **Load factor / resizing**: .NET's `Dictionary` automatically resizes and rehashes as it grows; pre-sizing the dictionary (e.g. `new Dictionary<string, Product>(expectedCount)`) when the approximate catalog size is known avoids repeated rehash operations during bulk loads.
- **Concurrency**: a real warehouse system has multiple workers updating stock simultaneously. `ConcurrentDictionary<string, Product>` (or row-level locking in a database-backed version) would be the next step to avoid race conditions on quantity updates.
- **Persistence**: an in-memory dictionary is fast but volatile. In production this would be backed by a database with the same key (ProductId) indexed, so the in-memory structure acts as a cache rather than the source of truth.

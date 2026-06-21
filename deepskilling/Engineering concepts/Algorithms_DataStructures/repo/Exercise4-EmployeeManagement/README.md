# Exercise 4: Employee Management System

## Scenario
An employee management system needs to store employee records and support add, search, traverse, and delete.

## How arrays are represented in memory
An array is a block of memory allocated as one contiguous region, where each element occupies a fixed-size slot and is accessed by `base_address + (index * element_size)`. This contiguity is what makes index-based access O(1) — the address of any element can be computed directly with no traversal needed. It's also what makes arrays cache-friendly: because elements sit next to each other in memory, reading one element tends to pull its neighbors into the CPU cache too, making sequential traversal fast in practice, not just in theory.

The trade-off is that contiguity must be reserved up front: an array's size is fixed at creation, so growing it means allocating a new, larger block and copying every existing element over (an O(n) operation) — there is no way to simply "extend" the original block if the next memory address is already in use by something else.

## Advantages of arrays
- O(1) random access by index.
- Contiguous memory layout gives excellent cache locality for sequential access.
- Simple, low memory overhead — no extra pointers or metadata per element (unlike a linked list).

## Implementation
This project intentionally uses a raw `Employee?[]` array with a fixed capacity (rather than `List<T>`), so the array's real characteristics are visible:
- `Employee.cs` — `EmployeeId`, `Name`, `Position`, `Salary`.
- `EmployeeManager.cs` — `Add` (fails once capacity is reached), `Search` (linear scan by ID), `Traverse` (visits every record in order), `Delete` (finds the record, then shifts everything after it left by one to close the gap).
- `Program.cs` — adds employees up to capacity, searches, deletes from the middle (showing the shift), fills the array again, then attempts one more add to demonstrate the capacity limit.

Run it:
```bash
dotnet run --project Exercise4-EmployeeManagement
```

## Time complexity analysis

| Operation | Complexity | Why |
|---|---|---|
| Add (append) | O(1) amortized, fails at capacity | Direct write to the next free slot — but only while space remains; this implementation does not auto-grow. |
| Search by ID | O(n) | No index by ID exists; every element must be checked until a match is found. |
| Traverse | O(n) | Every element must be visited once; cheap per-element due to contiguous memory. |
| Delete | O(n) | O(n) to find the element, plus up to O(n) to shift subsequent elements left and close the gap. |

## Limitations of arrays, and when to use them
The demo above shows both core limitations directly: deleting `E402` from the middle requires shifting every following element, and once the array fills to capacity, further adds simply fail (a real dynamic array implementation, like `List<T>` or Java's `ArrayList`, would instead allocate a new, larger backing array and copy everything over — itself an O(n) operation, just hidden behind a convenient API).

Arrays are the right choice when the dataset size is known or bounded in advance, reads/lookups-by-index dominate over inserts and deletes, and memory efficiency or cache performance matters. They're a poor choice when the system needs frequent inserts/deletes in the middle of the collection, or when the size is highly unpredictable — for the employee system specifically, if hiring and attrition are frequent, a structure like a linked list (Exercise 5) or a hash map keyed by `EmployeeId` (as used for the inventory in Exercise 1) would avoid the shifting cost and the fixed-capacity ceiling shown above.

# Exercise 5: Task Management System

## Scenario
A task management system needs tasks to be added, deleted, and traversed efficiently, with the task list growing and shrinking unpredictably as work is created and completed.

## Types of linked lists
- **Singly Linked List** — each node holds a value and a single pointer to the next node. Traversal is one-directional (head → tail). Lower memory overhead per node (one pointer) and simpler to implement, but you can't walk backward, and deleting a node requires a reference to the *previous* node (found by traversing from the head) to relink around it.
- **Doubly Linked List** — each node holds pointers to both the next and the previous node. This allows traversal in both directions and lets a node be deleted in O(1) once you already hold a reference to it (no need to find the previous node by scanning), at the cost of an extra pointer per node and slightly more bookkeeping to keep both links consistent. .NET's `LinkedList<T>` is a doubly linked list.
- (Not used here, but worth knowing) **Circular Linked List** — the last node points back to the first, useful for round-robin scheduling type problems.

This project implements a **singly linked list** by hand (`TaskNode` + `TaskLinkedList`), since the scenario only requires forward traversal, add, search, and delete.

## Implementation
- `Task.cs` — `TaskId`, `TaskName`, `Status`.
- `TaskNode.cs` — holds a `Task` and a `Next` reference.
- `TaskLinkedList.cs` — `Add` (appends at the tail, O(1) via a tracked tail pointer), `Search` (O(n) traversal from the head), `Traverse` (visits every node), `Delete` (finds the node and its predecessor, then relinks around it — no shifting of other elements).
- `Program.cs` — adds 4 tasks, searches for one, deletes one from the middle (showing that no shifting occurs, unlike the array in Exercise 4), then adds two more to show the list growing with no fixed-capacity ceiling.

Run it:
```bash
dotnet run --project Exercise5-TaskManagement
```

## Time complexity analysis

| Operation | Complexity | Why |
|---|---|---|
| Add (append) | O(1) | A tail pointer is kept, so the new node attaches directly without traversal. |
| Search by ID | O(n) | No random access — finding any node means walking `Next` pointers from the head. |
| Traverse | O(n) | Every node must be visited once. |
| Delete | O(n) | O(n) to locate the node and its predecessor by traversal; relinking itself is O(1) once both are found. |

## Linked lists vs. arrays for dynamic data
The array implementation in Exercise 4 had two limitations this linked list avoids entirely: deleting `T502` here required no shifting at all (just two pointer updates — `previous.Next = current.Next`), and the list has no fixed capacity to run out of, since each `Add` allocates exactly one new node rather than relying on a pre-sized backing block. This makes linked lists a natural fit when the collection size is unpredictable and insertions/deletions happen frequently, as is the case for an evolving task list.

The trade-off is that linked lists give up O(1) random access by index (getting the "5th task" means walking from the head) and lose array's cache-friendly contiguous memory layout, since nodes can be scattered anywhere in memory and each one requires following a pointer. They also use more memory per element (an extra pointer field per node). In short: choose arrays when you need fast random access and your data size is known or stable; choose linked lists when inserts/deletes dominate and the size is unpredictable, exactly as in this task manager.

# Algorithms & Data Structures — Exercise Portfolio

Seven scenario-based exercises covering core data structures and algorithms, implemented in **C# (.NET 8)**. Each exercise is a standalone, runnable console project paired with a `README.md` that works through the conceptual questions (why this data structure, complexity analysis, trade-offs) alongside the code.

## Exercises

| # | Exercise | Core topic | Folder |
|---|---|---|---|
| 1 | Inventory Management System | Hash maps (`Dictionary`) for O(1) add/update/delete | [`Exercise1-InventoryManagement`](Exercise1-InventoryManagement) |
| 2 | E-commerce Platform Search | Linear search vs. binary search, Big O | [`Exercise2-EcommerceSearch`](Exercise2-EcommerceSearch) |
| 3 | Sorting Customer Orders | Bubble Sort vs. Quick Sort | [`Exercise3-SortingOrders`](Exercise3-SortingOrders) |
| 4 | Employee Management System | Array representation and its limitations | [`Exercise4-EmployeeManagement`](Exercise4-EmployeeManagement) |
| 5 | Task Management System | Singly linked lists | [`Exercise5-TaskManagement`](Exercise5-TaskManagement) |
| 6 | Library Management System | Linear vs. binary search (recursive variant) | [`Exercise6-LibraryManagement`](Exercise6-LibraryManagement) |
| 7 | Financial Forecasting | Recursion, exponential blowup, and memoization | [`Exercise7-FinancialForecasting`](Exercise7-FinancialForecasting) |

Each exercise's `README.md` covers: the scenario, the conceptual explanation requested in the brief, the data structure/algorithm choice and why, a time-complexity table, and an optimization/trade-off discussion.

## Requirements
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Running an exercise
Each exercise is an independent console app. From the repo root:

```bash
dotnet run --project Exercise1-InventoryManagement
dotnet run --project Exercise2-EcommerceSearch
dotnet run --project Exercise3-SortingOrders
dotnet run --project Exercise4-EmployeeManagement
dotnet run --project Exercise5-TaskManagement
dotnet run --project Exercise6-LibraryManagement
dotnet run --project Exercise7-FinancialForecasting
```

Or open `AlgorithmsDataStructures.sln` in Visual Studio / Rider / VS Code and run any project directly.

To build everything at once:
```bash
dotnet build
```

## Structure
```
AlgorithmsDataStructures.sln
Exercise1-InventoryManagement/
  Product.cs
  InventoryManager.cs
  Program.cs
  README.md
Exercise2-EcommerceSearch/
  ...
...
```

Each `Program.cs` is a self-contained demo: it builds sample data, runs the operations/algorithms being demonstrated, and prints the results (and, where relevant, comparison/call counts) to the console — no external setup or input required.

# Exercise 7: Financial Forecasting

## Scenario
A forecasting tool needs to predict future values from past data — both a simple compounding projection and a trend-based projection that accounts for momentum from recent periods.

## Recursion
Recursion is a technique where a function solves a problem by calling itself on a smaller version of the same problem, until it reaches a base case simple enough to answer directly. It simplifies problems that have a natural self-similar structure — compounding growth, tree/graph traversal, divide-and-conquer algorithms (like Quick Sort in Exercise 3) — by letting the code mirror the mathematical recurrence directly instead of manually managing loops and state. The cost is that recursion uses call-stack space proportional to its depth, and naive recursive formulations can recompute the same sub-problem many times over if subproblems overlap, as shown below.

## Implementation
`FinancialForecaster.cs` implements two related but distinct recursive forecasts:

1. **`SimpleFutureValue`** — straightforward compound-growth recursion: `FV(0) = presentValue`, `FV(n) = FV(n-1) * (1 + growthRate)`. One recursive call per period.
2. **`NaiveTrendForecast`** — a trend/momentum model where each period depends on the *previous two* periods: `FV(n) = FV(n-1) + growthRate * FV(n-2)`. This is structurally identical to the Fibonacci recurrence, and it inherits Fibonacci's classic performance problem: evaluating `FV(n-1)` independently re-derives `FV(n-2)`, `FV(n-3)`, etc., that the `FV(n-2)` branch will *also* compute from scratch.
3. **`MemoizedTrendForecast`** — the exact same recurrence, but each `periodsAhead` value is cached in a `Dictionary<int, decimal>` after it's first computed, so repeat sub-calls become O(1) lookups instead of full re-computation.

`Program.cs` runs all three and reports the recursive call count and wall-clock time for the naive vs. memoized trend forecast, 30 periods ahead.

Run it:
```bash
dotnet run --project Exercise7-FinancialForecasting
```

Sample output:
```
Naive recursive result:    $41,686.98  | calls:    2,692,537 | time: 75.58 ms
Memoized recursive result: $41,686.98  | calls:           59 | time:  4.68 ms
Call reduction: 2,692,537 -> 59 (100.0% fewer calls)
```

## Time complexity of the recursive algorithms
- **`SimpleFutureValue`**: each call makes exactly one further recursive call, so it's O(n) time and O(n) stack depth — there's no redundant work to optimize away.
- **`NaiveTrendForecast`**: each call (except the base cases) makes two further recursive calls, and the same `periodsAhead` value gets re-derived repeatedly across different call paths. This gives the same growth rate as naive recursive Fibonacci: O(φⁿ) where φ ≈ 1.618 (the golden ratio) — exponential time, even though the underlying problem only has n distinct sub-problems. The 30-period forecast above makes nearly 2.7 million calls to solve what is really only 31 distinct values.
- **`MemoizedTrendForecast`**: with caching, each of the n distinct `periodsAhead` values is computed exactly once (and its two recursive dependencies are O(1) cache hits thereafter), bringing total time down to O(n) — confirmed by the 59 calls in the sample run (roughly 2× the 30 periods, since each is checked once on the way down and once via cache on the way back up).

## Optimizing the recursive solution
The technique used here — **memoization** — is the standard fix whenever a recursive recurrence has *overlapping subproblems* (the same sub-call appears more than once across different branches of the recursion tree) and *no dependency on path/history beyond the state being cached* (here, the result only depends on `periodsAhead`, not on how you got there). Two equivalent ways to apply it:
- **Top-down memoization** (used above): keep the recursive structure, but check a cache before recomputing, and store the result before returning.
- **Bottom-up dynamic programming**: rewrite the same recurrence as an iterative loop that fills an array `FV[0..n]` from the base cases upward, avoiding recursion (and its stack-depth cost) entirely while keeping the same O(n) time.

A useful rule of thumb when looking at any new recursive formula: if drawing out the recursion tree shows the same `(state)` appearing in more than one branch, it's a candidate for memoization; if every call's state is unique (as in `SimpleFutureValue`), there's nothing to cache.

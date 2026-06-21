namespace FinancialForecasting;

/// <summary>
/// Recursive algorithms for forecasting future financial values.
/// </summary>
public class FinancialForecaster
{
    /// <summary>Number of recursive calls made by the most recent forecast call. Reset via ResetCallCount().</summary>
    public long CallCount { get; private set; }

    public void ResetCallCount() => CallCount = 0;

    /// <summary>
    /// Simple recursive future value: compounds a single growth rate forward
    /// `periods` times. FV(0) = presentValue; FV(n) = FV(n-1) * (1 + growthRate).
    /// One recursive call per period, so time and stack depth are both O(n).
    /// </summary>
    public decimal SimpleFutureValue(decimal presentValue, double growthRate, int periods)
    {
        CallCount++;
        if (periods == 0) return presentValue;
        return SimpleFutureValue(presentValue, growthRate, periods - 1) * (decimal)(1 + growthRate);
    }

    /// <summary>
    /// Trend-based forecast: predicts period n from BOTH the level of the prior
    /// period and the momentum carried from two periods back —
    ///   FV(n) = FV(n-1) + growthRate * FV(n-2)
    /// This is the same recurrence shape as Fibonacci. Each call branches into
    /// two recursive calls, and those calls overlap heavily (FV(n-2) gets
    /// recomputed from scratch as part of evaluating FV(n-1) too), so this
    /// naive version costs O(phi^n) time — exponential.
    /// </summary>
    public decimal NaiveTrendForecast(decimal baseValue0, decimal baseValue1, double growthRate, int periodsAhead)
    {
        CallCount++;
        if (periodsAhead == 0) return baseValue0;
        if (periodsAhead == 1) return baseValue1;

        return NaiveTrendForecast(baseValue0, baseValue1, growthRate, periodsAhead - 1)
             + (decimal)growthRate * NaiveTrendForecast(baseValue0, baseValue1, growthRate, periodsAhead - 2);
    }

    /// <summary>
    /// The exact same recurrence as NaiveTrendForecast, optimized with
    /// memoization: every distinct `periodsAhead` value is solved once and
    /// cached, so subsequent requests for that same period are O(1) lookups
    /// instead of full re-computation. This brings the cost down to O(n).
    /// </summary>
    public decimal MemoizedTrendForecast(decimal baseValue0, decimal baseValue1, double growthRate, int periodsAhead)
    {
        var cache = new Dictionary<int, decimal>();
        return MemoizedTrendForecastHelper(baseValue0, baseValue1, growthRate, periodsAhead, cache);
    }

    private decimal MemoizedTrendForecastHelper(
        decimal baseValue0, decimal baseValue1, double growthRate, int periodsAhead, Dictionary<int, decimal> cache)
    {
        CallCount++;
        if (periodsAhead == 0) return baseValue0;
        if (periodsAhead == 1) return baseValue1;
        if (cache.TryGetValue(periodsAhead, out var cached)) return cached;

        var result = MemoizedTrendForecastHelper(baseValue0, baseValue1, growthRate, periodsAhead - 1, cache)
                   + (decimal)growthRate * MemoizedTrendForecastHelper(baseValue0, baseValue1, growthRate, periodsAhead - 2, cache);

        cache[periodsAhead] = result;
        return result;
    }
}

using System.Diagnostics;

namespace FinancialForecasting;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Exercise 7: Financial Forecasting ===\n");

        var forecaster = new FinancialForecaster();

        // --- Simple recursive future value (single fixed growth rate) ---
        decimal presentValue = 100_000m;
        double growthRate = 0.08; // 8% per period
        int periods = 10;

        decimal futureValue = forecaster.SimpleFutureValue(presentValue, growthRate, periods);
        Console.WriteLine($"Simple future value: ${presentValue:N2} compounded at {growthRate:P0} for {periods} periods");
        Console.WriteLine($"  -> ${futureValue:N2}");
        Console.WriteLine($"  Recursive calls made: {forecaster.CallCount}\n");

        // --- Trend-based forecast: naive (exponential) vs memoized (linear) ---
        decimal base0 = 10_000m; // value 2 periods ago
        decimal base1 = 10_800m; // value 1 period ago
        double momentumRate = 0.05;
        int periodsAhead = 30;

        forecaster.ResetCallCount();
        var stopwatchNaive = Stopwatch.StartNew();
        decimal naiveResult = forecaster.NaiveTrendForecast(base0, base1, momentumRate, periodsAhead);
        stopwatchNaive.Stop();
        long naiveCalls = forecaster.CallCount;

        forecaster.ResetCallCount();
        var stopwatchMemo = Stopwatch.StartNew();
        decimal memoResult = forecaster.MemoizedTrendForecast(base0, base1, momentumRate, periodsAhead);
        stopwatchMemo.Stop();
        long memoCalls = forecaster.CallCount;

        Console.WriteLine($"Trend-based forecast {periodsAhead} periods ahead (FV(n) = FV(n-1) + rate * FV(n-2)):");
        Console.WriteLine($"  Naive recursive result:    ${naiveResult:N2}  | calls: {naiveCalls,12:N0} | time: {stopwatchNaive.Elapsed.TotalMilliseconds:N2} ms");
        Console.WriteLine($"  Memoized recursive result: ${memoResult:N2}  | calls: {memoCalls,12:N0} | time: {stopwatchMemo.Elapsed.TotalMilliseconds:N2} ms");
        Console.WriteLine($"\n  Both results match: {naiveResult == memoResult}");
        Console.WriteLine($"  Call reduction: {naiveCalls:N0} -> {memoCalls:N0} ({(1 - (double)memoCalls / naiveCalls):P1} fewer calls)");
    }
}

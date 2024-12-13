using System.Diagnostics;
using AdventOfCode.Core.Input;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers;

namespace AdventOfCode.ConsoleApp;

internal static class Benchmarker
{
    internal static void Run(int year)
    {
        Console.WriteLine($"Running benchmarks for year {year}...");

        var solvers = typeof(IPuzzleSolver).Assembly.GetTypes()
            .Where(t => t.Namespace == $"AdventOfCode.Solvers._{year}" && typeof(IPuzzleSolver).IsAssignableFrom(t))
            .Select(t => (IPuzzleSolver?)Activator.CreateInstance(t)) ?? [];

        if (!solvers.Any())
        {
            ConsoleWriter.Error($"No solvers found for {year}");
            return;
        }

        ConsoleWriter.Write("|-----|------|---------|-------------|-------------|-------------|");
        ConsoleWriter.Write("| Day | Part | Samples | Mean time   | Min time    | Max time    |");
        ConsoleWriter.Write("|-----|------|---------|-------------|-------------|-------------|");

        foreach (var solver in solvers)
        {
            if (solver is null)
                continue;

            var dayNum = int.Parse(Regexes.Integer().Match(solver.GetType().Name).Value);
            var input = DataReader.GetData(year, dayNum, 0, false);

            // Maybe run in a background worker
            WritePartLine(dayNum, 1, AggregateSolves(() => solver.SolvePartOne(input)));
            WritePartLine(dayNum, 2, AggregateSolves(() => solver.SolvePartTwo(input)));

            ConsoleWriter.Write("|-----|------|---------|-------------|-------------|-------------|");
        }
    }

    private static Aggregate AggregateSolves(Func<string> action)
    {
        var results = new List<TimeSpan>();

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while (results.Count < 20 && stopwatch.Elapsed.TotalSeconds < 1)
        {
            results.Add(SolvePart(action));
        }
        stopwatch.Stop();

        return new Aggregate(
            results.Count,
            new TimeSpan((long)results.Average(r => r.Ticks)),
            results.Min(),
            results.Max());
    }

    private static TimeSpan SolvePart(Func<string> action)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private static void WritePartLine(int day, int part, Aggregate aggregate)
    {
        var dayStr = day.ToString().PadLeft(2, ' ');
        var samplesStr = aggregate.Count.ToString().PadLeft(7, ' ');
        Console.Write($"|  {dayStr} |    {part} | {samplesStr} | ");
        WriteTime(aggregate.Mean);
        Console.Write(" | ");
        WriteTime(aggregate.Min);
        Console.Write(" | ");
        WriteTime(aggregate.Max);
        Console.WriteLine(" |");
    }

    private static void WriteTime(TimeSpan duration)
    {
        Console.ForegroundColor = TimeColor(duration);
        Console.Write(ElapsedTimeString(duration).PadLeft(11, ' '));
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    private static string ElapsedTimeString(TimeSpan duration)
    {
        if (duration.TotalMilliseconds < 1)
            return $"{duration.TotalMicroseconds:N1} μs";

        if (duration.TotalSeconds < 1)
            return $"{duration.TotalMilliseconds:N1} ms";

        return $"{duration.TotalSeconds:N1} s ";
    }

    private static ConsoleColor TimeColor(TimeSpan duration)
    {
        if (duration.TotalSeconds > 10)
            return ConsoleColor.Red;

        if (duration.TotalSeconds > 1)
            return ConsoleColor.DarkRed;

        if (duration.TotalMilliseconds > 500)
            return ConsoleColor.Yellow;

        if (duration.TotalMilliseconds > 1)
            return ConsoleColor.Green;

        return ConsoleColor.Cyan;
    }

    private record Aggregate(int Count, TimeSpan Mean, TimeSpan Min, TimeSpan Max);
}

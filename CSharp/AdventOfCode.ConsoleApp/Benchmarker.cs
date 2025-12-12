using System.Diagnostics;
using AdventOfCode.Core.Input;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Solvers;

namespace AdventOfCode.ConsoleApp;

internal static class Benchmarker
{
    internal static async Task Run(int year, int? day, string session)
    {
        Console.WriteLine($"Running benchmarks for year {year}...");

#if DEBUG
        ConsoleWriter.Error($"WARNING: Running benchmarks in debug mode.");
#endif

        var solvers = typeof(PuzzleSolver).Assembly.GetTypes()
            .Where(t => t.Namespace == $"AdventOfCode.Solvers._{year}" && typeof(PuzzleSolver).IsAssignableFrom(t))
            .Where(t => !day.HasValue || t.Name.Contains(day.Value.ToString("00")))
            .Select(t => (PuzzleSolver?)Activator.CreateInstance(t)) ?? [];

        if (!solvers.Any())
        {
            ConsoleWriter.Error($"No solvers found for {year}");
            return;
        }

        ConsoleWriter.Write("┌─────┬──────┬────┬─────────────┬─────────────┬─────────────┬─────────────┐");
        ConsoleWriter.Write("│ Day │ Part │  n │ Mean time   │ Mode time   │ Min time    │ Max time    │");

        var aggregates = new List<Aggregate>();

        foreach (var solver in solvers)
        {
            if (solver is null)
                continue;

            var dayNum = int.Parse(Regexes.NonNegativeInteger().Match(solver.GetType().Name).Value);

            string input;
            try
            {
                input = DataReader.GetData(year, dayNum, 0, false);
            }
            catch (FileNotFoundException)
            {
                try
                {
                    await Downloader.DownloadInput(year, dayNum, session, isVerbose: false);
                    input = DataReader.GetData(year, dayNum, 0, false);
                }
                catch
                {
                    ConsoleWriter.Write("├─────┼──────┼────┼─────────────┼─────────────┼─────────────┼─────────────┤");
                    ConsoleWriter.Write($"│  {dayNum,2} │ Could not run: no input file found.                               │");
                    continue;
                }
            }

            ConsoleWriter.Write("├─────┼──────┼────┼─────────────┼─────────────┼─────────────┼─────────────┤");

            var partOne = AggregateSolves(dayNum, 1, () => solver.SolvePartOne(input));
            aggregates.AddRange(partOne);
            WritePartLine(dayNum, 1, partOne);

            var hasPartTwo =
                (year < 2025 && dayNum < 25) ||
                dayNum < 12;
            if (hasPartTwo)
            {
                var partTwo = AggregateSolves(dayNum, 2, () => solver.SolvePartTwo(input));
                aggregates.AddRange(partTwo);
                WritePartLine(dayNum, 2, partTwo);
            }
        }
        ConsoleWriter.Write("└─────┴──────┴────┴─────────────┴─────────────┴─────────────┴─────────────┘");

        aggregates = [.. aggregates.Where(a => a.Count > 0)];

        Console.WriteLine();
        ConsoleWriter.Write("┌─────────────────┬─────────────┬─────────────┬─────────────┬─────────────┐");
        ConsoleWriter.Write("│  ** OVERALL **  │ Mean time   │ Mode time   │ Min time    │ Max time    │");
        ConsoleWriter.Write("├─────────────────┼─────────────┼─────────────┼─────────────┼─────────────┤");

        WriteAggregateMean(aggregates, "Part 1 average ", a => a.Part == 1);
        WriteAggregateMean(aggregates, "Part 2 average ", a => a.Part == 2);
        WriteAggregateMean(aggregates, "Overall average", a => true);

        ConsoleWriter.Write("├─────────────────┼─────────────┼─────────────┼─────────────┼─────────────┤");

        WriteAggregateTotal(aggregates, "Part 1 total   ", a => a.Part == 1);
        WriteAggregateTotal(aggregates, "Part 2 total   ", a => a.Part == 2);
        WriteAggregateTotal(aggregates, "Overall total  ", a => true);

        ConsoleWriter.Write("└─────────────────┴─────────────┴─────────────┴─────────────┴─────────────┘");
    }

    private static void WriteAggregateMean(List<Aggregate> aggregates, string header, Func<Aggregate, bool> matcher)
    {
        var filtered = aggregates.Where(matcher);
        if (!filtered.Any())
            return;

        var averageMean = new TimeSpan((long)filtered.Average(a => a.Mean.Ticks));
        var averageMode = new TimeSpan((long)filtered.Average(a => a.Mode.Ticks));
        var averageMin = new TimeSpan((long)filtered.Average(a => a.Min.Ticks));
        var averageMax = new TimeSpan((long)filtered.Average(a => a.Max.Ticks));

        Console.Write($"│ {header} │ ");
        ConsoleWriter.WriteTime(averageMean);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(averageMode);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(averageMin);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(averageMax);
        Console.WriteLine(" │");
    }

    private static void WriteAggregateTotal(List<Aggregate> aggregates, string header, Func<Aggregate, bool> matcher)
    {
        var part1TotalMean = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Mean.Ticks));
        var part1TotalMode = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Mode.Ticks));
        var part1TotalMin = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Min.Ticks));
        var part1TotalMax = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Max.Ticks));

        Console.Write($"│ {header} │ ");
        ConsoleWriter.WriteTime(part1TotalMean, false);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(part1TotalMode, false);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(part1TotalMin, false);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(part1TotalMax, false);
        Console.WriteLine(" │");
    }

    private static Aggregate AggregateSolves(int day, int part, Func<object> action)
    {
        var results = new List<TimeSpan>();

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while (results.Count < 2 || (results.Count < 50 && stopwatch.Elapsed.TotalSeconds < 5))
        {
            try
            {
                results.Add(SolvePart(action));
            }
            catch (NotImplementedException)
            {
                return new Aggregate(day, part, 0, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero, TimeSpan.Zero);
            }
        }
        stopwatch.Stop();

        return new Aggregate(
            day, part,
            results.Count,
            new TimeSpan((long)results.Average(r => r.Ticks)),
            results[results.Count / 2],
            results.Min(),
            results.Max());
    }

    private static TimeSpan SolvePart(Func<object> action)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private static void WritePartLine(int day, int part, Aggregate aggregate)
    {
        if (aggregate.Count == 0)
        {
            ConsoleWriter.Write($"│  {day,2} │ Could not run: solver not implemented.                            │");
            return;
        }

        Console.Write($"│  {day,2} │    {part} │ {aggregate.Count,2} │ ");
        ConsoleWriter.WriteTime(aggregate.Mean);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(aggregate.Mode);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(aggregate.Min);
        Console.Write(" │ ");
        ConsoleWriter.WriteTime(aggregate.Max);
        Console.WriteLine(" │");
    }

    private record Aggregate(int Day, int Part, int Count, TimeSpan Mean, TimeSpan Mode, TimeSpan Min, TimeSpan Max);
}

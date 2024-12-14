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

        ConsoleWriter.Write("┌─────┬──────┬─────────────┬─────────────┬─────────────┬─────────────┐");
        ConsoleWriter.Write("│ Day │ Part │ Mean time   │ Mode time   │ Min time    │ Max time    │");

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
                ConsoleWriter.Write("├─────┼──────┼─────────────┼─────────────┼─────────────┼─────────────┤");
                ConsoleWriter.Write($"│  {dayNum,2} │ Could not run: no input file found.                          │");
                continue;
            }

            ConsoleWriter.Write("├─────┼──────┼─────────────┼─────────────┼─────────────┼─────────────┤");
            var partOne = AggregateSolves(dayNum, 1, () => solver.SolvePartOne(input));
            var partTwo = AggregateSolves(dayNum, 2, () => solver.SolvePartTwo(input));
            aggregates.AddRange(partOne);
            aggregates.AddRange(partTwo);
            WritePartLine(dayNum, 1, partOne);
            WritePartLine(dayNum, 2, partTwo);
        }
        ConsoleWriter.Write("└─────┴──────┴─────────────┴─────────────┴─────────────┴─────────────┘");

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
        var part1AverageMean = new TimeSpan((long)aggregates.Where(matcher).Average(a => a.Mean.Ticks));
        var part1AverageMode = new TimeSpan((long)aggregates.Where(matcher).Average(a => a.Mode.Ticks));
        var part1AverageMin = new TimeSpan((long)aggregates.Where(matcher).Average(a => a.Min.Ticks));
        var part1AverageMax = new TimeSpan((long)aggregates.Where(matcher).Average(a => a.Max.Ticks));

        Console.Write($"│ {header} │ ");
        WriteTime(part1AverageMean);
        Console.Write(" │ ");
        WriteTime(part1AverageMode);
        Console.Write(" │ ");
        WriteTime(part1AverageMin);
        Console.Write(" │ ");
        WriteTime(part1AverageMax);
        Console.WriteLine(" │");
    }

    private static void WriteAggregateTotal(List<Aggregate> aggregates, string header, Func<Aggregate, bool> matcher)
    {
        var part1TotalMean = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Mean.Ticks));
        var part1TotalMode = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Mode.Ticks));
        var part1TotalMin = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Min.Ticks));
        var part1TotalMax = new TimeSpan(aggregates.Where(matcher).Sum(a => a.Max.Ticks));

        Console.Write($"│ {header} │ ");
        WriteTime(part1TotalMean, false);
        Console.Write(" │ ");
        WriteTime(part1TotalMode, false);
        Console.Write(" │ ");
        WriteTime(part1TotalMin, false);
        Console.Write(" │ ");
        WriteTime(part1TotalMax, false);
        Console.WriteLine(" │");
    }

    private static Aggregate AggregateSolves(int day, int part, Func<string> action)
    {
        var results = new List<TimeSpan>();

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        while (results.Count < 2 || (results.Count < 20 && stopwatch.Elapsed.TotalSeconds < 1))
        {
            try
            {
                results.Add(SolvePart(action));
            }
            catch (NotImplementedException)
            {
                return new Aggregate(day, part, 0, TimeSpan.MinValue, TimeSpan.MinValue, TimeSpan.MinValue, TimeSpan.MinValue);
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
        if (aggregate.Count == 0)
        {
            ConsoleWriter.Write("├─────┼──────┼─────────────┼─────────────┼─────────────┼─────────────┤");
            ConsoleWriter.Write($"│  {day,2} │ Could not run: solver not implemented.                       │");
            return;
        }

        Console.Write($"│  {day,2} │    {part} │ ");
        WriteTime(aggregate.Mean);
        Console.Write(" │ ");
        WriteTime(aggregate.Mode);
        Console.Write(" │ ");
        WriteTime(aggregate.Min);
        Console.Write(" │ ");
        WriteTime(aggregate.Max);
        Console.WriteLine(" │");
    }

    private static void WriteTime(TimeSpan duration, bool useColor = true)
    {
        if (useColor)
        {
            Console.ForegroundColor = TimeColor(duration);
        }

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
            return ConsoleColor.DarkRed;

        if (duration.TotalSeconds > 5)
            return ConsoleColor.Red;

        if (duration.TotalSeconds > 1)
            return ConsoleColor.DarkYellow;

        if (duration.TotalMilliseconds > 500)
            return ConsoleColor.Yellow;

        if (duration.TotalMilliseconds > 250)
            return ConsoleColor.Green;

        if (duration.TotalMilliseconds > 1)
            return ConsoleColor.DarkGreen;

        if (duration.TotalMicroseconds > 500)
            return ConsoleColor.Blue;

        return ConsoleColor.DarkBlue;
    }

    private record Aggregate(int Day, int Part, int Count, TimeSpan Mean, TimeSpan Mode, TimeSpan Min, TimeSpan Max);
}

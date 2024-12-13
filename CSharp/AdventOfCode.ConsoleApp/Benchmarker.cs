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

        ConsoleWriter.Write("|-----|------|-------------|");
        ConsoleWriter.Write("| Day | Part | Time        |");
        ConsoleWriter.Write("|-----|------|-------------|");

        foreach (var solver in solvers)
        {
            if (solver is null)
                continue;

            var dayNum = int.Parse(Regexes.Integer().Match(solver.GetType().Name).Value);
            var input = DataReader.GetData(year, dayNum, 0, false);

            // Maybe run in a background worker
            WritePartLine(dayNum, 1, SolvePart(() => solver.SolvePartOne(input)));
            WritePartLine(dayNum, 2, SolvePart(() => solver.SolvePartTwo(input)));

            ConsoleWriter.Write("|-----|------|-------------|");
        }
    }

    private static Stopwatch SolvePart(Func<string> action)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        action();
        stopwatch.Stop();
        return stopwatch;
    }

    private static void WritePartLine(int day, int part, Stopwatch stopwatch)
    {
        var duration = new TimeSpan(stopwatch.ElapsedTicks);
        var dayStr = day.ToString().PadLeft(2, ' ');
        Console.Write($"|  {dayStr} |    {part} | ");
        Console.ForegroundColor = TimeColor(duration);
        Console.Write(ElapsedTimeString(duration).PadLeft(11, ' '));
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(" |");
    }

    private static string ElapsedTimeString(TimeSpan duration)
    {
        if (duration.TotalMilliseconds < 1)
            return $"{duration.TotalMicroseconds:N4} μs";

        if (duration.TotalSeconds < 1)
            return $"{duration.TotalMilliseconds:N4} ms";

        return $"{duration.TotalSeconds:N4} s ";
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
}

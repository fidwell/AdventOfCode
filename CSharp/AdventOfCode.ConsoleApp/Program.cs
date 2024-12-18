using AdventOfCode.ConsoleApp;
using AdventOfCode.Core.Input;
using AdventOfCode.Solvers;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleWriter.Write(" ** Advent of Code utility helper ** ", ConsoleColor.Yellow);
        await Run(args);

        Console.WriteLine($"{Environment.NewLine}Press any key to close this window . . .");
        Console.ReadKey();
    }

    private static async Task Run(string[] args)
    {
        var cliArgs = ParseArgs(args);

        if (!cliArgs.TryGetValue("action", out string? action))
        {
            ConsoleWriter.Error("No CLI action provided.");
            return;
        }

        cliArgs.TryGetValue("session", out string? session);

        int? year = null, day = null, part = null;
        if (cliArgs.TryGetValue("year", out string? yearStr))
        {
            year = TryParseNullable(yearStr);
        }
        if (cliArgs.TryGetValue("day", out string? dayStr))
        {
            day = TryParseNullable(dayStr);
        }
        if (cliArgs.TryGetValue("part", out string? partStr))
        {
            part = TryParseNullable(partStr);
        }

        switch (action)
        {
            case "download-today":
                await DownloadToday(session);
                break;
            case "download-day":
                ConsoleWriter.Info($"Downloading input for {year} day {day}...");
                await DownloadDay(session, year, day);
                return;
            case "download-year":
                await DownloadYear(session, year);
                break;
            case "benchmark":
            case "benchmarks":
                if (!year.HasValue)
                {
                    ConsoleWriter.Error("Year not provided.");
                    break;
                }

                Benchmarker.Run(year.Value);
                break;
            case "run":
            case "solve":
                if (!year.HasValue || !day.HasValue)
                {
                    ConsoleWriter.Error("Year and day not specified.");
                    return;
                }

                RunSolver(year.Value, day.Value, part, cliArgs.ContainsKey("example"), cliArgs.ContainsKey("verbose"));
                break;
            default:
                ConsoleWriter.Error("Invalid program argument.");
                break;
        }
    }

    private static Dictionary<string, string> ParseArgs(string[] args)
    {
        var cliArgs = new Dictionary<string, string>();
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("--") && args.Length >= i)
            {
                cliArgs.Add(args[i][2..], args[i + 1]);
                i++;
            }
            else if (args[i].StartsWith('-'))
            {
                cliArgs.Add(args[i][1..], string.Empty);
            }
        }

        return cliArgs;
    }

    private static void RunSolver(int year, int day, int? part, bool useExample, bool printOutput)
    {
        var types = typeof(PuzzleSolver).Assembly.GetTypes()
            .Where(t => t.FullName == $"AdventOfCode.Solvers._{year}.Puzzle{day.ToString().PadLeft(2, '0')}Solver");
        var solver = (PuzzleSolver?)Activator.CreateInstance(types.First());

        if (solver is null)
        {
            ConsoleWriter.Error($"Can't create a solver for {year} puzzle {day}.");
            return;
        }

        solver.ShouldPrint = printOutput;
        if (part.HasValue)
        {
            ConsoleWriter.Info($"Solving part {part}...");
            var input = DataReader.GetData(year, day, part.Value, useExample);
            var result = part.Value == 1
                ? solver.SolvePartOne(input)
                : solver.SolvePartTwo(input);
            ConsoleWriter.Answer(part.Value, result);
        }
        else
        {
            ConsoleWriter.Info($"Solving part 1...");
            var input1 = DataReader.GetData(year, day, 1, useExample);
            ConsoleWriter.Answer(1, solver.SolvePartOne(input1));
            ConsoleWriter.Info($"Solving part 2...");
            var input2 = DataReader.GetData(year, day, 2, useExample);
            ConsoleWriter.Answer(2, solver.SolvePartTwo(input2));
        }
    }

    private static async Task DownloadToday(string session)
    {
        ConsoleWriter.Info("Downloading today's input...");

        var today = DateTime.Now;
        if (today.Month != 12 || !IsValidDay(today.Day))
        {
            ConsoleWriter.Error("Advent of Code is not active today.");
            return;
        }

        await DownloadInner(session, today.Year, today.Day);
    }

    private static async Task DownloadDay(string session, int? year, int? day)
    {
        if (!year.HasValue || !IsValidYear(year.Value))
        {
            ConsoleWriter.Error($"Invalid year: {year}");
            return;
        }

        if (!day.HasValue || !IsValidDay(day.Value))
        {
            ConsoleWriter.Error($"Invalid day: {day}");
            return;
        }

        await DownloadInner(session, year.Value, day.Value);
    }

    private static async Task DownloadYear(string session, int? year)
    {
        ConsoleWriter.Info($"Downloading all missing inputs for {year}...");
        var maxDay = year == DateTime.Now.Year ? DateTime.Now.Day : 25;
        for (var day = 1; day <= maxDay; day++)
        {
            await DownloadDay(session, year, day);
        }
    }

    private static async Task DownloadInner(string session, int year, int day)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            ConsoleWriter.Error("No session value provided.");
            return;
        }

        if (Downloader.SetUpDirectories(year))
        {
            await Downloader.DownloadInput(year, day, session);
        }
    }

    private static int? TryParseNullable(string val) =>
        int.TryParse(val, out int outValue) ? outValue : null;

    private static bool IsValidYear(int year) => year >= 2015 && year <= DateTime.Now.Year;
    private static bool IsValidDay(int day) => day >= 1 && day <= 25;
}

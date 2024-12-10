using AdventOfCode.ConsoleApp;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ConsoleWriter.Write(" --- Advent of Code utility helper --- ", ConsoleColor.Yellow);

        var cliArgs = new Dictionary<string, string>();
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("--") && args.Length >= i)
            {
                cliArgs.Add(args[i][2..], args[i + 1]);
            }
        }

        if (!cliArgs.TryGetValue("action", out string? value))
        {
            ConsoleWriter.Error("No CLI action provided.");
            return;
        }

        cliArgs.TryGetValue("session", out string? session);

        switch (value)
        {
            case "download-today":
                await DownloadToday(session);
                break;
            case "download-day":
                cliArgs.TryGetValue("year", out string? yearStr);
                cliArgs.TryGetValue("day", out string? dayStr);

                if (int.TryParse(yearStr, out int year) &&
                    int.TryParse(dayStr, out int day))
                {
                    await DownloadDay(session, year, day);
                }
                else
                {
                    ConsoleWriter.Error($"Invalid year {yearStr} or day {dayStr}.");
                }
                return;
            case "download-year":
                if (!cliArgs.TryGetValue("year", out string? yearStr2))
                {
                    ConsoleWriter.Error("No year value provided.");
                    return;
                }

                if (int.TryParse(yearStr2, out int year2))
                {
                    await DownloadYear(session, year2);
                }
                else
                {
                    ConsoleWriter.Error($"Invalid year {yearStr2}.");
                }
                break;
            default:
                ConsoleWriter.Error("Invalid program argument.");
                break;
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

    private static async Task DownloadDay(string session, int year, int day)
    {
        if (!IsValidYear(year))
        {
            ConsoleWriter.Error($"Invalid year: {year}");
            return;
        }

        if (!IsValidDay(day))
        {
            ConsoleWriter.Error($"Invalid day: {day}");
            return;
        }

        ConsoleWriter.Info($"Downloading input for {year} day {day}...");
        await DownloadInner(session, year, day);
    }

    private static async Task DownloadYear(string session, int year)
    {
        ConsoleWriter.Info($"Downloading all missing inputs for {year}...");
        for (var day = 1; day <= 25; day++)
        {
            await DownloadDay(session, year, day);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }

    private static async Task DownloadInner(string session, int year, int day)
    {
        if (string.IsNullOrWhiteSpace(session))
        {
            ConsoleWriter.Error("No session value provided.");
            return;
        }

        Downloader.SetUpDirectories(year);
        await Downloader.DownloadInput(year, day, session);
    }

    private static bool IsValidYear(int year) => year >= 2015 && year <= DateTime.Now.Year;
    private static bool IsValidDay(int day) => day >= 1 && day <= 25;
}

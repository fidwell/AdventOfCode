﻿using System.Net;
using AdventOfCode.Core.Input;

namespace AdventOfCode.ConsoleApp;

internal static class Downloader
{
    internal static async Task DownloadInput(int year, int day, string cookie, bool isVerbose = true)
    {
        var inputDirectoryName = DataReader.GetInputDirectory();
        var filename = Path.Combine(inputDirectoryName, year.ToString(), $"puzzle{day.ToString().PadLeft(2, '0')}.txt");
        if (File.Exists(filename))
        {
            if (isVerbose)
            {
                ConsoleWriter.Error("File already exists.");
            }
            return;
        }

        // Prevent hammering the site
        Thread.Sleep(TimeSpan.FromSeconds(1));

        var uri = new Uri("https://adventofcode.com");
        var cookies = new CookieContainer();
        cookies.Add(uri, new Cookie("session", cookie));
        using var handler = new HttpClientHandler() { CookieContainer = cookies };
        using var client = new HttpClient(handler) { BaseAddress = uri };
        using var response = await client.GetAsync($"/{year}/day/{day}/input");

        if (response.IsSuccessStatusCode)
        {
            using var stream = await response.Content.ReadAsStreamAsync();
            using var file = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            await stream.CopyToAsync(file);
            if (isVerbose)
            {
                ConsoleWriter.Info("File successfully downloaded.");
            }
        }
        else if (isVerbose)
        {
            ConsoleWriter.Error($"Couldn't download file. {(int)response.StatusCode}: {response.ReasonPhrase}");
        }
    }

    internal static bool SetUpDirectories(int year, bool isVerbose = true)
    {
        var inputDirectoryName = DataReader.GetInputDirectory();
        if (!Directory.Exists(inputDirectoryName))
        {
            if (isVerbose)
            {
                ConsoleWriter.Error("Couldn't find root input directory.");
            }
            return false;
        }

        var thisYearDirectory = Path.Join(inputDirectoryName, year.ToString());
        if (!Directory.Exists(thisYearDirectory))
        {
            if (isVerbose)
            {
                ConsoleWriter.Info($"Creating new directory for {year}...");
            }
            Directory.CreateDirectory(thisYearDirectory);
        }
        return true;
    }
}

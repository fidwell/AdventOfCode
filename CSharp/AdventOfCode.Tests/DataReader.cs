using System.Reflection;

namespace AdventOfCode.Tests;

public static class DataReader
{
    public static string GetData(int puzzleId, int partId, bool useSample)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var partString = partId <= 0 ? string.Empty : $"_Part{partId}";
        var sampleString = useSample ? $"{partString}_Sample" : string.Empty;
        var resource = $"AdventOfCode.Tests.Inputs.Puzzle{puzzleId:00}{sampleString}.txt";

        using var stream = assembly.GetManifestResourceStream(resource);
        if (stream is null)
            return string.Empty;

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}

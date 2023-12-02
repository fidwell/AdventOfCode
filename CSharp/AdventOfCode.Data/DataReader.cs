using System.Reflection;

namespace AdventOfCode.Data;

public static class DataReader
{
    public static string GetData(int puzzleId, int partId, bool useSample = false)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var partString = partId <= 0 ? string.Empty : $"_Part{partId}";
        var sampleString = useSample ? $"{partString}_Sample" : string.Empty;
        var resource = $"AdventOfCode.Data.Inputs.Puzzle{puzzleId:00}{sampleString}.txt";

        using var stream = assembly.GetManifestResourceStream(resource);
        if (stream is null)
            return string.Empty;

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}

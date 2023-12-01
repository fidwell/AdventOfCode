using System.Reflection;

namespace AdventOfCode.Data;

public static class DataReader
{
    public static string GetData(int puzzleId, bool useSample = false)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var sampleString = useSample ? "Sample" : string.Empty;
        var resource = $"AdventOfCode.Data.Inputs.Puzzle{puzzleId:00}{sampleString}.txt";

        using var stream = assembly.GetManifestResourceStream(resource);
        if (stream is null)
            return string.Empty;

        using var reader = new StreamReader(stream);

        return string.Join(Environment.NewLine, reader.ReadToEnd());
    }
}

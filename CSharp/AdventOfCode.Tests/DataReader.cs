using System.Reflection;

namespace AdventOfCode.Tests;

public static class DataReader
{
    public static string GetData(int year, int puzzleId, int partId, bool useSample)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var sampleStringWithPart = useSample ? $"_Part{partId}_Sample" : string.Empty;
        var sampleStringWithputPart = useSample ? $"_Sample" : string.Empty;
        var pathWithSamplePart = $"AdventOfCode.Tests.Inputs._{year}.Puzzle{puzzleId:00}{sampleStringWithPart}.txt";
        var pathWithoutSamplePart = $"AdventOfCode.Tests.Inputs._{year}.Puzzle{puzzleId:00}{sampleStringWithputPart}.txt";

        using var stream = assembly.GetManifestResourceStream(pathWithoutSamplePart)
             ?? assembly.GetManifestResourceStream(pathWithSamplePart)
             ?? throw new FileNotFoundException();

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}

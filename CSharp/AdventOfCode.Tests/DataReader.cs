using System.Reflection;

namespace AdventOfCode.Tests;

public static class DataReader
{
    public static string GetData(int year, int puzzleId, int partId, bool useExample)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var exampleStringWithPart = useExample ? $"_part{partId}_example" : string.Empty;
        var exampleStringWithputPart = useExample ? $"_example" : string.Empty;
        var pathWithSamplePart = $"AdventOfCode.Tests.Inputs._{year}.puzzle{puzzleId:00}{exampleStringWithPart}.txt";
        var pathWithoutSamplePart = $"AdventOfCode.Tests.Inputs._{year}.puzzle{puzzleId:00}{exampleStringWithputPart}.txt";

        using var stream = assembly.GetManifestResourceStream(pathWithoutSamplePart)
             ?? assembly.GetManifestResourceStream(pathWithSamplePart)
             ?? throw new FileNotFoundException();

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}

namespace AdventOfCode.Core.Input;

public static class DataReader
{
    public static string GetData(int year, int puzzleId, int partId, bool useExample)
    {
        var exampleStringWithPart = useExample ? $"_part{partId}_example" : string.Empty;
        var exampleStringWithoutPart = useExample ? $"_example" : string.Empty;

        var fileWithSample = Path.Combine(GetInputDirectory(), year.ToString(), $"puzzle{puzzleId:00}{exampleStringWithPart}.txt");
        if (File.Exists(fileWithSample))
            return File.ReadAllText(fileWithSample);

        var fileWithoutSample = Path.Combine(GetInputDirectory(), year.ToString(), $"puzzle{puzzleId:00}{exampleStringWithoutPart}.txt");
        if (File.Exists(fileWithoutSample))
            return File.ReadAllText(fileWithoutSample);

        throw new FileNotFoundException();
    }

    public static string GetInputDirectory()
    {
        var directory = new DirectoryInfo(Environment.CurrentDirectory);
        do
        {
            directory = directory.Parent;

            if (directory is null)
                throw new Exception("Couldn't find the root directory");
        }
        while (directory.Name != "AdventOfCode");
        return Path.Combine(directory.FullName, "input");
    }
}

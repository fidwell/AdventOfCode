using System.Text.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle12Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        Number()
        .Matches(input)
        .Select(m => m.Captures[0])
        .Select(m => int.Parse(m.Value))
        .Sum().ToString();

    public override string SolvePartTwo(string input) =>
        SumObject(JsonDocument.Parse(input).RootElement).ToString();

    public static int SumObject(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var asObj = element.EnumerateObject();
                if (asObj.Any(x => IsRed(x.Value)))
                    return 0;
                return asObj.Sum(x => SumObject(x.Value));
            case JsonValueKind.Array:
                var asArray = element.EnumerateArray();
                return asArray.Sum(SumObject);
            case JsonValueKind.Number:
                return element.GetInt32();
            default:
                return 0;
        }
    }

    public static bool IsRed(JsonElement element) =>
        element.ValueKind == JsonValueKind.String && element.ValueEquals("red");

    [GeneratedRegex(@"(\-?\d+)")]
    private static partial Regex Number();
}

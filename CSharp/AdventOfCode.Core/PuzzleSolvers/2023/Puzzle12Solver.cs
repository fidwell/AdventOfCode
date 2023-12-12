using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle12Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) =>
        DataReader.GetData(12, 0, useSample)
        .Split(Environment.NewLine)
        .Select(PossibleArrangementCount)
        .Sum()
        .ToString();

    public string SolvePartTwo(bool useSample = false)
    {
        throw new NotImplementedException();
    }

    private int PossibleArrangementCount(string input)
    {
        var data = input.Split(" ");
        var asString = data[0];
        var groupCounts = data[1].Split(",").Select(int.Parse).ToArray();

        return FindPossibleArrangements(asString).Where(a => Verify(a, groupCounts)).Count();
    }

    private static IEnumerable<string> FindPossibleArrangements(string input)
    {
        // Probably inefficient recursion...
        if (!input.Contains('?'))
        {
            yield return input;
        }
        else
        {
            var firstQ = input.IndexOf('?');

            foreach (var result in FindPossibleArrangements($"{input.Substring(0, firstQ)}#{input.Substring(firstQ + 1)}"))
                yield return result;

            foreach (var result in FindPossibleArrangements($"{input.Substring(0, firstQ)}.{input.Substring(firstQ + 1)}"))
                yield return result;
        }
    }

    private static bool Verify(string input, int[] groupCounts) =>
        Enumerable.SequenceEqual(input.SplitAndTrim(".").Select(g => g.Length), groupCounts);
}

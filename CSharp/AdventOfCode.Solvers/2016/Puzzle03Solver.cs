using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle03Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
        => input
            .SplitByNewline()
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(AsIntArray)
            .Count(IsTriangle)
            .ToString();

    public override string SolvePartTwo(string input)
        => input
            .SplitByNewline()
            .Chunk(3)
            .SelectMany(ParseChunk)
            .Count(IsTriangle)
            .ToString();

    private static IEnumerable<int> AsIntArray(string input) => input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

    private static bool IsTriangle(IEnumerable<int> lengths)
    {
        var asArray = lengths.ToArray();
        if (asArray.Length != 3) throw new Exception("Source must have 3 elements.");
        if (asArray[0] >= asArray[1] + asArray[2]) return false;
        if (asArray[1] >= asArray[2] + asArray[0]) return false;
        if (asArray[2] >= asArray[0] + asArray[1]) return false;
        return true;
    }

    private static IEnumerable<IEnumerable<int>> ParseChunk(IEnumerable<string> input)
    {
        if (input.Count() != 3) throw new Exception("Expecting a chunk of size 3.");

        var allInts = input.SelectMany(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Select(int.Parse).ToArray();
        return new[] { 0, 1, 2 }.Select(i => new int[] { allInts[i], allInts[i + 3], allInts[i + 6] });
    }
}

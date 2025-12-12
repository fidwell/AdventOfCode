using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle12Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var regions = input.Chunk().Last().Select(ParseRegion).ToList();
        return regions.Count < 5 ? 2 : regions.Count(CanFitAllPresentsListed);
    }

    public override object SolvePartTwo(string input) => null!;

    private readonly record struct Region(int Width, int Height, int[] Quantities);

    private Region ParseRegion(string input)
    {
        var ints = StringExtensions.ParseInts(input);
        return new(ints[0], ints[1], [.. ints.Skip(2)]);
    }

    // You got punked
    private static bool CanFitAllPresentsListed(Region region) =>
        region.Quantities.Sum() * 9 <= region.Width * region.Height;
}

using AdventOfCode.Core.StringUtilities;

using Present = bool[,];

namespace AdventOfCode.Solvers._2025;

public class Puzzle12Solver : PuzzleSolver
{
    public override object SolvePartOne(string input)
    {
        var chunks = input.Chunk();
        var presents = chunks.Take(chunks.Count() - 1).Select(ParsePresent).ToArray();
        var regions = chunks.Last().Select(ParseRegion);
        return regions.Count(r => CanFitAllPresentsListed(r, presents));
    }

    public override object SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private readonly record struct Region(int Width, int Height, int[] Quantities);

    private Present ParsePresent(string[] input)
    {
        // Shapes are always 3x3
        var shape = new bool[3, 3];
        for (var y = 1; y < input.Length; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                shape[y - 1, x] = input[y][x] == '#';
            }
        }
        return shape;
    }

    private Region ParseRegion(string input)
    {
        var ints = StringExtensions.ParseInts(input);
        return new(ints[0], ints[1], [.. ints.Skip(2)]);
    }

    private static bool CanFitAllPresentsListed(Region region, Present[] presentDefinitions)
    {
        var availableArea = region.Width * region.Height;
        var maximumNeededSpace = region.Quantities.Sum() * 9;

        if (maximumNeededSpace < availableArea)
            // Easily fitted without rotating or flipping
            return true;

        var presentsRequired = region.Quantities.SelectMany((q, i) =>
            Enumerable.Range(0, q).Select(_ => presentDefinitions[i]));

        return maximumNeededSpace < availableArea;
    }

    // This is working toward some kind of naive depth-first search,
    // but there's no way it's going to be sufficient for how
    // large the search space is going to be
    private static bool CanFit(bool[,] regionSoFar, Present present)
    {
        for (var y = 0; y < regionSoFar.GetLength(0); y++)
        {
            for (var x = 0; x < regionSoFar.GetLength(1); x++)
            {
                if (DoesFitAt(regionSoFar, present, x, y))
                    return true;
            }
        }
        return false;
    }

    // Only for the original orientation
    private static bool DoesFitAt(bool[,] region, Present present, int tlX, int tlY)
    {
        for (var y = 0; y < 3; y++)
        {
            for (var x = 0; x < 3; x++)
            {
                if (region[y + tlY, x + tlX] && present[y, x])
                    // Region is already filled where the present needs to go
                    return false;
            }
        }
        return true;
    }
}

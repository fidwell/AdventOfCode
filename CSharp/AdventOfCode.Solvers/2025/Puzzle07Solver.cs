using AdventOfCode.Core.StringUtilities;
using Coord = (int, int);

namespace AdventOfCode.Solvers._2025;

public class Puzzle07Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var grid = new CharacterMatrix(input);

        var beams = new HashSet<Coord>
        {
            grid.SingleMatch('S')
        };

        var splits = 0;
        while (beams.All(b => b.Item2 < grid.Height))
        {
            var nextBeams = new HashSet<Coord>();
            foreach (var beam in beams)
            {
                if (grid.CharAt((beam.Item1, beam.Item2 + 1)) == '^')
                {
                    nextBeams.Add((beam.Item1 - 1, beam.Item2 + 1));
                    nextBeams.Add((beam.Item1 + 1, beam.Item2 + 1));
                    splits++;
                }
                else
                {
                    nextBeams.Add((beam.Item1, beam.Item2 + 1));
                }
            }

            beams = nextBeams;
        }

        return splits.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var grid = new CharacterMatrix(input);
        return PossibleStatesAfter(grid, grid.SingleMatch('S')).ToString();
    }

    // Todo - Figure out why custom Memoizer class didn't work
    private readonly Dictionary<Coord, long> cache = [];

    public long PossibleStatesAfter(CharacterMatrix grid, Coord beam)
    {
        if (cache.TryGetValue(beam, out long value))
            return value;

        if (beam.Item2 >= grid.Height)
        {
            cache.Add(beam, 1);
            return 1;
        }

        if (grid.CharAt((beam.Item1, beam.Item2 + 1)) == '^')
        {
            var result =
                PossibleStatesAfter(grid, (beam.Item1 - 1, beam.Item2 + 1)) +
                PossibleStatesAfter(grid, (beam.Item1 + 1, beam.Item2 + 1));
            cache.Add(beam, result);
            return result;
        }
        else
        {
            var result = PossibleStatesAfter(grid, (beam.Item1, beam.Item2 + 1));
            cache.Add(beam, result);
            return result;
        }
    }
}

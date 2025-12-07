using AdventOfCode.Core.Optimization;
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

    public static long PossibleStatesAfter(CharacterMatrix grid, Coord beam) =>
        Memoizer.Memoize<Coord, long>((c, func) =>
        {
            if (c.Item2 >= grid.Height)
                return 1;

            return grid.CharAt((c.Item1, c.Item2 + 1)) switch
            {
                '^' => func((c.Item1 - 1, c.Item2 + 1)) +
                    func((c.Item1 + 1, c.Item2 + 1)),
                _ => func((c.Item1, c.Item2 + 1))
            };
        })(beam);
}

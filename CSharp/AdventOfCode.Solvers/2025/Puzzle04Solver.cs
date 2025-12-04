using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var grid = new CharacterMatrix(input);
        return grid.AllCoordinates
            .Where(c => grid.CharAt(c) == '@')
            .Count(c => CanBeRemoved(grid, c))
            .ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var grid = new CharacterMatrix(input);
        var count = 0;

        do
        {
            var candidates = grid.AllCoordinates.Where(c => grid.CharAt(c) == '@' && CanBeRemoved(grid, c));
            if (!candidates.Any())
            {
                break;
            }

            foreach (var c in candidates)
            {
                grid.SetCharacter(c, '.');
                count++;
            }
        } while (true);

        return count.ToString();
    }

    private static bool CanBeRemoved(CharacterMatrix grid, (int, int) coord) =>
        grid.CoordinatesOfNeighbors(coord).Count(n => grid.CharAt(n) == '@') < 4;
}

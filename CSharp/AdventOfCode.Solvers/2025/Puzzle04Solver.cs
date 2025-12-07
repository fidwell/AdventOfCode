using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2025;

public class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var grid = new CharacterMatrix(input);
        return grid
            .FindAllCharacters('@')
            .Count(c => CanBeRemoved(grid, c))
            .ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var grid = new CharacterMatrix(input);
        var queue = new Queue<Coord>(grid.FindAllCharacters('@')
            .Where(c => CanBeRemoved(grid, c)));
        var count = 0;

        while (queue.Count > 0)
        {
            var r = queue.Dequeue();
            if (grid.CharAt(r) == '.')
                continue;

            grid.SetCharacter(r, '.');
            count++;

            var neighbors = grid.CoordinatesOfNeighbors(r);
            foreach (var n in neighbors.Where(n => grid.CharAt(n) == '@' && CanBeRemoved(grid, n)))
            {
                queue.Enqueue(n);
            }
        }

        return count.ToString();
    }

    private static bool CanBeRemoved(CharacterMatrix grid, Coord coord) =>
        grid.ValuesOfNeighbors(coord).Count(c => c == '@') < 4;
}

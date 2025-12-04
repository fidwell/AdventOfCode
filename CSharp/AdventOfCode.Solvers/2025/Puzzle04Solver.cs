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
        var rolls = grid.FindAllCharacters('@').ToHashSet();
        var originalCount = rolls.Count;

        do
        {
            var removableRolls = rolls.Where(c => CanBeRemoved(grid, c));
            if (!removableRolls.Any())
                break;

            foreach (var c in removableRolls)
            {
                grid.SetCharacter(c, '.');
                rolls.Remove(c);
            }
        } while (true);

        return (originalCount - rolls.Count).ToString();
    }

    private static bool CanBeRemoved(CharacterMatrix grid, (int, int) coord) =>
        grid.ValuesOfNeighbors(coord).Count(c => c == '@') < 4;
}

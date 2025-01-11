using System.Text;
using AdventOfCode.Core.Geometry;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle02Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, new CharacterMatrix("123\r\n456\r\n789"));
    public override string SolvePartTwo(string input) => Solve(input, new CharacterMatrix("..1..\r\n.234.\r\n56789\r\n.ABC.\r\n..D.."));

    private static string Solve(string input, CharacterMatrix keyPad)
    {
        var location = keyPad.SingleMatch('5');
        var lines = input.SplitByNewline();
        var code = new StringBuilder();

        foreach (var line in lines)
        {
            foreach (var instruction in line)
            {
                var direction = instruction.ToDirection();
                var newLocation = location.Go(direction);
                if (keyPad.IsInBounds(newLocation) && keyPad.CharAt(newLocation) != '.')
                {
                    location = newLocation;
                }
            }
            var keyValue = keyPad.CharAt(location);
            code.Append(keyValue);
        }

        return code.ToString();
    }
}

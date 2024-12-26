using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle22Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var lines = input.SplitByNewline().ToArray();
        var bossHp = int.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
        var bossDamge = int.Parse(Regexes.NonNegativeInteger().Match(lines[1]).Value);
        throw new NotImplementedException();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    public enum Spell
    {
        MagicMissile,
        Drain,
        Shield,
        Poison,
        Recharge
    }
}

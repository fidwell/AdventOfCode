using AdventOfCode.Solvers.Common;

namespace AdventOfCode.Solvers._2015;

public class Puzzle01Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) =>
        2 * input.Count(c => c == '(') - input.Length;

    public override object SolvePartTwo(string input)
    {
        var floor = 0;

        for (var i = 0; i < input.Length; i++)
        {
            floor += input[i] == '(' ? 1 : -1;
            if (floor == -1)
                return i + 1;
        }

        throw new SolutionNotFoundException();
    }
}

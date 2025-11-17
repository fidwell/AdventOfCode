namespace AdventOfCode.Solvers._2022;

public class Puzzle06Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, 4).ToString();

    public override string SolvePartTwo(string input) => Solve(input, 14).ToString();

    private static int Solve(string input, int size)
    {
        for (var i = 0; i < input.Length - size; i++)
        {
            var substring = input.Substring(i, size);
            if (substring.ToCharArray().Distinct().Count() == size)
                return i + size;
        }

        return -1;
    }
}

using AdventOfCode.Solvers;

namespace AdventOfCode.Solvers._2022;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(input, 4).ToString();

    public string SolvePartTwo(string input) => Solve(input, 14).ToString();

    private static int Solve(string input, int size)
    {
        for (int i = 0; i < input.Length - size; i++)
        {
            var substring = input.Substring(i, size);
            if (substring.ToCharArray().Distinct().Count() == size)
                return i + size;
        }

        return -1;
    }
}

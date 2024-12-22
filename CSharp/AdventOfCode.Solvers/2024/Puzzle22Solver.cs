using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle22Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var numbers = input.SplitByNewline().Select(int.Parse);
        var answers = numbers.Select(n => EvolveNTimes(n, 2000));
        return answers.Aggregate(0UL, (sum, answer) => sum + (ulong)answer).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static int EvolveNTimes(int number, int times)
    {
        for (var i = 0; i < times; i++)
        {
            number = Evolve(number);
        }
        return number;
    }

    private static int Evolve(int number)
    {
        const int n16777216minus1 = 0b1111_1111_1111_1111_1111_1111;

        number = ((number << 6) ^ number) & n16777216minus1;
        number = ((number >> 5) ^ number) & n16777216minus1;
        number = ((number << 11) ^ number) & n16777216minus1;
        return number;
    }
}

namespace AdventOfCode.Solvers._2015;

public class Puzzle20Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        // Say each elf gives 1 present instead of 10,
        // and divide our target by 10 instead.
        var target = int.Parse(input) / 10;

        // https://oeis.org/A000203
        // https://en.wikipedia.org/wiki/Divisor_function
        // 𝜎_1(n)

        var h = 664720;
        int divFun;
        do
        {
            h += 1;
            divFun = DivisorFunction(h);
            Console.WriteLine($"House {h}: {divFun}");
        } while (divFun < target);

        return h.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static int DivisorFunction(int input)
    {
        var sum = 0;
        for (var i = 1; i <= input; i++)
        {
            if (input % i == 0)
                sum += i;
        }
        return sum;
    }
}

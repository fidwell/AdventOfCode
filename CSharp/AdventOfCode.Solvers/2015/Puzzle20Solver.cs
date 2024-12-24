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
        var target = int.Parse(input);

        var houses = new Dictionary<int, int>();
        for (var elf = 1; elf < target / 10; elf++)
        {
            for (var house = elf; house <= elf * 50; house += elf)
            {
                var delivered = 11 * elf;
                if (!houses.TryAdd(house, delivered))
                {
                    houses[house] += delivered;
                }
            }
        }

        var matchingHouses = houses.Where(h => h.Value >= target).OrderBy(h => h.Key);
        return matchingHouses.Select(h => h.Key).Min().ToString();
    }

    private static int DivisorFunction(int input)
    {
        var sum = 0;
        for (var e = 1; e <= input; e++)
        {
            if (input % e == 0)
                sum += e;
        }
        return sum;
    }
}

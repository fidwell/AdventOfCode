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

                //if (houses[house] >= target)
                //{
                //    Console.WriteLine($"elf {elf}, house {house}");
                //    return house.ToString();
                //}
            }
        }

        var matchingHouses = houses.Where(h => h.Value >= target).OrderBy(h => h.Key);
        return matchingHouses.Select(h => h.Key).Min().ToString();

        // TRY 705600
        // 1441440 is WRONG
        // 3255840 is WRONG
        // 3326400 is TOO HIGH

        //return houses.First(h => h.Value >= target).Key.ToString();

        //foreach (var house in houses)
        //{
        //    Console.WriteLine($"{house.Key}: {house.Value}");
        //}

        /*

        // Elf 1 visits 1, 2, 3, ... 50
        // Elf 2 visits 2, 4, 6, ... 100
        // Elf 3 visits 3, 6, 9, ... 150
        // House h gets visited by elf e if e|h and e*50 <= h

        // Answer is lower than 4324320
        var h = 4324320;
        int presentCount;
        do
        {
            h -= 1;
            presentCount = 11 * DivisorFunction2(h);
            if (presentCount == target)
                Console.WriteLine($"House {h}: {presentCount}");
        } while (h > 4000000);


        return h.ToString();
        */
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

    private static int DivisorFunction2(int input)
    {
        var sum = 0;
        for (var e = 1; e <= input; e++)
        {
            if (e * 50 <= input && input % e == 0)
                sum += e;
        }
        return sum;
    }
}

using AdventOfCode.Core.MathUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle22Solver : PuzzleSolver
{
    const int TotalSecretNumbers = 2000;

    public override string SolvePartOne(string input) =>
        input.SplitByNewline().Select(int.Parse)
            .Select(n => EvolveNTimes(n, TotalSecretNumbers))
            .Select(n => (ulong)n)
            .Sum().ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline().Select(int.Parse)
            .Select(n => ComputeBuyerValues(n, TotalSecretNumbers))
            .SelectMany(b => b)
            .GroupBy(ds => ds.Key)
            .Select(g => g.Sum(x => x.Value))
            .Max().ToString();

    private static IEnumerable<int> Evolutions(int number, int times)
    {
        for (var i = 0; i < times; i++)
        {
            yield return number;
            number = Evolve(number);
        }
        yield return number;
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

    private static Dictionary<int, int> ComputeBuyerValues(int seed, int sequenceLength)
    {
        var lastDigits = Evolutions(seed, sequenceLength).Select(n => n % 10).ToArray();
        var differences = lastDigits.Zip(lastDigits.Skip(1), (a, b) => b - a).ToArray();

        var result = new Dictionary<int, int>();

        for (var i = 0; i < differences.Length - 3; i++)
        {
            var key = (differences[i], differences[i + 1], differences[i + 2], differences[i + 3]).GetHashCode();
            var value = lastDigits[i + 4];

            if (!result.TryGetValue(key, out var cachedValue))
            {
                result.Add(key, value);
            }
        }

        return result;
    }
}

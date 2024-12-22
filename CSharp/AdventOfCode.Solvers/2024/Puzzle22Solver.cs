using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle22Solver : PuzzleSolver
{
    const int TotalSecretNumbers = 2000;

    public override string SolvePartOne(string input) =>
        input.SplitByNewline().Select(int.Parse)
            .Select(n => EvolveNTimes(n, TotalSecretNumbers))
            .Aggregate(0UL, (sum, answer) => sum + (ulong)answer)
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline().Select(int.Parse)
            .Select(n => new Buyer(n, TotalSecretNumbers))
            .SelectMany(b => b.PrecomputedValues)
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

    private class Buyer
    {
        public int[] LastDigits { get; } = [];
        public int[] Differences { get; } = [];

        public Dictionary<int, int> PrecomputedValues { get; } = [];

        public Buyer(int seed, int sequenceLength)
        {
            var sequence = Evolutions(seed, sequenceLength).ToArray();
            LastDigits = sequence.Select(n => n % 10).ToArray();
            Differences = LastDigits.Zip(LastDigits.Skip(1), (a, b) => b - a).ToArray();

            for (var i = 0; i < Differences.Length - 3; i++)
            {
                var key = (Differences[i], Differences[i + 1], Differences[i + 2], Differences[i + 3]).GetHashCode();
                var value = LastDigits[i + 4];

                if (!PrecomputedValues.TryGetValue(key, out var cachedValue))
                {
                    PrecomputedValues.Add(key, value);
                }
            }
        }
    }
}

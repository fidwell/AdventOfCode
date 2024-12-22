using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle22Solver : PuzzleSolver
{
    const int TotalSecretNumbers = 2000;
    const int MiniSequenceLength = 4;

    public override string SolvePartOne(string input)
    {
        var numbers = input.SplitByNewline().Select(int.Parse);
        var answers = numbers.Select(n => EvolveNTimes(n, TotalSecretNumbers));
        return answers.Aggregate(0UL, (sum, answer) => sum + (ulong)answer).ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var numbers = input.SplitByNewline().Select(int.Parse);
        var sequenceLength = TotalSecretNumbers;
        var buyers = numbers.Select(n => new Buyer(n, sequenceLength)).ToArray();

        var highestPrice = 0;
        for (var i = 0; i < sequenceLength - MiniSequenceLength + 1; i++)
        {
            var sequence = buyers[0].DiffSequenceAt(i);

            //if (sequence.SequenceEqual([-2, 1, -1, 3]))
            //{

            //}

            var pricesHere = buyers.Select(b => b.PriceAtDiffSequence(sequence));
            var totalPricesHere = pricesHere.Sum();
            if (totalPricesHere > highestPrice)
            {
                Console.WriteLine($"Better sequence at index {i} of {string.Join(",", sequence)}; total is {totalPricesHere}");
                highestPrice = totalPricesHere;
            }
        }

        // 1631 is too low
        // 1650 is wrong
        return highestPrice.ToString();
    }

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
        public int[] Sequence { get; } = [];
        public int[] LastDigits { get; } = [];
        public int[] Differences { get; } = [];

        public Dictionary<(int, int, int, int), int> PrecomputedValues { get; } = [];

        public Buyer(int seed, int sequenceLength)
        {
            Sequence = Evolutions(seed, sequenceLength).ToArray();
            LastDigits = Sequence.Select(n => n % 10).ToArray();
            Differences = LastDigits.Zip(LastDigits.Skip(1), (a, b) => b - a).ToArray();

            for (var i = 0; i < Differences.Length - MiniSequenceLength + 1; i++)
            {
                var key = (Differences[i], Differences[i + 1], Differences[i + 2], Differences[i + 3]);
                var value = LastDigits[i + 4];

                if (PrecomputedValues.TryGetValue(key, out var cachedValue))
                {
                    if (value > cachedValue)
                    {
                        PrecomputedValues[key] = value;
                    }
                }
                else
                {
                    PrecomputedValues.Add(key, value);
                }
            }
        }

        public int[] DiffSequenceAt(int index) =>
            Differences.Skip(index).Take(MiniSequenceLength).ToArray();

        public int PriceAtDiffSequence(int[] sequence)
        {
            if (PrecomputedValues.TryGetValue((sequence[0], sequence[1], sequence[2], sequence[3]), out int value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
    }
}

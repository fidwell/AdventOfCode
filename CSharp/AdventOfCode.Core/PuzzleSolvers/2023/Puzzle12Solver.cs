using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle12Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) =>
        DataReader.GetData(12, 0, useSample)
        .Split(Environment.NewLine)
        .Select(l => new ConditionRecord(l))
        .Select(r => r.PossibleArrangementCount)
        .Sum()
        .ToString();

    public string SolvePartTwo(bool useSample = false)
    {
        throw new NotImplementedException();
    }

    private class ConditionRecord
    {
        private string _asString;
        private int[] _groupCounts;

        public int PossibleArrangementCount { get; private set; }

        public ConditionRecord(string input)
        {
            var data = input.Split(" ");
            _asString = data[0];
            _groupCounts = data[1].Split(",").Select(int.Parse).ToArray();

            var arrangements = FindPossibleArrangements(_asString).Where(Verify).ToList();
            PossibleArrangementCount = arrangements.Count;
        }

        private bool Verify(string input) =>
            Enumerable.SequenceEqual(input.SplitAndTrim(".").Select(g => g.Length), _groupCounts);

        private static IEnumerable<string> FindPossibleArrangements(string input)
        {
            // Probably inefficient recursion...
            if (!input.Contains('?'))
            {
                yield return input;
            }
            else
            {
                var firstQ = input.IndexOf('?');

                foreach (var result in FindPossibleArrangements($"{input.Substring(0, firstQ)}#{input.Substring(firstQ + 1)}"))
                    yield return result;

                foreach (var result in FindPossibleArrangements($"{input.Substring(0, firstQ)}.{input.Substring(firstQ + 1)}"))
                    yield return result;
            }
        }
    }
}

using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle09Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input
        .SplitByNewline()
        .Select(l => new DataLine(l))
        .Select(l => l.GetNextPredicted())
        .Sum()
        .ToString();

    public override string SolvePartTwo(string input) =>
        input
        .SplitByNewline()
        .Select(l => new DataLine(l))
        .Select(l => l.GetPreviousPredicted())
        .Sum()
        .ToString();

    private class DataLine
    {
        private readonly IList<int[]> _values;

        public DataLine(string input)
        {
            _values = [[.. input.Split(" ").Select(int.Parse)]];
            FillDifferences();
        }

        public int GetNextPredicted()
        {
            ExtrapolateForwards();
            return _values[0].Last();
        }

        public int GetPreviousPredicted()
        {
            ExtrapolateBackwards();
            return _values[0].First();
        }

        private void FillDifferences()
        {
            int[] nextState = [];
            do
            {
                var lastRow = _values.Last();
                nextState = [.. lastRow.Skip(1).Select((value, index) => lastRow[index + 1] - lastRow[index])];
                _values.Add(nextState);
            } while (nextState.Any(x => x != 0));
        }

        private void ExtrapolateForwards()
        {
            AppendToRow(_values.Count - 1, 0);

            for (var i = _values.Count - 2; i >= 0; i--)
            {
                AppendToRow(i, _values[i].Last() + _values[i + 1].Last());
            }
        }

        private void ExtrapolateBackwards()
        {
            PrependToRow(_values.Count - 1, 0);

            for (var i = _values.Count - 2; i >= 0; i--)
            {
                var x = _values[i].First() - _values[i + 1].First();
                PrependToRow(i, x);
            }
        }

        public void AppendToRow(int rowIndex, int value)
            => _values[rowIndex] = [.. _values[rowIndex], value];

        public void PrependToRow(int rowIndex, int value)
            => _values[rowIndex] = [value, .. _values[rowIndex]];

        public override string ToString() => string.Join(" | ", _values.Select(v => string.Join(" ", v)));
    }
}

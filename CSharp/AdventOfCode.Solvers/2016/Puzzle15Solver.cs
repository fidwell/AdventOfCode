using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle15Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input, false);
    public override object SolvePartTwo(string input) => Solve(input, true);

    public static int Solve(string input, bool isPartTwo)
    {
        var discs = input.SplitByNewline().Select(l =>
        {
            var values = l.ParseInts();
            return new Disc(values[0] - 1, values[1], values[3]);
        }).ToList();

        if (isPartTwo)
        {
            discs.Add(new Disc(discs.Count, 11, 0));
        }

        var t = 0;

        // Simply counting up at every t is sufficient...
        while (discs.Any(d => !d.IsAtDesiredPosition))
        {
            discs.ForEach(d => d.Tick());
            t++;
        }

        return t - 1;
    }

    private class Disc(int position, int slotCount, int state)
    {
        public int State = state;

        private readonly int SlotCount = slotCount;
        private readonly int DesiredStateAtButtonPress = (slotCount * 2 - position) % slotCount;

        public void Tick() => State = (State + 1) % SlotCount;

        public bool IsAtDesiredPosition => State == DesiredStateAtButtonPress;
    }
}

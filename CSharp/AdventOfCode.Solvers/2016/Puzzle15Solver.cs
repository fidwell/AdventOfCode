using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle15Solver : PuzzleSolver
{
    public override object SolvePartOne(string input) => Solve(input, false);
    public override object SolvePartTwo(string input) => Solve(input, true);

    public static int Solve(string input, bool isPartTwo)
    {
        var discs = input.SplitByNewline().Select((l, i) =>
        {
            var words = l.Replace(".", "").Split(" ");
            var positionCount = int.Parse(words[3]);
            var stateAtTime0 = int.Parse(words[11]);
            return new Disc(positionCount, stateAtTime0, i);
        }).ToList();

        if (isPartTwo)
        {
            discs.Add(new Disc(11, 0, discs.Count));
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

    private class Disc(int positionCount, int state, int position)
    {
        public int State = state;

        private readonly int PositionCount = positionCount;
        private readonly int DesiredPositionAtButtonPress = (positionCount * 2 - position) % positionCount;

        public void Tick() => State = (State + 1) % PositionCount;

        public bool IsAtDesiredPosition => State == DesiredPositionAtButtonPress;
    }
}

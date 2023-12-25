using AdventOfCode.Core.Poker;
using AdventOfCode.Solvers;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle07Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(true, input);
    public string SolvePartTwo(string input) => Solve(false, input);

    private static string Solve(bool isPartOne, string input)
        => input
            .Split(Environment.NewLine)
            .Select(l => new InputLine(l, !isPartOne))
            .OrderBy(l => l.Hand)
            .Select((line, ix) => (long)(line.Bid * (ix + 1)))
            .Sum().ToString();

    private class InputLine
    {
        public PokerHand Hand { get; private set; }
        public int Bid { get; private set; }

        public InputLine(string line, bool jIsJoker)
        {
            var data = line.Split(" ");
            Hand = new PokerHand(data[0], jIsJoker);
            Bid = int.Parse(data[1]);
        }

        public override string ToString() => $"{Hand.Input} {Hand.Type} {Bid}";
    }
}

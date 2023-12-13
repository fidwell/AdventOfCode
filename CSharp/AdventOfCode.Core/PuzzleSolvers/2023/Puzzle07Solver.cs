using AdventOfCode.Core.Poker;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle07Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) => Solve(true, useSample);
    public string SolvePartTwo(bool useSample = false) => Solve(false, useSample);

    private static string Solve(bool isPartOne, bool useSample = false)
        => DataReader.GetData(7, useSample)
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

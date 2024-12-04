using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2023;

public class Puzzle04Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) =>
        GetCards(input)
        .Sum(c => c.Score)
        .ToString();

    public string SolvePartTwo(string input)
    {
        var cards = GetCards(input).ToList();
        cards.ForEach(card =>
            Enumerable.Range(card.Id, card.MatchingCount).ToList()
                .ForEach(index => cards[index].CopyCount += card.CopyCount));
        return cards.Sum(c => c.CopyCount).ToString();
    }

    private static IEnumerable<Scratchcard> GetCards(string input)
        => input.Split(Environment.NewLine).Select(l => new Scratchcard(l));

    private class Scratchcard
    {
        public readonly int Id;
        public readonly int MatchingCount;
        public int CopyCount;

        public Scratchcard(string input)
        {
            Id = int.Parse(input.Split(": ")[0].Substring("Card ".Length).Trim());
            var numberData = input.Split(": ")[1].SplitAndTrim(" | ");
            var winningNumbers = numberData[0].SplitAndTrim(' ').Select(int.Parse);
            var ownNumbers = numberData[1].SplitAndTrim(' ').Select(int.Parse);
            MatchingCount = winningNumbers.Intersect(ownNumbers).Count();
            CopyCount = 1;
        }

        // Hacky, but it works for part 1
        // (a score of 0 is really calculated as 0.5)
        public int Score => (int)Math.Pow(2, MatchingCount - 1);
    }
}

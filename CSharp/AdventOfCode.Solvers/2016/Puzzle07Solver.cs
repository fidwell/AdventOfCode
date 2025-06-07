using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle07Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
        => input.SplitByNewline().Select(l => new Ip(l)).Count(l => l.SupportsTLS).ToString();

    public override string SolvePartTwo(string input)
        => input.SplitByNewline().Select(l => new Ip(l)).Count(l => l.SupportsSSL).ToString();

    private class Ip
    {
        readonly IEnumerable<string> SupernetSequences = [];
        readonly IEnumerable<string> HypernetSequences = [];

        public Ip(string input)
        {
            var chunks = input.Split('[', ']').ToArray();
            var indexed = chunks.Select((c, i) => new
            {
                index = i,
                chunk = c
            });
            SupernetSequences = indexed.Where(ci => ci.index % 2 == 0).Select(ci => ci.chunk);
            HypernetSequences = indexed.Where(ci => ci.index % 2 != 0).Select(ci => ci.chunk);
        }

        public bool SupportsTLS => SupernetSequences.Any(ContainsAbba) && HypernetSequences.All(c => !ContainsAbba(c));

        public bool SupportsSSL
        {
            get
            {
                var abas = SupernetSequences.SelectMany(FindAbas);
                var correspondingBabs = abas.Select(ToBab);
                var babs = HypernetSequences.SelectMany(FindAbas);
                return babs.Any(b => correspondingBabs.Contains(b));
            }
        }

        private static bool ContainsAbba(string chunk)
        {
            if (chunk.Length < 4)
                return false;

            for (var i = 0; i <= chunk.Length - 4; i++)
            {
                if (IsAbba(chunk, i))
                    return true;
            }

            return false;
        }

        private static bool IsAbba(string chunk, int startingAt) =>
            chunk[0 + startingAt] != chunk[1 + startingAt] &&
            chunk[0 + startingAt] == chunk[3 + startingAt] &&
            chunk[1 + startingAt] == chunk[2 + startingAt];

        private static IEnumerable<string> FindAbas(string input)
        {
            for (var i = 0; i <= input.Length - 3; i++)
            {
                if (input[i] != input[i + 1] && input[i] == input[i + 2])
                    yield return input.Substring(i, 3);
            }
        }

        private static string ToBab(string aba) => $"{aba[1]}{aba[0]}{aba[1]}";
    }
}

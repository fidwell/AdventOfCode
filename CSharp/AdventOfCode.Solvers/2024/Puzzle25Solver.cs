using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle25Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var chunks = input.Chunk()
            .Where(c => c.Length > 0)
            .Select(l => string.Join(string.Empty, l))
            .ToArray();
        var count = 0;

        for (var i = 0; i < chunks.Length - 1; i++)
        {
            for (var j = i + 1; j < chunks.Length; j++)
            {
                if (chunks[i].Select((c, i) => (c, i)).All(t => t.c == '.' || chunks[j][t.i] != '#'))
                    count++;
            }
        }

        return count.ToString();
    }

    public override string SolvePartTwo(string input) => string.Empty;
}

using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle25Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var chunks = input.SplitByNewline(StringSplitOptions.None).Chunk();

        var keys = new List<Key>();
        var locks = new List<Lock>();

        foreach (var chunk in chunks.Where(c => c.Length > 0))
        {
            if (chunk[0].All(c => c == '#'))
            {
                locks.Add(new Lock(Enumerable.Range(0, 5).Select(col =>
                {
                    return chunk.Select(c => c[col]).Count(c => c == '#') - 1;
                }).ToArray()));
            }
            else
            {
                keys.Add(new Key(Enumerable.Range(0, 5).Select(col =>
                {
                    return chunk.Select(c => c[col]).Count(c => c == '#') - 1;
                }).ToArray()));
            }
        }

        var sum = 0;
        for (var k = 0; k < keys.Count; k++)
        {
            for (var l = 0; l < locks.Count; l++)
            {
                if (DoFit(keys[k], locks[l]))
                    sum++;
            }
        }

        return sum.ToString();
    }

    public override string SolvePartTwo(string input) => string.Empty;

    private readonly record struct Key(int[] Values);
    private readonly record struct Lock(int[] Values);

    private static bool DoFit(Key k, Lock l) =>
        k.Values.Select((v, i) => (v, i)).All(t => t.v + l.Values[t.i] <= 5);
}

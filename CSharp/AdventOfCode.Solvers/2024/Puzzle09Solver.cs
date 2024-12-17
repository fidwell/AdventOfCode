namespace AdventOfCode.Solvers._2024;

public class Puzzle09Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        input = input.ReplaceLineEndings("");
        var idFromLeft = input.Length / 2;
        var amountLeftOnLeft = input[^1] - '0';
        ulong total = 0;
        var block = 0;
        var allDone = false;

        for (var i = 0; i < input.Length && !allDone; i++)
        {
            var blockLength = input[i] - '0';
            for (var j = 0; j < blockLength; j++)
            {
                int idHere;
                if (i % 2 == 0)
                {
                    // Calculate full block
                    idHere = i / 2;

                    if (idHere == idFromLeft)
                    {
                        // Last block
                        blockLength = amountLeftOnLeft;
                        allDone = true;
                    }
                }
                else
                {
                    // Calculate empty block
                    idHere = idFromLeft;
                    amountLeftOnLeft--;
                    if (amountLeftOnLeft == 0)
                    {
                        idFromLeft--;
                        amountLeftOnLeft = input[idFromLeft * 2] - '0';

                        if (idFromLeft == i)
                            break;
                    }
                }
                total += (ulong)((block + j) * idHere);
            }
            block += blockLength;
        }

        return total.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var memory = LoadMemory(input);
        Defrag(memory);
        return Checksum(memory).ToString();
    }

    private static List<MemChunk> LoadMemory(string input)
    {
        var values = input.ReplaceLineEndings("").ToCharArray().Select(c => c - '0');
        var index = 0;
        var result = new List<MemChunk>();
        foreach (var it in values.Select((v, i) => (v, i)))
        {
            result.Add(new MemChunk(index, it.i % 2 == 0 ? it.i / 2 : -1, it.v));
            index += it.v;
        }
        return result;
    }

    private static void Defrag(List<MemChunk> memory)
    {
        for (var j = memory.Count - 1; j > 1; j -= 2)
        {
            for (var i = 1; i < j; i += 2)
            {
                var used = memory[j];
                var empty = memory[i];
                if (empty.Length >= used.Length)
                {
                    used.Index = empty.Index;
                    empty.Index += used.Length;
                    empty.Length -= used.Length;
                    break;
                }
            }
        }
    }

    private static ulong Checksum(List<MemChunk> memory) =>
        memory.Where(m => m.Id > 0).Aggregate(0UL, (aggregate, chunk) =>
            aggregate + Enumerable.Range(0, chunk.Length)
                .Aggregate(0UL, (a, b) => a + (ulong)((chunk.Index + b) * chunk.Id)));

    private class MemChunk(int Index, int Id, int Length)
    {
        public int Index { get; set; } = Index;
        public int Id { get; } = Id;
        public int Length { get; set; } = Length;
    }
}

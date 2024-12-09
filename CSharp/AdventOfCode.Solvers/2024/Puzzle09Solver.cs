namespace AdventOfCode.Solvers._2024;

public class Puzzle09Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var memory = LoadMemory1(input);
        Defrag1(memory);
        return Checksum1(memory).ToString();
    }

    public string SolvePartTwo(string input)
    {
        var memory = LoadMemory2(input);
        memory = Defrag2(memory);
        return Checksum2(memory).ToString();
    }

    private static int[] LoadMemory1(string input)
    {
        var blockSizes = input.ToCharArray().Select(c => c - '0').ToArray();
        var totalLength = blockSizes.Sum();
        var memory = new int[totalLength + 100]; // why do we need this buffer?

        // initialize memory
        for (var i = 0; i < memory.Length; i++)
        {
            memory[i] = -1;
        }

        // load memory
        var currentBlockId = 0;
        for (var i = 0; i < memory.Length;)
        {
            if (currentBlockId == blockSizes.Length)
                break;

            var currentBlockSize = blockSizes[currentBlockId];
            for (var j = 0; j < currentBlockSize; j++)
            {
                if (currentBlockId % 2 == 0)
                    memory[i] = currentBlockId / 2;

                i++;
            }

            currentBlockId++;
        }

        return memory;
    }

    private static void Defrag1(int[] memory)
    {
        for (var i = memory.Length - 1; i > 0; i--)
        {
            var firstFree = Array.IndexOf(memory, -1);

            // No free memory
            if (firstFree == -1) break;

            // Done defragging
            if (firstFree >= i) break;

            if (memory[i] == -1)
                continue;

            memory[firstFree] = memory[i];
            memory[i] = -1;
        }
    }

    private static ulong Checksum1(int[] memory)
    {
        ulong checksum = 0;
        for (var i = memory.Length - 1; i > 0; i--)
        {
            if (memory[i] >= 0)
                checksum += (ulong)(i * memory[i]);
        }
        return checksum;
    }

    private static List<MemChunk> LoadMemory2(string input) =>
        input.ToCharArray().Select(c => c - '0')
        .Select((bs, i) => new MemChunk(i % 2 == 0 ? i / 2 : -1, bs))
        .Where(c => c.Length > 0)
        .ToList();

    private static List<MemChunk> Defrag2(List<MemChunk> memory)
    {
        for (var id = memory.Last().Id; id >= 0; id--)
        {
            var memIndex = memory.Index();
            var chunkHere = memIndex.Single(m => m.Item.Id == id);

            var firstEmptyChunk = memory.Index()
                .FirstOrDefault(x =>
                    x.Item.Id == -1 &&                       // it's empty (no id)
                    x.Index < chunkHere.Index &&             // it's earlier in memory
                    x.Item.Length >= chunkHere.Item.Length); // it would fit

            if (firstEmptyChunk.Item is null)
                continue;

            var leftoverEmptyLength = firstEmptyChunk.Item.Length - chunkHere.Item.Length;

            // Replace old slot with empty memory
            memory.RemoveAt(chunkHere.Index);
            memory.Insert(chunkHere.Index, new MemChunk(-1, chunkHere.Item.Length));

            // Replace new slot with filled memory + leftovers
            memory.RemoveAt(firstEmptyChunk.Index);
            memory.Insert(firstEmptyChunk.Index, new MemChunk(chunkHere.Item.Id, chunkHere.Item.Length));
            memory.Insert(firstEmptyChunk.Index + 1, new MemChunk(-1, leftoverEmptyLength));

            // Merge consecutive empty chunks
            for (var i = 0; i < memory.Count - 1; i++)
            {
                while (i < memory.Count - 1 && memory[i].Id == -1 && memory[i + 1].Id == -1)
                {
                    var totalLength = memory[i].Length + memory[i + 1].Length;
                    memory.RemoveAt(i);
                    memory.RemoveAt(i);
                    memory.Insert(i, new MemChunk(-1, totalLength));
                }
            }

            memory = memory.Where(m => m.Length > 0).ToList();
        }

        return memory;
    }

    private static ulong Checksum2(List<MemChunk> memory)
    {
        var checksum = 0UL;

        var block = 0;
        for (var i = 0; i < memory.Count; i++)
        {
            for (var j = 0; j < memory[i].Length; j++)
            {
                checksum += memory[i].Id >= 0 ? (ulong)(block * memory[i].Id) : 0;
                block++;
            }
        }

        return checksum;
    }

    private record MemChunk(int Id, int Length);
}

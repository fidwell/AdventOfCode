using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false) =>
        new Almanac(DataReader.GetData(5, 0, useSample))
            .LowestLocationNumberThatCorrespondsToAnyOfTheInitialSeedNumbers()
            .ToString();

    public string SolvePartTwo(bool useSample = false)
    {
        throw new NotImplementedException();
    }

    private class Almanac
    {
        private readonly IEnumerable<long> _seeds;
        private readonly IList<Map> _maps;

        public Almanac(string input)
        {
            var data = input.Split(Environment.NewLine);
            _seeds = data[0].Split(": ")[1].SplitAndTrim(" ").Select(long.Parse);
            _maps = ArrayChunker.Chunk(data.Skip(2).ToArray()).Select(d => new Map(d)).ToList();
        }

        public long LowestLocationNumberThatCorrespondsToAnyOfTheInitialSeedNumbers() =>
            _seeds.Select(seed =>
            {
                var value = seed;
                foreach (var map in _maps)
                {
                    value = map.MapValue(value);
                }
                return value;
            }).Min();
    }

    private class Map(string[] data)
    {
        private readonly IEnumerable<MapRange> _mapRanges = data.Skip(1).Select(l => new MapRange(l));

        public long MapValue(long input)
        {
            foreach (var range in _mapRanges)
            {
                if (range.SourceRangeStart <= input && range.SourceRangeStart + range.RangeLength > input)
                {
                    var diff = input - range.SourceRangeStart;
                    return range.DestinationRangeStart + diff;
                }
            }

            return input;
        }

        private class MapRange
        {
            public readonly long DestinationRangeStart;
            public readonly long SourceRangeStart;
            public readonly long RangeLength;

            public MapRange(string input)
            {
                var numbers = input.Split(" ").Select(long.Parse).ToArray();
                DestinationRangeStart = numbers[0];
                SourceRangeStart = numbers[1];
                RangeLength = numbers[2];
            }
        }
    }
}

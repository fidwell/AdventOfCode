using AdventOfCode.Core.ArrayUtilities;
using AdventOfCode.Core.Ranges;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle05Solver : IPuzzleSolver
{
    public string SolvePartOne(string input) => Solve(true, input);
    public string SolvePartTwo(string input) => Solve(false, input);

    private static string Solve(bool isPartOne, string input) =>
        new Almanac(input, isPartOne)
            .LowestLocationNumberThatCorrespondsToAnyOfTheInitialSeedNumbers()
            .ToString();

    private class Almanac
    {
        private readonly IEnumerable<RangeLong> _seedRanges;
        private readonly IList<Map> _maps;

        public Almanac(string input, bool isPartOne)
        {
            var data = input.Split(Environment.NewLine);
            var seedData = data[0].Split(": ")[1].SplitAndTrim(" ").Select(long.Parse).ToArray();
            _seedRanges = isPartOne
                ? seedData.Select(s => new RangeLong(s, 1))
                : seedData
                    .Where((x, i) => i % 2 == 0)
                    .Select((x, i) => new RangeLong(seedData[i * 2], seedData[i * 2 + 1]));
            _maps = data.Skip(2).ToArray().Chunk().Select(d => new Map(d)).ToList();
        }

        public long LowestLocationNumberThatCorrespondsToAnyOfTheInitialSeedNumbers()
        {
            var results = _seedRanges
                .Select(range =>
                {
                    IEnumerable<RangeLong> value = new[] { range };
                    foreach (var map in _maps)
                    {
                        value = map.TransformRanges(value).ToList();
                    }
                    return value;
                });
            return results.SelectMany(s => s).Min(r => r.Start);
        }
    }

    private class Map
    {
        private readonly IEnumerable<MapRange> _mapRanges;

        public Map(string[] data)
        {
            var mapRanges = data.Skip(1).Select(l => new MapRange(l)).OrderBy(r => r.SourceRangeStart).ToArray();

            IEnumerable<MapRange> nonTransformingRanges = Array.Empty<MapRange>();
            long min = 0;
            for (var i = 0; i < mapRanges.Length; i++)
            {
                var newRange = new MapRange(min, min, mapRanges[i].SourceRangeStart - min);
                if (newRange.RangeLength > 0)
                {
                    nonTransformingRanges = nonTransformingRanges.Append(newRange);
                }
                min = mapRanges[i].SourceRangeEnd;
            }
            nonTransformingRanges = nonTransformingRanges.Append(new MapRange(min, min, long.MaxValue / 2));

            _mapRanges = mapRanges.Union(nonTransformingRanges).OrderBy(r => r.SourceRangeStart);
        }

        public IEnumerable<RangeLong> TransformRanges(IEnumerable<RangeLong> input)
        {
            var results = new List<RangeLong>();
            foreach (var range in _mapRanges)
            {
                foreach (var inputRange in input)
                {
                    results.AddRange(range.Transform(inputRange));
                }
            }
            return results;
        }
    }

    public class MapRange
    {
        public long DestinationRangeStart { get; private set; }
        public long SourceRangeStart { get; private set; }
        public long RangeLength { get; private set; }
        public long Transformation { get; private set; }
        public long SourceRangeEnd { get; private set; }

        public MapRange(string input)
        {
            var numbers = input.Split(" ").Select(long.Parse).ToArray();
            Init(numbers[0], numbers[1], numbers[2]);
        }

        public MapRange(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            Init(destinationRangeStart, sourceRangeStart, rangeLength);
        }

        private void Init(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            DestinationRangeStart = destinationRangeStart;
            SourceRangeStart = sourceRangeStart;
            RangeLength = rangeLength;
            Transformation = DestinationRangeStart - SourceRangeStart;
            SourceRangeEnd = SourceRangeStart + RangeLength;
        }

        public IEnumerable<RangeLong> Transform(RangeLong input)
        {
            var intersection = AsRange.Intersection(input);
            return new RangeLong[]
                {
                    new(intersection.Start + Transformation, intersection.Length)
                }.Where(r => r.Length > 0);
        }

        private RangeLong AsRange => new (SourceRangeStart, RangeLength);

        public override string ToString() => $"[{SourceRangeStart},{SourceRangeEnd}) > [{DestinationRangeStart},{DestinationRangeStart + RangeLength}) ({RangeLength}); {Transformation}";
    }
}

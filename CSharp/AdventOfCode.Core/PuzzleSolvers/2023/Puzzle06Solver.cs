using AdventOfCode.Core.StringUtilities;
using AdventOfCode.Data;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle06Solver : IPuzzleSolver
{
    public string SolvePartOne(bool useSample = false)
    {
        var data = DataReader.GetData(6, 0, useSample).Split(Environment.NewLine);
        var times = data[0].Split(": ")[1].SplitAndTrim(" ");
        var records = data[1].Split(": ")[1].SplitAndTrim(" ");
        var races = times.Select((t, i) => new Race(int.Parse(t), int.Parse(records[i])));
        var waysToWin = races.Select(r => r.NumberOfWaysToBeatTheRecord());
        return waysToWin.Aggregate((a, b) => a * b).ToString();
    }

    public string SolvePartTwo(bool useSample = false)
    {
        var data = DataReader.GetData(6, 0, useSample).Split(Environment.NewLine);
        var time = data[0].Split(": ")[1].Replace(" ", "");
        var record = data[1].Split(": ")[1].Replace(" ", "");
        var race = new Race(long.Parse(time), long.Parse(record));
        return race.NumberOfWaysToBeatTheRecord().ToString();
    }

    private class Race(long time, long record)
    {
        public long Time { get; private set; } = time;
        public long Record { get; private set; } = record;

        public int NumberOfWaysToBeatTheRecord() =>
            EnumerableExtensions.Range(0, Time).Count(t => (Time - t) * t > Record);

        public override string ToString() => $"{Time} ms, {Record} mm";
    }
}

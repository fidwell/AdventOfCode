using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public partial class Puzzle14Solver : IPuzzleSolver
{
    const int RaceDuration = 2503;

    public string SolvePartOne(string input) =>
        GetReindeer(input)
        .Max(r => r.DistanceAfterFlying(RaceDuration))
        .ToString();

    public string SolvePartTwo(string input)
    {
        var reindeer = GetReindeer(input).ToList();
        for (var t = 1; t <= RaceDuration; t++)
        {
            var checkpoint = reindeer.Select(r => (Reindeer: r, Distance: r.DistanceAfterFlying(t)));
            var maxDistance = checkpoint.Max(c => c.Distance);
            var winningReindeer = checkpoint.Where(c => c.Distance == maxDistance);
            foreach (var r in winningReindeer)
                r.Reindeer.Score += 1;
        }
        return reindeer.Max(r => r.Score).ToString();
    }

    private static IEnumerable<Reindeer> GetReindeer(string input) =>
        input.SplitByNewline().Select(l =>
        {
            var numbers = Regexes.NonNegativeInteger().Matches(l);
            return new Reindeer
            {
                Speed = int.Parse(numbers[0].Value),
                FlightDuration = int.Parse(numbers[1].Value),
                RestDuration = int.Parse(numbers[2].Value)
            };
        });

    private class Reindeer
    {
        public int Speed { get; set; }
        public int FlightDuration { get; set; }
        public int RestDuration { get; set; }
        public int Score { get; set; }

        private int DistancePerFlight => FlightDuration * Speed;

        public int DistanceAfterFlying(int seconds)
        {
            var cycleTime = FlightDuration + RestDuration;
            var fullCycles = seconds / cycleTime;
            var remainder = seconds % cycleTime;
            var distanceOfFullFlights = fullCycles * DistancePerFlight;

            if (remainder >= FlightDuration)
                // We stop to measure while this reindeer is resting
                return distanceOfFullFlights + DistancePerFlight;

            // We stop to measure while this reindeer is flying
            return distanceOfFullFlights + remainder * Speed;
        }
    }
}

using AdventOfCode.Core.Ranges;
using AdventOfCode.Solvers;

namespace AdventOfCode.Core.PuzzleSolvers._2023;

public class Puzzle19Solver : IPuzzleSolver
{
    public string SolvePartOne(string input)
    {
        var workflows = new Dictionary<string, IEnumerable<WorkflowRule>>();
        var parts = new List<Part>();

        foreach (var line in input.Split(Environment.NewLine))
        {
            if (line.StartsWith('{'))
            {
                parts.Add(ParsePart(line));
            }
            else if (line.Length > 0)
            {
                var w = ParseWorkflow(line);
                workflows.Add(w.Item1, w.Item2);
            }
        }

        var acceptedPartSum = 0;
        var startingWorkflow = workflows["in"];
        foreach (var part in parts)
        {
            var workflow = startingWorkflow;
            while (true)
            {
                var result = ApplyWorkflow(part, workflow);
                if (result == "A")
                {
                    acceptedPartSum += part.Sum;
                    break;
                }
                else if (result == "R")
                {
                    break;
                }
                else
                {
                    workflow = workflows[result];
                }
            }
        }
        
        return acceptedPartSum.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var workflows = new Dictionary<string, IEnumerable<WorkflowRule>>();

        foreach (var line in input.Split(Environment.NewLine))
        {
            if (line.Length > 0 && line[0] != '{')
            {
                var w = ParseWorkflow(line);
                workflows.Add(w.Item1, w.Item2);
            }
        }

        var startingRange = new PartValueRange(new Dictionary<char, Range>
        {
            { 'x', new Range(1, 4001) },
            { 'm', new Range(1, 4001) },
            { 'a', new Range(1, 4001) },
            { 's', new Range(1, 4001) }
        });
        var unfinishedRanges = new Queue<RangeResult>();
        unfinishedRanges.Enqueue(new RangeResult("in", startingRange));

        var acceptedRanges = new List<PartValueRange>();
        while (unfinishedRanges.Count > 0)
        {
            var thisRange = unfinishedRanges.Dequeue();
            var resultingRanges = ApplyWorkflow(thisRange.PartValueRange, workflows[thisRange.ResultingWorkflow]).ToList();
            foreach (var range in resultingRanges)
            {
                if (range.ResultingWorkflow == "A")
                {
                    acceptedRanges.Add(range.PartValueRange);
                }
                else if (range.ResultingWorkflow != "R")
                {
                    unfinishedRanges.Enqueue(range);
                }
            }
        }

        return acceptedRanges.Sum(r => r.TotalValues).ToString();
    }

    private static (string, IEnumerable<WorkflowRule>) ParseWorkflow(string line)
    {
        var portions = line.Replace("}", "").Split('{');
        var name = portions[0];
        var rulesTest = portions[1];
        var rules = rulesTest.Split(",").Select(r =>
        {
            if (r.Contains(':'))
            {
                var parameter = r[0];
                var isLessThan = r[1] == '<';
                var colon = r.IndexOf(':');
                var amount = int.Parse(r.Substring(2, colon - 2));
                var target = r.Substring(r.IndexOf(':') + 1);
                return new ConditionalRule(parameter, isLessThan, amount, target);
            }
            else
            {
                return (WorkflowRule)new FinalRule(r);
            }
        });

        return (name, rules);
    }

    private static Part ParsePart(string line)
    {
        var portions = line.Substring(1, line.Length - 2).Split(',');
        var values = new Dictionary<char, int>();
        foreach (var p in portions)
        {
            values.Add(p[0], int.Parse(p.Substring(2)));
        }
        return new Part(values);
    }

    private static string ApplyWorkflow(Part part, IEnumerable<WorkflowRule> rules)
    {
        foreach (var rule in rules)
        {
            var result = rule.Apply(part);
            if (!string.IsNullOrEmpty(result))
                return result;
        }
        return string.Empty;
    }

    private static IEnumerable<RangeResult> ApplyWorkflow(PartValueRange partValueRange, IEnumerable<WorkflowRule> rules)
    {
        var ranges = new Queue<PartValueRange>();
        ranges.Enqueue(partValueRange);

        foreach (var rule in rules)
        {
            var thisRange = ranges.Dequeue();

            var results = rule.Apply(thisRange).ToList();
            foreach (var result in results)
            {
                if (!string.IsNullOrWhiteSpace(result.ResultingWorkflow))
                {
                    yield return result;
                }
                else
                {
                    ranges.Enqueue(result.PartValueRange);
                }
            }
        }
    }

    #region Classes

    private abstract class WorkflowRule(string target)
    {
        public readonly string Target = target;
        public abstract string Apply(Part part);
        public abstract IEnumerable<RangeResult> Apply(PartValueRange partValueRange);
    }

    private class ConditionalRule(char parameter, bool isLessThan, int amount, string target)
        : WorkflowRule(target)
    {
        public readonly char Parameter = parameter;
        public readonly bool IsLessThan = isLessThan;
        public readonly int Amount = amount;

        public override string Apply(Part part)
        {
            var rating = part.Ratings[Parameter];
            if (IsLessThan && rating < Amount)
                return Target;
            if (!IsLessThan && rating > Amount)
                return Target;
            return string.Empty;
        }

        public override IEnumerable<RangeResult> Apply(PartValueRange partValueRange)
        {
            var rangeInQuestion = partValueRange.RatingRanges[Parameter];
            var unaffectedRatings = partValueRange.RatingRanges.Where(r => r.Key != Parameter);

            var lowerRangeRatings = unaffectedRatings.ToDictionary(e => e.Key, e => e.Value);
            var lowerRatingRange = IsLessThan
                ? new Range(rangeInQuestion.Start, Amount)
                : new Range(rangeInQuestion.Start, Amount + 1);
            lowerRangeRatings.Add(Parameter, lowerRatingRange);
            yield return new RangeResult(
                IsLessThan ? Target : string.Empty,
                new PartValueRange(lowerRangeRatings));

            var higherRangeRatings = unaffectedRatings.ToDictionary(e => e.Key, e => e.Value);
            var higherRatingRange = IsLessThan
                ? new Range(Amount, rangeInQuestion.End)
                : new Range(Amount + 1, rangeInQuestion.End);
            higherRangeRatings.Add(Parameter, higherRatingRange);
            yield return new RangeResult(
                IsLessThan ? string.Empty : Target,
                new PartValueRange(higherRangeRatings));
        }
    }

    private class FinalRule(string target)
        : WorkflowRule(target)
    {
        public override string Apply(Part part) => Target;
        public override IEnumerable<RangeResult> Apply(PartValueRange partValueRange) => [new(Target, partValueRange)];
    }

    private class Part(Dictionary<char, int> values)
    {
        public readonly Dictionary<char, int> Ratings = values;
        public int Sum => Ratings.Sum(r => r.Value);
    }

    private class PartValueRange(Dictionary<char, Range> ratingRanges)
    {
        public readonly Dictionary<char, Range> RatingRanges = ratingRanges;
        public long TotalValues => RatingRanges.Aggregate(1L, (a, b) => a * b.Value.Length());
    }

    private class RangeResult(string resultingWorkflow, PartValueRange partValueRange)
    {
        public string ResultingWorkflow = resultingWorkflow;
        public PartValueRange PartValueRange = partValueRange;
    }

    #endregion
}

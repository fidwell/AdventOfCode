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

    public string SolvePartTwo(string input) => throw new NotImplementedException();

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

    private abstract class WorkflowRule(string target)
    {
        public readonly string Target = target;
        public abstract string Apply(Part part);
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
    }

    private class FinalRule(string target)
        : WorkflowRule(target)
    {
        public override string Apply(Part part) => Target;
    }

    private class Part(Dictionary<char, int> values)
    {
        public readonly Dictionary<char, int> Ratings = values;
        public int Sum => Ratings.Sum(r => r.Value);
    }
}

﻿using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle21Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, 2);
    public override string SolvePartTwo(string input) => Solve(input, 25);

    private static readonly char[] Activate = ['A'];
    private static string Solve(string input, int layers)
    {
        var dirButtons = new[] { '>', 'v', '<', '^', 'A' };
        var dirPairs = dirButtons.SelectMany(a => dirButtons.Select(b => (a, b)));

        // Start with "my" dir pad (pad 0).
        var dirPadCosts = dirPairs.ToDictionary(pair => pair, pair => 1UL);

        for (var i = 0; i < layers; i++)
        {
            // To find the cost of some path on for robot 2,
            // find the path from X->Y. Then sum the costs
            // for each step of that path from robot 1.

            // I want to know the length of the shortest paths
            // to go from resting on button X to pressing button Y.
            dirPadCosts = dirPairs.ToDictionary(pair => pair, pair =>
            {
                var path = Activate.Concat(DirpadShortestPaths[pair]).Concat(Activate);
                var steps = path.Zip(path.Skip(1));
                var costs = steps.Select(step => dirPadCosts[step]);
                return costs.Aggregate(0UL, (sum, cost) => sum + cost);
            });
        }

        var numButtons = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A' };
        var numPairs = numButtons.SelectMany(a => numButtons.Select(b => (a, b)));

        var keyPad = numPairs.ToDictionary(pair => pair, pair =>
        {
            var path = Activate.Concat(NumpadShortestPaths[pair]).Concat(Activate);
            var steps = path.Zip(path.Skip(1));
            var costs = steps.Select(step => dirPadCosts[step]);
            return costs.Aggregate(0UL, (sum, cost) => sum + cost);
        });

        var codes = input.SplitByNewline();
        var answer = 0UL;
        foreach (var code in codes)
        {
            var path = Activate.Concat(code.ToCharArray());
            var steps = path.Zip(path.Skip(1));
            var costs = steps.Select(step => keyPad[step]);
            var total = costs.Aggregate(0UL, (sum, cost) => sum + cost);

            var numPart = ulong.Parse(Regexes.NonNegativeInteger().Match(code).Value);
            var complexity = total * numPart;
            answer += complexity;
        }

        return answer.ToString();
    }

    private static readonly Dictionary<(char, char), IEnumerable<char>> NumpadShortestPaths = new()
    {
        { ('7', '7'), [] },
        { ('7', '8'), ['>'] },
        { ('7', '9'), ['>', '>'] },
        { ('7', '4'), ['v'] },
        { ('7', '5'), ['v', '>'] },
        { ('7', '6'), ['v', '>', '>'] },
        { ('7', '1'), ['v', 'v'] },
        { ('7', '2'), ['v', 'v', '>'] },
        { ('7', '3'), ['>', '>', 'v', 'v'] },
        { ('7', '0'), ['>', 'v', 'v', 'v'] },
        { ('7', 'A'), ['v', 'v', 'v', '>', '>'] },

        { ('8', '7'), ['<'] },
        { ('8', '8'), [] },
        { ('8', '9'), ['>'] },
        { ('8', '4'), ['<', 'v'] },
        { ('8', '5'), ['v'] },
        { ('8', '6'), ['>', 'v'] },
        { ('8', '1'), ['<', 'v', 'v'] },
        { ('8', '2'), ['v', 'v'] },
        { ('8', '3'), ['v', 'v', '>'] },
        { ('8', '0'), ['v', 'v', 'v'] },
        { ('8', 'A'), ['>', 'v', 'v', 'v'] },

        { ('9', '7'), ['<', '<'] },
        { ('9', '8'), ['<'] },
        { ('9', '9'), [] },
        { ('9', '4'), ['<', '<', 'v'] },
        { ('9', '5'), ['<', 'v'] },
        { ('9', '6'), ['v'] },
        { ('9', '1'), ['<', '<', 'v', 'v'] },
        { ('9', '2'), ['<', 'v', 'v'] },
        { ('9', '3'), ['v', 'v'] },
        { ('9', '0'), ['<', 'v', 'v', 'v'] },
        { ('9', 'A'), ['v', 'v', 'v'] },

        { ('4', '7'), ['^'] },
        { ('4', '8'), ['>', '^'] },
        { ('4', '9'), ['>', '>', '^'] },
        { ('4', '4'), [] },
        { ('4', '5'), ['>'] },
        { ('4', '6'), ['>', '>'] },
        { ('4', '1'), ['v'] },
        { ('4', '2'), ['v', '>'] },
        { ('4', '3'), ['v', '>', '>'] },
        { ('4', '0'), ['>', 'v', 'v'] },
        { ('4', 'A'), ['>', '>', 'v', 'v'] },

        { ('5', '7'), ['<', '^'] },
        { ('5', '8'), ['^'] },
        { ('5', '9'), ['>', '^'] },
        { ('5', '4'), ['<'] },
        { ('5', '5'), [] },
        { ('5', '6'), ['>'] },
        { ('5', '1'), ['<', 'v'] },
        { ('5', '2'), ['v'] },
        { ('5', '3'), ['>', 'v'] },
        { ('5', '0'), ['v', 'v'] },
        { ('5', 'A'), ['v', 'v', '>'] },

        { ('6', '7'), ['<', '<', '^'] },
        { ('6', '8'), ['<', '^'] },
        { ('6', '9'), ['^'] },
        { ('6', '4'), ['<', '<'] },
        { ('6', '5'), ['<'] },
        { ('6', '6'), [] },
        { ('6', '1'), ['<', '<', 'v'] },
        { ('6', '2'), ['<', 'v'] },
        { ('6', '3'), ['v'] },
        { ('6', '0'), ['<', 'v', 'v'] },
        { ('6', 'A'), ['v', 'v'] },

        { ('1', '7'), ['^', '^'] },
        { ('1', '8'), ['>', '^', '^'] },
        { ('1', '9'), ['>', '>', '^', '^'] },
        { ('1', '4'), ['^'] },
        { ('1', '5'), ['>', '^'] },
        { ('1', '6'), ['>', '>', '^'] },
        { ('1', '1'), [] },
        { ('1', '2'), ['>'] },
        { ('1', '3'), ['>', '>'] },
        { ('1', '0'), ['>', 'v'] },
        { ('1', 'A'), ['>', '>', 'v'] },

        { ('2', '7'), ['<', '^', '^'] },
        { ('2', '8'), ['^', '^'] },
        { ('2', '9'), ['>', '^', '^'] },
        { ('2', '4'), ['<', '^'] },
        { ('2', '5'), ['^'] },
        { ('2', '6'), ['>', '^'] },
        { ('2', '1'), ['<'] },
        { ('2', '2'), [] },
        { ('2', '3'), ['>'] },
        { ('2', '0'), ['v'] },
        { ('2', 'A'), ['>', 'v'] },

        { ('3', '7'), ['<', '<', '^', '^'] },
        { ('3', '8'), ['^', '^', '<'] },
        { ('3', '9'), ['^', '^'] },
        { ('3', '4'), ['<', '<', '^'] },
        { ('3', '5'), ['<', '^'] },
        { ('3', '6'), ['^'] },
        { ('3', '1'), ['<', '<'] },
        { ('3', '2'), ['<'] },
        { ('3', '3'), [] },
        { ('3', '0'), ['<', 'v'] },
        { ('3', 'A'), ['v'] },

        { ('0', '7'), ['^', '^', '^', '<'] },
        { ('0', '8'), ['^', '^', '^'] },
        { ('0', '9'), ['^', '^', '^', '>'] },
        { ('0', '4'), ['^', '^', '<'] },
        { ('0', '5'), ['^', '^'] },
        { ('0', '6'), ['^', '^', '>'] },
        { ('0', '1'), ['^', '<'] },
        { ('0', '2'), ['^'] },
        { ('0', '3'), ['^', '>'] },
        { ('0', '0'), [] },
        { ('0', 'A'), ['>'] },

        { ('A', '7'), ['^', '^', '^', '<', '<'] },
        { ('A', '8'), ['<', '^', '^', '^'] },
        { ('A', '9'), ['^', '^', '^'] },
        { ('A', '4'), ['^', '^', '<', '<'] },
        { ('A', '5'), ['<', '^', '^'] },
        { ('A', '6'), ['^', '^'] },
        { ('A', '1'), ['^', '<', '<'] },
        { ('A', '2'), ['<', '^'] },
        { ('A', '3'), ['^'] },
        { ('A', '0'), ['<'] },
        { ('A', 'A'), [] }
    };

    private static readonly Dictionary<(char, char), IEnumerable<char>> DirpadShortestPaths = new()
    {
        { ('^', '^'), [] },
        { ('^', 'A'), ['>'] },
        { ('^', '<'), ['v', '<'] },
        { ('^', 'v'), ['v'] },
        { ('^', '>'), ['v', '>'] },

        { ('A', '^'), ['<'] },
        { ('A', 'A'), [] },
        { ('A', '<'), ['v', '<', '<'] },
        { ('A', 'v'), ['<', 'v'] },
        { ('A', '>'), ['v'] },

        { ('<', '^'), ['>', '^'] },
        { ('<', 'A'), ['>', '>', '^'] },
        { ('<', '<'), [] },
        { ('<', 'v'), ['>'] },
        { ('<', '>'), ['>', '>'] },

        { ('v', '^'), ['^'] },
        { ('v', 'A'), ['>', '^'] },
        { ('v', '<'), ['<'] },
        { ('v', 'v'), [] },
        { ('v', '>'), ['>'] },

        { ('>', '^'), ['<', '^'] },
        { ('>', 'A'), ['^'] },
        { ('>', '<'), ['<', '<'] },
        { ('>', 'v'), ['<'] },
        { ('>', '>'), [] }
    };
}

﻿using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2024;

public class Puzzle21Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var answer = 0;
        var codes = input.SplitByNewline();
        //List<string> codes = ["379A"];
        foreach (var code in codes)
        {
            var numPart = int.Parse(Regexes.NonNegativeInteger().Match(code).Value);
            var myDirections = GetDirections(code);
            var complexity = myDirections.Length * numPart;
            Console.WriteLine($"Complexity: {myDirections.Length} * {numPart} = {complexity}");
            answer += complexity;
        }
        return answer.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static string GetDirections(string code)
    {
        Console.WriteLine(code);
        var layer1 = GetNumpadDirections(code);
        Console.WriteLine(layer1);
        var layer2 = GetDirpadDirections(layer1);
        Console.WriteLine(layer2);
        var layer3 = GetDirpadDirections(layer2);
        Console.WriteLine(layer3);
        return layer3;
    }

    private static readonly Dictionary<(char, char), IEnumerable<char>> NumpadShortestPaths = new()
    {
        { ('7', '7'), [] },
        { ('7', '8'), ['>'] },
        { ('7', '9'), ['>', '>'] },
        { ('7', '4'), ['v'] },
        { ('7', '5'), ['>', 'v'] },
        { ('7', '6'), ['>', '>', 'v'] },
        { ('7', '1'), ['v', 'v'] },
        { ('7', '2'), ['>', 'v', 'v'] },
        { ('7', '3'), ['>', '>', 'v', 'v'] },
        { ('7', '0'), ['>', 'v', 'v', 'v'] },
        { ('7', 'A'), ['>', '>', 'v', 'v', 'v'] },

        { ('8', '7'), ['<'] },
        { ('8', '8'), [] },
        { ('8', '9'), ['>'] },
        { ('8', '4'), ['v', '<'] },
        { ('8', '5'), ['v'] },
        { ('8', '6'), ['>', 'v'] },
        { ('8', '1'), ['v', 'v', '<'] },
        { ('8', '2'), ['v', 'v'] },
        { ('8', '3'), ['>', 'v', 'v'] },
        { ('8', '0'), ['v', 'v', 'v'] },
        { ('8', 'A'), ['>', 'v', 'v', 'v'] },

        { ('9', '7'), ['<', '<'] },
        { ('9', '8'), ['<'] },
        { ('9', '9'), [] },
        { ('9', '4'), ['v', '<', '<'] },
        { ('9', '5'), ['v', '<'] },
        { ('9', '6'), ['v'] },
        { ('9', '1'), ['v', 'v', '<', '<'] },
        { ('9', '2'), ['v', 'v', '<'] },
        { ('9', '3'), ['v', 'v'] },
        { ('9', '0'), ['v', 'v', 'v', '<'] },
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

        { ('5', '7'), ['^', '<'] },
        { ('5', '8'), ['^'] },
        { ('5', '9'), ['^', '>'] },
        { ('5', '4'), ['<'] },
        { ('5', '5'), [] },
        { ('5', '6'), ['>'] },
        { ('5', '1'), ['v', '<'] },
        { ('5', '2'), ['v'] },
        { ('5', '3'), ['v', '>'] },
        { ('5', '0'), ['v', 'v'] },
        { ('5', 'A'), ['>', 'v', 'v'] },

        { ('6', '7'), ['^', '<', '<'] },
        { ('6', '8'), ['^', '<'] },
        { ('6', '9'), ['^'] },
        { ('6', '4'), ['<', '<'] },
        { ('6', '5'), ['<'] },
        { ('6', '6'), [] },
        { ('6', '1'), ['v', '<', '<'] },
        { ('6', '2'), ['v', '<'] },
        { ('6', '3'), ['v'] },
        { ('6', '0'), ['v', 'v', '<'] },
        { ('6', 'A'), ['v', 'v'] },

        { ('1', '7'), ['^', '^'] },
        { ('1', '8'), ['^', '^', '>'] },
        { ('1', '9'), ['^', '^', '>', '>'] },
        { ('1', '4'), ['^'] },
        { ('1', '5'), ['>', '^'] },
        { ('1', '6'), ['^', '>', '>'] },
        { ('1', '1'), [] },
        { ('1', '2'), ['>'] },
        { ('1', '3'), ['>', '>'] },
        { ('1', '0'), ['>', 'v'] },
        { ('1', 'A'), ['>', '>', 'v'] },

        { ('2', '7'), ['^', '^', '<'] },
        { ('2', '8'), ['^', '^'] },
        { ('2', '9'), ['>', '^', '^'] },
        { ('2', '4'), ['^', '<'] },
        { ('2', '5'), ['^'] },
        { ('2', '6'), ['^', '>'] },
        { ('2', '1'), ['<'] },
        { ('2', '2'), [] },
        { ('2', '3'), ['>'] },
        { ('2', '0'), ['v'] },
        { ('2', 'A'), ['>', 'v'] },

        { ('3', '7'), ['<', '<', '^', '^'] },
        { ('3', '8'), ['^', '^', '<'] },
        { ('3', '9'), ['^', '^'] },
        { ('3', '4'), ['^', '<', '<'] },
        { ('3', '5'), ['^', '<'] },
        { ('3', '6'), ['^'] },
        { ('3', '1'), ['<', '<'] },
        { ('3', '2'), ['<'] },
        { ('3', '3'), [] },
        { ('3', '0'), ['v', '<'] },
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
        { ('A', '8'), ['^', '^', '^', '<'] },
        { ('A', '9'), ['^', '^', '^'] },
        { ('A', '4'), ['^', '^', '<', '<'] },
        { ('A', '5'), ['^', '^', '<'] },
        { ('A', '6'), ['^', '^'] },
        { ('A', '1'), ['^', '<', '<'] },
        { ('A', '2'), ['^', '<'] },
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

    private static string GetNumpadDirections(string code)
    {
        IEnumerable<char> result = NumpadShortestPaths[('A', code[0])].Concat(['A']);
        for (var i = 0; i < code.Length - 1; i++)
        {
            var nextPath = NumpadShortestPaths[(code[i], code[i + 1])];
            result = result.Concat(nextPath).Concat(['A']);
        }
        return new string(result.ToArray());
    }

    private static string GetDirpadDirections(string code)
    {
        IEnumerable<char> result = DirpadShortestPaths[('A', code[0])].Concat(['A']);
        for (var i = 0; i < code.Length - 1; i++)
        {
            var nextPath = DirpadShortestPaths[(code[i], code[i + 1])];
            result = result.Concat(nextPath).Concat(['A']);
        }
        return new string(result.ToArray());
    }
}

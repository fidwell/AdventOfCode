using AdventOfCode.Core.StringUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class NumberWordConverterTests
{
    [DataTestMethod]
    [DataRow("two1nine", 2, 9)]
    [DataRow("eightwothree", 8, 3)]
    [DataRow("abcone2threexyz", 1, 3)]
    [DataRow("xtwone3four", 2, 4)]
    [DataRow("4nineeightseven2", 4, 2)]
    [DataRow("zoneight234", 1, 4)]
    [DataRow("7pqrstsixteen", 7, 6)]
    [DataRow("eightwo", 8, 2)]
    public void Puzzle02Sample(string input, int first, int last)
    {
        var result = DigitWordFinder.FindWordDigits(input);
        Assert.AreEqual(first, result.First());
        Assert.AreEqual(last, result.Last());
    }
}

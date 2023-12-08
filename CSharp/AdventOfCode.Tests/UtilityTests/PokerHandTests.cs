using AdventOfCode.Core.Poker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode.Tests.UtilityTests;

[TestClass]
public class PokerHandTests
{
    [DataTestMethod]
    [DataRow("AAAAA", PokerHandType.FiveOfAKind)]
    [DataRow("AA7AA", PokerHandType.FourOfAKind)]
    [DataRow("AAAA7", PokerHandType.FourOfAKind)]
    [DataRow("7AAAA", PokerHandType.FourOfAKind)]
    [DataRow("44999", PokerHandType.FullHouse)]
    [DataRow("84848", PokerHandType.FullHouse)]
    [DataRow("33237", PokerHandType.ThreeOfAKind)]
    [DataRow("93332", PokerHandType.ThreeOfAKind)]
    [DataRow("27274", PokerHandType.TwoPair)]
    [DataRow("44822", PokerHandType.TwoPair)]
    [DataRow("46223", PokerHandType.OnePair)]
    [DataRow("84398", PokerHandType.OnePair)]
    [DataRow("34567", PokerHandType.HighCard)]
    public void PokerHandTypeTest(string input, PokerHandType expected)
    {
        var hand = new PokerHand(input);
        Assert.AreEqual(expected, hand.Type);
    }

    [DataTestMethod]
    [DataRow("AAAAA", PokerHandType.FiveOfAKind)]
    [DataRow("AA7AA", PokerHandType.FourOfAKind)]
    [DataRow("AAAA7", PokerHandType.FourOfAKind)]
    [DataRow("7AAAA", PokerHandType.FourOfAKind)]
    [DataRow("44999", PokerHandType.FullHouse)]
    [DataRow("84848", PokerHandType.FullHouse)]
    [DataRow("33237", PokerHandType.ThreeOfAKind)]
    [DataRow("93332", PokerHandType.ThreeOfAKind)]
    [DataRow("27274", PokerHandType.TwoPair)]
    [DataRow("44822", PokerHandType.TwoPair)]
    [DataRow("46223", PokerHandType.OnePair)]
    [DataRow("84398", PokerHandType.OnePair)]
    [DataRow("34567", PokerHandType.HighCard)]

    [DataRow("JJJJJ", PokerHandType.FiveOfAKind)]
    [DataRow("AJAAA", PokerHandType.FiveOfAKind)]
    [DataRow("AA7JA", PokerHandType.FourOfAKind)]
    [DataRow("AJAA7", PokerHandType.FourOfAKind)]
    [DataRow("7AAAJ", PokerHandType.FourOfAKind)]
    [DataRow("44J99", PokerHandType.FullHouse)]
    [DataRow("84J48", PokerHandType.FullHouse)]
    [DataRow("3J237", PokerHandType.ThreeOfAKind)]
    [DataRow("933J2", PokerHandType.ThreeOfAKind)]
    [DataRow("462J3", PokerHandType.OnePair)]
    [DataRow("8439J", PokerHandType.OnePair)]

    [DataRow("T55J5", PokerHandType.FourOfAKind)]
    [DataRow("KTJJT", PokerHandType.FourOfAKind)]
    [DataRow("QQQJA", PokerHandType.FourOfAKind)]
    public void PokerHandTypeTestWithJoker(string input, PokerHandType expected)
    {
        var hand = new PokerHand(input, true);
        Assert.AreEqual(expected, hand.Type);
    }

    [DataTestMethod]
    [DataRow("JKKK2", "QQQQ2", -1)]
    public void PokerHandComparisonTest(string input1, string input2, int expected)
    {
        var hand1 = new PokerHand(input1, true);
        var hand2 = new PokerHand(input2, true);
        Assert.AreEqual(expected, hand1.CompareTo(hand2));
    }
}

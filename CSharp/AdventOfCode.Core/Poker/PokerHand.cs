namespace AdventOfCode.Core.Poker;

public class PokerHand : IComparable<PokerHand>
{
    public string Input { get; private set; }

    public PokerHandType Type { get; private set; }

    public PokerHand(string input, bool jIsJoker = false)
    {
        // If we replace characters like this,
        // we can do simple char value comparisons
        // 2 3 4 5 6 7 8 9 T J Q K A
        // 2 3 4 5 6 7 8 9 B C D E F
        var jokerValue = jIsJoker ? '1' : 'C';
        Input = input
            .Replace('T', 'B')
            .Replace('J', jokerValue)
            .Replace('Q', 'D')
            .Replace('K', 'E')
            .Replace('A', 'F');

        Type = jIsJoker
            ? Values.Select(v => FindType(Input.Replace(jokerValue, v))).Max()
            : FindType(Input);
    }

    public int CompareTo(PokerHand? other)
    {
        if (other is null)
            return 0;

        if (Type < other.Type)
            return -1;

        if (Type > other.Type)
            return 1;

        for (var i = 0; i < Input.Length; i++)
        {
            if (Input[i] < other.Input[i])
                return -1;

            if (Input[i] > other.Input[i])
                return 1;
        }

        return 0;
    }

    private static char[] Values => new[] { '2', '3', '4', '5', '6', '7', '8', '9', 'B', 'D', 'E', 'F' };

    private static PokerHandType FindType(string input)
    {
        var labelGroups = input.ToCharArray().GroupBy(c => c);

        if (labelGroups.Count() == 1)
            return PokerHandType.FiveOfAKind;
        if (labelGroups.Count() == 4)
            return PokerHandType.OnePair;
        if (labelGroups.Count() == 5)
            return PokerHandType.HighCard;

        var groupSizes = labelGroups.Select(g => g.Count());
        if (labelGroups.Count() == 3)
        {
            return groupSizes.Any(g => g == 3)
                ? PokerHandType.ThreeOfAKind
                : PokerHandType.TwoPair;
        }

        if (labelGroups.Count() == 2)
        {
            return groupSizes.Any(g => g == 3)
                ? PokerHandType.FullHouse
                : PokerHandType.FourOfAKind;
        }

        throw new Exception($"I can't tell what kind of hand {input} is");
    }
}

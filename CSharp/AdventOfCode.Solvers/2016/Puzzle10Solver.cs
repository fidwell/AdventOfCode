using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public class Puzzle10Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var (bots, _) = Run(input);
        var isExample = bots.Count < 10;
        var targetHigh = isExample ? 5 : 61;
        var targetLow = isExample ? 2 : 17;
        var answerBot = bots.Single(b => b.Value.Low == targetLow && b.Value.High == targetHigh);
        return answerBot.Key.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        var (_, outputs) = Run(input);
        return (outputs[0] * outputs[1] * outputs[2]).ToString();
    }

    private static (Dictionary<int, Bot>, Dictionary<int, int>) Run(string input)
    {
        var directions = new Queue<string>(input.SplitByNewline());

        var bots = new Dictionary<int, Bot>();
        var outputs = new Dictionary<int, int>();

        while (directions.Count != 0)
        {
            var direction = directions.Dequeue();

            var words = direction.Split(' ');

            if (words[0] == "value")
            {
                var value = int.Parse(words[1]);
                var botId = int.Parse(words[5]);
                bots.TryAdd(botId, new Bot());
                bots[botId].ReceiveValue(value);
            }
            else if (words[0] == "bot")
            {
                var botId = int.Parse(words[1]);

                var lowTargetType = words[5];
                var lowTargetId = int.Parse(words[6]);
                var highTargetType = words[10];
                var highTargetId = int.Parse(words[11]);

                bots.TryAdd(botId, new Bot());

                var bot = bots[botId];
                bot.LowTargetType = lowTargetType;
                bot.LowTargetId = lowTargetId;
                bot.HighTargetType = highTargetType;
                bot.HighTargetId = highTargetId;

                if (bot.CanAct)
                {
                    if (bot.LowTargetType == "bot")
                    {
                        bots.TryAdd(bot.LowTargetId.Value, new Bot());
                        var lowBot = bots[lowTargetId];
                        lowBot.ReceiveValue(bot.Low!.Value);
                    }
                    else
                    {
                        outputs.TryAdd(bot.LowTargetId!.Value, bot.Low!.Value);
                    }

                    if (bot.HighTargetType == "bot")
                    {
                        bots.TryAdd(bot.HighTargetId.Value, new Bot());
                        var highBot = bots[highTargetId];
                        highBot.ReceiveValue(bot.High!.Value);
                    }
                    else
                    {
                        outputs.TryAdd(bot.HighTargetId!.Value, bot.High!.Value);
                    }
                }
                else
                {
                    directions.Enqueue(direction);
                }
            }
        }

        return (bots, outputs);
    }

    private class Bot
    {
        public int? Low;
        public int? High;

        public bool CanAct => Low.HasValue && High.HasValue;

        public string? LowTargetType;
        public int? LowTargetId;

        public string? HighTargetType;
        public int? HighTargetId;

        public void ReceiveValue(int value)
        {
            if (!Low.HasValue)
            {
                Low = value;
            }
            else if (!High.HasValue)
            {
                if (value < Low)
                {
                    High = Low;
                    Low = value;
                }
                else
                {
                    High = value;
                }
            }
        }
    }
}

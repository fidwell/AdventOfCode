using System.Text.RegularExpressions;
using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2016;

public partial class Puzzle04Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
        => RealRooms(input).Sum(r => r.Sector).ToString();

    public override string SolvePartTwo(string input)
        => RealRooms(input).FirstOrDefault(r => r.IsNorthpoleObjectStorage).Sector.ToString();

    private static IEnumerable<Room> RealRooms(string input)
        => input.SplitByNewline().Select(l => new Room(l)).Where(r => r.IsReal);

    private record struct Room
    {
        public string Name { get; set; }
        public int Sector { get; set; }
        public string Checksum { get; set; }

        public Room(string input)
        {
            var match = RoomDefinition().Match(input);
            Name = match.Groups[1].Value;
            Sector = int.Parse(match.Groups[3].Value);
            Checksum = match.Groups[4].Value;
        }

        public bool IsReal
        {
            get
            {
                var charsInName = Name.GroupBy(n => n)
                    .Where(g => g.Key != '-')
                    .OrderByDescending(g => g.Count())
                    .ThenBy(g => g.Key)
                    .Select(g => g.Key)
                    .Take(5)
                    .ToArray();
                return new string(charsInName).Equals(Checksum);
            }
        }

        public bool IsNorthpoleObjectStorage
        {
            get
            {
                const string target = "northpole object storage ";

                if (Name.Length != target.Length)
                    return false;

                var rotationAmount = Sector % 26;

                for (var i = 0; i < Name.Length; i++)
                {
                    var targetChar = target[i];
                    var transformedChar = TransformChar(Name[i], rotationAmount);
                    if (targetChar != transformedChar)
                        return false;
                }

                return true;
            }
        }

        private static char TransformChar(char input, int rotationAmount)
        {
            if (input == '-')
                return ' ';

            var newChar = input + rotationAmount;
            if (newChar > 'z') newChar -= 26;
            return (char)newChar;
        }
    }

    [GeneratedRegex(@"((\w+\-)+)(\d+)\[(\w+)\]")]
    private static partial Regex RoomDefinition();
}

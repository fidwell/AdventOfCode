using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle15Solver : PuzzleSolver
{
    const int Target = 100;

    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    private static string Solve(string input, bool includeCalories)
    {
        var ingredients = input.SplitByNewline().Select(l => new Ingredient(l)).ToList();

        var max = 0;
        for (var i0 = 0; i0 <= Target; i0++)
        {
            if (ingredients.Count == 2)
            {
                IEnumerable<(Ingredient, int)> recipe = [
                    (ingredients[0], i0),
                    (ingredients[1], Target - i0),
                    ];

                if (includeCalories)
                {
                    var calories = recipe.Sum(i => i.Item1.Calories * i.Item2);
                    if (calories != 500)
                        continue;
                }

                var score = CookieScore(recipe);
                if (score > max)
                    max = score;
            }
            else
            {
                for (var i1 = 0; i1 <= Target - i0; i1++)
                {
                    for (var i2 = 0; i2 <= Target - i0 - i1; i2++)
                    {
                        IEnumerable<(Ingredient, int)> recipe = [
                            (ingredients[0], i0),
                            (ingredients[1], i1),
                            (ingredients[2], i2),
                            (ingredients[3], Target - (i0 + i1 + i2)),
                            ];

                        if (includeCalories)
                        {
                            var calories = recipe.Sum(i => i.Item1.Calories * i.Item2);
                            if (calories != 500)
                                continue;
                        }

                        var score = CookieScore(recipe);
                        if (score > max)
                            max = score;
                    }
                }
            }
        }

        return max.ToString();
    }

    private static int CookieScore(IEnumerable<(Ingredient, int)> ingredients)
    {
        var capacity = ingredients.Sum(i => i.Item1.Capacity * i.Item2);
        var durability = ingredients.Sum(i => i.Item1.Durability * i.Item2);
        var flavor = ingredients.Sum(i => i.Item1.Flavor * i.Item2);
        var texture = ingredients.Sum(i => i.Item1.Texture * i.Item2);
        return
            (capacity > 0 ? capacity : 0) *
            (durability > 0 ? durability : 0) *
            (flavor > 0 ? flavor : 0) *
            (texture > 0 ? texture : 0);
    }

    private class Ingredient
    {
        public string Name { get; }
        public int Capacity { get; }
        public int Durability { get; }
        public int Flavor { get; }
        public int Texture { get; }
        public int Calories { get; }

        public Ingredient(string input)
        {
            var parts = input.Split(' ');
            Name = parts[0][..^1];
            Capacity = int.Parse(parts[2][..^1]);
            Durability = int.Parse(parts[4][..^1]);
            Flavor = int.Parse(parts[6][..^1]);
            Texture = int.Parse(parts[8][..^1]);
            Calories = int.Parse(parts[10]);
        }
    }
}

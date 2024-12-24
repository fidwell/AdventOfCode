using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle21Solver : PuzzleSolver
{
    public override string SolvePartOne(string input)
    {
        var stats = input.SplitByNewline()
            .Select(l => int.Parse(Regexes.NonNegativeInteger().Match(l).Value)).ToArray();
        var bossHp = stats[0];
        var bossDamage = stats[1];
        var bossArmor = stats[2];

        var myHp = 100;
        var myDamage = 0;
        var myArmor = 0;

        var didIWin = SimulateBattle(bossHp, bossDamage, bossArmor,
            myHp, myDamage, myArmor);

        var weapons = new List<Item>
        {
            new (8, 4, 0),
            new (10, 5, 0),
            new (25, 6, 0),
            new (40, 7, 0),
            new (74, 8, 0)
        };

        var armors = new List<Item>
        {
            new (0, 0, 0),
            new (13, 0, 1),
            new (31, 0, 2),
            new (53, 0, 3),
            new (75, 0, 4),
            new (102, 0, 5)
        };

        var rings = new List<Item>
        {
            new (0, 0, 0),
            new (0, 0, 0),
            new (25, 1, 0),
            new (50, 2, 0),
            new (100, 3, 0),
            new (20, 0, 1),
            new (40, 0, 2),
            new (80, 0, 3),
        };

        var lowestCost = int.MaxValue;
        for (var w = 0; w < weapons.Count; w++)
        {
            var weapon = weapons[w];
            for (var a = 0; a < armors.Count; a++)
            {
                var armor = armors[a];

                for (var r1 = 0; r1 < rings.Count; r1++)
                {
                    var ring1 = rings[r1];
                    for (var r2 = r1 + 1; r2 < rings.Count; r2++)
                    {
                        var ring2 = rings[r2];

                        var totalCost = weapon.Cost + armor.Cost + ring1.Cost + ring2.Cost;
                        var totalDamage = weapon.Damage + ring1.Damage + ring2.Damage;
                        var totalArmor = armor.Armor + ring1.Armor + ring2.Armor;

                        if (totalCost < lowestCost &&
                            SimulateBattle(
                            bossHp, bossDamage, bossArmor,
                            myHp, totalDamage, totalArmor))
                        {
                            lowestCost = totalCost;
                        }
                    }
                }
            }
        }

        return lowestCost.ToString();
    }

    public override string SolvePartTwo(string input)
    {
        throw new NotImplementedException();
    }

    private static bool SimulateBattle(
        int bossHp, int bossDamage, int bossArmor,
        int myHp, int myDamage, int myArmor)
    {
        // return true if you win
        var myTurn = true;
        while (bossHp > 0 && myHp > 0)
        {
            if (myTurn)
            {
                var damageDealt = myDamage - bossArmor;
                if (damageDealt < 1) damageDealt = 1;
                bossHp -= damageDealt;
            }
            else
            {
                var damageDealt = bossDamage - myArmor;
                if (damageDealt < 1) damageDealt = 1;
                myHp -= damageDealt;
            }

            myTurn = !myTurn;
        }

        return myHp > 0;
    }

    private readonly record struct Item(int Cost, int Damage, int Armor);
}

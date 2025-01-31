﻿using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2015;

public class Puzzle22Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) => Solve(input, false);
    public override string SolvePartTwo(string input) => Solve(input, true);

    private string Solve(string input, bool isHardMode)
    {
        var lines = input.SplitByNewline().ToArray();
        var bossHp = int.Parse(Regexes.NonNegativeInteger().Match(lines[0]).Value);
        var bossDamage = int.Parse(Regexes.NonNegativeInteger().Match(lines[1]).Value);

        var isExample = bossHp == 13 && bossDamage == 8;
        var playerHp = isExample ? 10 : 50;
        var mana = isExample ? 250 : 500;

        var queue = new PriorityQueue<GameState, int>();
        var startingState = new GameState(
            IsPlayerTurn: true,
            PlayerHp: playerHp,
            Mana: mana,
            BossHp: bossHp,
            ShieldTimer: 0,
            PoisonTimer: 0,
            RechargeTimer: 0,
            EndState: EndState.NoWinnerYet,
            TotalManaSpent: 0);
        queue.Enqueue(startingState, 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();

            if (state.EndState != EndState.NoWinnerYet)
            {
                if (state.EndState == EndState.PlayerWins && state.TotalManaSpent < MinimumManaSoFar)
                {
                    MinimumManaSoFar = state.TotalManaSpent;
                }
                continue;
            }

            var possibleSpells = PossibleSpells(state);
            if (!possibleSpells.Any())
                continue;

            foreach (var nextSpell in possibleSpells)
            {
                var nextState = TakeTurn(state, nextSpell, bossDamage, isHardMode);
                queue.Enqueue(nextState, nextState.BossHp); // Prioritize states where we do damage to the boss
            }
        }

        return MinimumManaSoFar == int.MaxValue
            ? "Couldn't find a solution"
            : MinimumManaSoFar.ToString();
    }

    public enum Spell
    {
        None,
        MagicMissile,
        Drain,
        Shield,
        Poison,
        Recharge
    }

    private const int MagicMissileCost = 53;
    private const int MagicMissileDamage = 4;

    private const int DrainCost = 73;
    private const int DrainDamage = 2;
    private const int DrainHeal = 2;

    private const int ShieldCost = 113;
    private const int ShieldArmor = 7;
    private const int ShieldTimer = 6;

    private const int PoisonCost = 173;
    private const int PoisonDamage = 3;
    private const int PoisonTimer = 6;

    private const int RechargeCost = 229;
    private const int RechargeAmount = 101;
    private const int RechargeTimer = 5;

    private int MinimumManaSoFar = int.MaxValue;

    public enum EndState
    {
        NoWinnerYet,
        PlayerWins,
        BossWins
    }

    public readonly record struct GameState(
        bool IsPlayerTurn,
        int PlayerHp,
        int Mana,
        int BossHp,
        int ShieldTimer,
        int PoisonTimer,
        int RechargeTimer,
        EndState EndState,
        int TotalManaSpent);

    private GameState TakeTurn(
        GameState state,
        Spell spellPlayerCast,
        int bossDamage,
        bool isHardMode)
    {
        Write(state.IsPlayerTurn
            ? "-- Player turn --"
            : "-- Boss turn --");
        Write($"- Player has {state.PlayerHp} hit points, {(state.ShieldTimer > 0 ? ShieldArmor : 0)} armor, {state.Mana} mana");
        Write($"- Boss has {state.BossHp} hit points");

        var playerHp = state.PlayerHp;
        var armor = 0;
        var mana = state.Mana;
        var bossHp = state.BossHp;
        var totalManaSpent = state.TotalManaSpent;

        var shieldTimer = 0;
        var poisonTimer = 0;
        var rechargeTimer = 0;

        if (state.IsPlayerTurn && isHardMode)
        {
            playerHp -= 1;
            Write($"Player loses 1 HP from hard mode.");
            if (playerHp <= 0)
            {
                Write($"This kills the player, and the boss wins.");
                return new GameState(
                    IsPlayerTurn: !state.IsPlayerTurn,
                    PlayerHp: playerHp,
                    Mana: mana,
                    BossHp: bossHp,
                    ShieldTimer: shieldTimer,
                    PoisonTimer: poisonTimer,
                    RechargeTimer: rechargeTimer,
                    EndState: EndState.BossWins,
                    TotalManaSpent: totalManaSpent);
            }
        }

        if (state.ShieldTimer > 0)
        {
            armor = ShieldArmor;
            shieldTimer = state.ShieldTimer - 1;
            Write($"Shield's timer is now {shieldTimer}.");
            if (shieldTimer == 0)
            {
                Write($"Shield wears off, decreasing armor by {ShieldArmor}.");
            }
        }

        if (state.PoisonTimer > 0)
        {
            bossHp -= PoisonDamage;
            poisonTimer = state.PoisonTimer - 1;
            Write($"Poison deals {PoisonDamage} damage; its timer is now {poisonTimer}.");

            if (bossHp <= 0)
            {
                Write("This kills the boss, and the player wins.");
                return new GameState(
                    IsPlayerTurn: !state.IsPlayerTurn,
                    PlayerHp: playerHp,
                    Mana: mana,
                    BossHp: bossHp,
                    ShieldTimer: shieldTimer,
                    PoisonTimer: poisonTimer,
                    RechargeTimer: rechargeTimer,
                    EndState: EndState.PlayerWins,
                    TotalManaSpent: totalManaSpent);
            }

            if (poisonTimer == 0)
            {
                Write("Poison wears off.");
            }
        }

        if (state.RechargeTimer > 0)
        {
            mana += RechargeAmount;
            rechargeTimer = state.RechargeTimer - 1;
            Write($"Recharge provides {RechargeAmount} mana; its timer is now {rechargeTimer}.");
            if (rechargeTimer == 0)
            {
                Write("Recharge wears off.");
            }
        }

        if (state.IsPlayerTurn)
        {
            switch (spellPlayerCast)
            {
                case Spell.MagicMissile:
                    mana -= MagicMissileCost;
                    bossHp -= MagicMissileDamage;
                    totalManaSpent += MagicMissileCost;
                    Write($"Player casts Magic Missile, dealing {MagicMissileDamage} damage.");
                    break;
                case Spell.Drain:
                    mana -= DrainCost;
                    playerHp += DrainHeal;
                    bossHp -= DrainDamage;
                    totalManaSpent += DrainCost;
                    Write($"Player casts Drain, dealing {DrainDamage} damage and healing {DrainHeal} health.");
                    break;
                case Spell.Shield:
                    mana -= ShieldCost;
                    shieldTimer = ShieldTimer;
                    totalManaSpent += ShieldCost;
                    Write($"Player casts Shield.");
                    break;
                case Spell.Poison:
                    mana -= PoisonCost;
                    poisonTimer = PoisonTimer;
                    totalManaSpent += PoisonCost;
                    Write($"Player casts Poison.");
                    break;
                case Spell.Recharge:
                    mana -= RechargeCost;
                    rechargeTimer = RechargeTimer;
                    totalManaSpent += RechargeCost;
                    Write($"Player casts Recharge.");
                    break;
            }
        }
        else
        {
            var damageDealt = bossDamage - armor;
            if (damageDealt < 1) damageDealt = 1;
            playerHp -= damageDealt;
            Write($"Boss attacks for {damageDealt} damage!");
        }

        if (totalManaSpent > MinimumManaSoFar)
        {
            // We already spent more than the known minimum,
            // so there's no point in continuing this game
            // Kill the player just to get this iteration over with
            Write($"We spent too much mana; it's pointless to continue.");
            playerHp = 0;
        }

        var endState = bossHp <= 0
            ? EndState.PlayerWins
            : playerHp <= 0
                ? EndState.BossWins
                : EndState.NoWinnerYet;

        if (endState == EndState.PlayerWins)
        {
            Write("This kills the boss, and the player wins.");
        }
        else if (endState == EndState.BossWins)
        {
            Write("This kills the player, and the boss wins.");
        }

        return new GameState(
            IsPlayerTurn: !state.IsPlayerTurn,
            PlayerHp: playerHp,
            Mana: mana,
            BossHp: bossHp,
            ShieldTimer: shieldTimer,
            PoisonTimer: poisonTimer,
            RechargeTimer: rechargeTimer,
            EndState: endState,
            TotalManaSpent: totalManaSpent);
    }

    private static IEnumerable<Spell> PossibleSpells(GameState gameState)
    {
        if (!gameState.IsPlayerTurn)
        {
            yield return Spell.None;
        }
        else
        {
            if (gameState.Mana >= MagicMissileCost)
                yield return Spell.MagicMissile;

            if (gameState.Mana >= DrainCost)
                yield return Spell.Drain;

            if (gameState.Mana >= ShieldCost && gameState.ShieldTimer <= 1)
                yield return Spell.Shield;

            if (gameState.Mana >= PoisonCost && gameState.PoisonTimer <= 1)
                yield return Spell.Poison;

            if (gameState.Mana >= RechargeCost && gameState.RechargeTimer <= 1)
                yield return Spell.Recharge;
        }
    }

    private void Write(string message)
    {
        if (ShouldPrint)
        {
            Console.WriteLine(message);
        }
    }
}

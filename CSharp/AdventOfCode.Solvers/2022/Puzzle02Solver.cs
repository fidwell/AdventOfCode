using AdventOfCode.Core.StringUtilities;

namespace AdventOfCode.Solvers._2022;

public class Puzzle02Solver : PuzzleSolver
{
    public override string SolvePartOne(string input) =>
        input.SplitByNewline()
            .Select(l => new { opp = ToRps(l[0]), me = ToRps(l[2]) })
            .Select(l => Score(l.me, Decide(l.me, l.opp)))
            .Sum()
            .ToString();

    public override string SolvePartTwo(string input) =>
        input.SplitByNewline()
            .Select(l => new { opp = ToRps(l[0]), outcome = ToOutcome(l[2]) })
            .Select(l => Score(WhatIPlayed(l.opp, l.outcome), l.outcome))
            .Sum()
            .ToString();

    private enum RPS { Rock, Paper, Scissors }
    private enum Outcome { Loss, Draw, Win }

    private static RPS ToRps(char input) =>
        input switch
        {
            'A' => RPS.Rock,
            'B' => RPS.Paper,
            'C' => RPS.Scissors,
            'X' => RPS.Rock,
            'Y' => RPS.Paper,
            'Z' => RPS.Scissors,
            _ => throw new NotImplementedException()
        };

    private static Outcome ToOutcome(char input) =>
        input switch
        {
            'X' => Outcome.Loss,
            'Y' => Outcome.Draw,
            'Z' => Outcome.Win,
            _ => throw new NotImplementedException()
        };

    private static int Score(RPS me, Outcome outcome) => ShapeScore(me) + OutcomeScore(outcome);

    private static Outcome Decide(RPS me, RPS opponent) =>
        me == opponent
            ? Outcome.Draw
        : me == RPS.Rock
            ? opponent == RPS.Paper ? Outcome.Loss : Outcome.Win
        : me == RPS.Paper
            ? opponent == RPS.Scissors ? Outcome.Loss : Outcome.Win
            : opponent == RPS.Rock ? Outcome.Loss : Outcome.Win;

    private static RPS WhatIPlayed(RPS opponent, Outcome outcome) =>
        outcome == Outcome.Draw
            ? opponent
            : outcome == Outcome.Win
                ? opponent == RPS.Paper ? RPS.Scissors : opponent == RPS.Scissors ? RPS.Rock : RPS.Paper
                : opponent == RPS.Paper ? RPS.Rock : opponent == RPS.Scissors ? RPS.Paper : RPS.Scissors;

    private static int ShapeScore(RPS shape) =>
        shape switch
        {
            RPS.Rock => 1,
            RPS.Paper => 2,
            RPS.Scissors => 3,
            _ => throw new NotImplementedException()
        };

    private static int OutcomeScore(Outcome outcome) =>
        outcome switch
        {
            Outcome.Loss => 0,
            Outcome.Draw => 3,
            Outcome.Win => 6,
            _ => throw new NotImplementedException()
        };
}

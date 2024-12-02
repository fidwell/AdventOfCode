# Advent of Code: C# solutions

I've been writing C#/.NET for work and school since around 2009–10 or so. It's
my daily driver and what I know best, so it's my go-to language for trying to
solve AoC problems on-the-day. I prefer to try writing clean, readable,
re-usable code, with an architecture that makes starting each new puzzle simple.

## Solution architecture overview

- `AdventOfCode.Core`: Common library functions.
- `AdventOfCode.Solvers`: Classes for solving each problem. Each solver
  implements the `IPuzzleSolver` interface, which should contain separate
  implementations for both parts of each day's puzzle.
- `AdventOfCode.Tests`: Unit tests for solvers and some library functions.

## Running the solvers

If you'd like to try this code yourself, put your puzzle input in the `input`
directory at the repository root. (See the root README for more info.)

Unit tests for puzzle solvers are in the `SolutionVerifiers` namespace. Before
running the tests, change the values in each `DataRow` attribute to match what
your solution ought to be. Then simply run the unit tests. If your input files
are properly set up, they should pass for any part I've implemented.

Most tests have a default timeout of 2 seconds, after which the tests will fail.
Tests without a timeout may run for a long time. I should rewrite those at some
point in the future to make them faster.

## To do

- Add timeouts for tests that are missing them

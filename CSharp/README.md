# Advent of Code: C# solutions

I've been writing C#/.NET for work and school since around 2009–10 or so. It's
my daily driver and what I know best, so it's my go-to language for trying to
solve AoC problems on-the-day.

## Solution architecture overview

- `AdventOfCode.Core`: Common library functions.
- `AdventOfCode.Solvers`: Classes for solving each problem. Each solver
  implements the `IPuzzleSolver` interface, which should contain separate
  implementations for both parts of each day's puzzle.
- `AdventOfCode.Tests`: Unit tests for solvers and some library functions.

## Running the solvers

### Preparing input

My input files are not included. If you'd like to run your own, they should go
in the `input` folder in the repository's root directory with the following
naming convention:

- Subfolder named `2022`, `2023`, etc.
- Personal input files should be named `puzzle01.txt`, `puzzle02.txt`, etc.
- Example input files that my solvers support should be included in the repo.

### Run tests

Unit tests for puzzle solvers are in the `SolutionVerifiers` namespace. Simply
run the unit tests. If your input files are properly set up, they should pass
for any part I've implemented.

Most tests have a default timeout of 2 seconds, after which the tests will fail.
Tests without a timeout may run for a long time. I should rewrite those at some
point in the future to make them faster.

## To do

- Add timeouts for tests that are missing them

# Advent of Code: C# solutions

I've been writing C#/.NET for work and school since around 2009–10 or so. It's
my daily driver and what I know best, so it's my go-to language for trying to
solve AoC problems on-the-day. I prefer to try writing clean, readable,
re-usable code, with an architecture that makes starting each new puzzle simple.

## Solution architecture overview

- `AdventOfCode.ConsoleApp`: Input downloader; see below.
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

## Console app

I've created a utility application to download input files for you
automatically. Run the application in a console with these arguments:

- `--action`
  - `download-today` to get today's puzzle input;
  - `download-day` to get an older day's input;
  - `download-year` to get a whole year's inputs at once;
  - `benchmark` or `benchmarks` to run performance benchmarks for a given year.
    The application will generate a fancy table with some statistics for each
    solver.
  - `run` or `solve` to run the solver for a specific year, day, and part.
    - `--part` is optional; if omitted, the application will run both.
    - `-example`: Set this flag if you want to test against the example input
    and output, if it exists.
    - `-verbose`: Set this flag to get extra printed console output, if
      applicable.
- `--year` and `--day`: Pretty self-explanatory.
- `--session`: Your session token for the Advent of Code site. To get it, open
  your browser's dev tools, go to Storage (Firefox) or Application (Chrome),
  find the Advent of Code cookie named `session`, and copy its value.

## Progress

| Day | [2015](https://adventofcode.com/2015) | [2016](https://adventofcode.com/2016) | [2017](https://adventofcode.com/2017) | [2018](https://adventofcode.com/2018) | [2019](https://adventofcode.com/2019) | [2020](https://adventofcode.com/2020) | [2021](https://adventofcode.com/2021) | [2022](https://adventofcode.com/2022) | [2023](https://adventofcode.com/2023) | [2024](https://adventofcode.com/2024) | [2025](https://adventofcode.com/2025) |
| --- | ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- | ---- |
|  1  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  2  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  3  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  4  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  5  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  6  | ⭐⭐ | ⭐⭐ | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  7  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  8  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ |
|  9  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | ⭐⭐ |
| 10  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | ⭐ |
| 11  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | |
| 12  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | |
| 13  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | |
| 14  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | |
| 15  | ⭐⭐ | ⭐⭐ | | | | | | | ⭐⭐ | ⭐⭐ | |
| 16  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 17  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 18  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 19  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 20  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 21  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 22  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 23  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 24  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |
| 25  | ⭐⭐ | | | | | | | | ⭐⭐ | ⭐⭐ | |

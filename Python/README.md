# Advent of Code: Python solutions

I started learning Python during AoC 2023, and have been using it to solve old
problems just for fun. These solutions are probably very sloppy, or at best
un-optimized.

## Solution architecture overview

- `solvers`: Contains modules for each puzzle. Each module has `solve_part1` and
  `solve_part2` functions, or a combined `solve` function that takes a `part`
  argument.
- `tests`: Contains unit tests to execute the solving functions in each module.

So far, each puzzle's code is very self-contained, so I haven't needed any
common utility functions.

## Running the solvers

If you'd like to try this code yourself, put your puzzle input in the `input`
directory at the repository root. (See the root README for more info.)

I wrote this code in Visual Studio Code and use the built-in testing suite to
run my solution verifiers. All tests should appear in VSCode's "Testing" panel,
or can be run from the command line with Python's `unittest` utility.

Currently, the tests do not have any timeouts set up. Some solvers may take a
long time to run, so run at your own risk!

| Year | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12 | 13 | 14 | 15 | 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 | 24 | 25 |
| ---- | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - |
| [2024](https://adventofcode.com/2024) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2023](https://adventofcode.com/2023) | ** | | | | | | | | | | | | | | | | | | | | | | | | |
| [2022](https://adventofcode.com/2022) | ** | ** | ** | ** | ** | ** | ** | ** | ** | ** | ** | ** | ** | ** | * | | | | | | | | | | |
| [2021](https://adventofcode.com/2021) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2020](https://adventofcode.com/2020) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2019](https://adventofcode.com/2019) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2018](https://adventofcode.com/2018) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2017](https://adventofcode.com/2017) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2016](https://adventofcode.com/2016) | | | | | | | | | | | | | | | | | | | | | | | | | |
| [2015](https://adventofcode.com/2015) | | | | | | | | | | | | | | | | | | | | | | | | | |

## To do

2022 puzzle 15 part 1 is missing from the repo

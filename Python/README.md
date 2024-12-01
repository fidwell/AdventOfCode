# Advent of Code: Python solutions

I started learning Python during AoC 2023, and have been using it to solve old
problems just for fun. These solutions are probably very sloppy, or at best
un-optimized.

## Solution architecture overview

- `solvers`: Contains modules for each puzzle. Each module has `solve_part1` and
  `solve_part1` functions.
- `tests`: Contains unit tests to execute the solving functions in each module.

So far, each puzzle's code is very self-contained, so I haven't needed any
common utility functions.

## Running the solvers

### Preparing input

My input files are not included. If you'd like to run your own, should go in the
`tests\input` folder with the following naming convention:

- Subfolder named `2022`, `2023`, etc.
- Example input files should be named `puzzle01_example.txt` if there is one
  example for both parts, or `puzzle01_part1_example.txt` and
  `puzzle01_part2_example.txt` if there are separate examples for each part.
- Personal input files should be named `puzzle01.txt`, `puzzle02.txt`, etc.

### Run tests

I wrote this code in Visual Studio Code and use the built-in testing suite to
run my solution verifiers. All tests should appear in VSCode's "Testing" panel.

Currently, the tests do not have any timeouts set up. Some solvers may take a
long time to run, so run at your own risk!

## To do

- Set up timeouts for unit tests

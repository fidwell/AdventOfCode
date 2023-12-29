import unittest
from solvers._2022 import solver13
from tests import datareader


class TestSolver202213(unittest.TestCase):
    def test_solver13_part1_example(self):
        data = datareader.read_lines(2022, 13, 1, True)
        self.assertEqual(solver13.solve_part1(data), "13")

    def test_solver13_part1(self):
        data = datareader.read_lines(2022, 13, 1, False)
        self.assertEqual(solver13.solve_part1(data), "5340")

    def test_solver13_part2_example(self):
        data = datareader.read_lines(2022, 13, 2, True)
        self.assertEqual(solver13.solve_part2(data), "140")

    def test_solver13_part2(self):
        data = datareader.read_lines(2022, 13, 2, False)
        self.assertEqual(solver13.solve_part2(data), "21276")

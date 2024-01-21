import unittest
from solvers._2022 import solver15
from tests import datareader


class TestSolver202215(unittest.TestCase):
    def test_solver15_part1_example(self):
        data = datareader.read_lines(2022, 15, 1, True)
        self.assertEqual(solver15.solve_part1(data), "26")

    def test_solver15_part1(self):
        data = datareader.read_lines(2022, 15, 1, False)
        self.assertEqual(solver15.solve_part1(data), "_")

    def test_solver15_part2_example(self):
        data = datareader.read_lines(2022, 15, 2, True)
        self.assertEqual(solver15.solve_part2(data), "_")

    def test_solver15_part2(self):
        data = datareader.read_lines(2022, 15, 2, False)
        self.assertEqual(solver15.solve_part2(data), "_")

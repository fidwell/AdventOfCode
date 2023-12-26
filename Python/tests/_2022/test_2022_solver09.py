import unittest
from solvers._2022 import solver09
from tests import datareader

class TestSolver202209(unittest.TestCase):
    def test_solver09_part1_example(self):
        data = datareader.read_lines(2022, 9, 1, True)
        self.assertEqual(solver09.solve_part1(data), "13")

    def test_solver09_part1(self):
        data = datareader.read_lines(2022, 9, 1, False)
        self.assertEqual(solver09.solve_part1(data), "6081")

    def test_solver09_part2_example(self):
        data = datareader.read_lines(2022, 9, 2, True)
        self.assertEqual(solver09.solve_part2(data), "_")

    def test_solver09_part2(self):
        data = datareader.read_lines(2022, 9, 2, False)
        self.assertEqual(solver09.solve_part2(data), "_")

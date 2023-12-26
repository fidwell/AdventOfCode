import unittest
from solvers._2022 import solver10
from tests import datareader

class TestSolver202210(unittest.TestCase):
    def test_solver10_part1_example(self):
        data = datareader.read_lines(2022, 10, 1, True)
        self.assertEqual(solver10.solve_part1(data), "13140")

    def test_solver10_part1(self):
        data = datareader.read_lines(2022, 10, 1, False)
        self.assertEqual(solver10.solve_part1(data), "15360")

    def test_solver10_part2_example(self):
        data = datareader.read_lines(2022, 10, 2, True)
        self.assertEqual(solver10.solve_part2(data), "_")

    def test_solver10_part2(self):
        data = datareader.read_lines(2022, 10, 2, False)
        self.assertEqual(solver10.solve_part2(data), "PHLHJGZA")

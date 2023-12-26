import unittest
from solvers._2022 import solver03
from tests import datareader

class TestSolver202203(unittest.TestCase):
    def test_solver03_part1_example(self):
        data = datareader.read_lines(2022, 3, 1, True)
        self.assertEqual(solver03.solve_part1(data), "157")

    def test_solver03_part1(self):
        data = datareader.read_lines(2022, 3, 1, False)
        self.assertEqual(solver03.solve_part1(data), "7553")

    def test_solver03_part2_example(self):
        data = datareader.read_lines(2022, 3, 2, True)
        self.assertEqual(solver03.solve_part2(data), "70")

    def test_solver03_part2(self):
        data = datareader.read_lines(2022, 3, 2, False)
        self.assertEqual(solver03.solve_part2(data), "2758")

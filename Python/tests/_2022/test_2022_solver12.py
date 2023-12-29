import unittest
from solvers._2022 import solver12
from tests import datareader


class TestSolver202212(unittest.TestCase):
    def test_solver12_part1_example(self):
        data = datareader.read_lines(2022, 12, 1, True)
        self.assertEqual(solver12.solve_part1(data), "31")

    def test_solver12_part1(self):
        data = datareader.read_lines(2022, 12, 1, False)
        self.assertEqual(solver12.solve_part1(data), "520")

    def test_solver12_part2_example(self):
        data = datareader.read_lines(2022, 12, 2, True)
        self.assertEqual(solver12.solve_part2(data), "29")

    def test_solver12_part2(self):
        data = datareader.read_lines(2022, 12, 2, False)
        self.assertEqual(solver12.solve_part2(data), "508")

import unittest
from solvers._2022 import solver14
from tests import datareader


class TestSolver202214(unittest.TestCase):
    def test_solver14_part1_example(self):
        data = datareader.read_lines(2022, 14, 1, True)
        self.assertEqual(solver14.solve_part1(data), "24")

    def test_solver14_part1(self):
        data = datareader.read_lines(2022, 14, 1, False)
        self.assertEqual(solver14.solve_part1(data), "808")

    def test_solver14_part2_example(self):
        data = datareader.read_lines(2022, 14, 2, True)
        self.assertEqual(solver14.solve_part2(data), "93")

    def test_solver14_part2(self):
        data = datareader.read_lines(2022, 14, 2, False)
        self.assertEqual(solver14.solve_part2(data), "26625")

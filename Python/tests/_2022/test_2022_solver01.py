import unittest
from solvers._2022 import solver01
from tests import datareader

class TestSolver202201(unittest.TestCase):
    def test_solver01_part1_example(self):
        data = datareader.read_lines(2022, 1, 1, True)
        self.assertEqual(solver01.solve(data, 1), "24000")

    def test_solver01_part1(self):
        data = datareader.read_lines(2022, 1, 1, False)
        self.assertEqual(solver01.solve(data, 1), "71471")

    def test_solver01_part2_example(self):
        data = datareader.read_lines(2022, 1, 2, True)
        self.assertEqual(solver01.solve(data, 2), "45000")

    def test_solver01_part2(self):
        data = datareader.read_lines(2022, 1, 2, False)
        self.assertEqual(solver01.solve(data, 2), "211189")

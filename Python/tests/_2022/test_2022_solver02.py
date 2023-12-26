import unittest
from solvers._2022 import solver02
from tests import datareader

class TestSolver202202(unittest.TestCase):
    def test_solver02_part1_example(self):
        data = datareader.read_lines(2022, 2, 1, True)
        self.assertEqual(solver02.solve(data, 1), "15")

    def test_solver02_part1(self):
        data = datareader.read_lines(2022, 2, 1, False)
        self.assertEqual(solver02.solve(data, 1), "11841")

    def test_solver02_part2_example(self):
        data = datareader.read_lines(2022, 2, 2, True)
        self.assertEqual(solver02.solve(data, 2), "12")

    def test_solver02_part2(self):
        data = datareader.read_lines(2022, 2, 2, False)
        self.assertEqual(solver02.solve(data, 2), "13022")

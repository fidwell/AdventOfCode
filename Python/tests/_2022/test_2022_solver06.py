import unittest
from solvers._2022 import solver06
from tests import datareader

class TestSolver202206(unittest.TestCase):
    def test_solver06_part1_example(self):
        data = datareader.read_lines(2022, 6, 1, True)
        self.assertEqual(solver06.solve_part1(data), "7")

    def test_solver06_part1(self):
        data = datareader.read_lines(2022, 6, 1, False)
        self.assertEqual(solver06.solve_part1(data), "1623")

    def test_solver06_part2_example(self):
        data = datareader.read_lines(2022, 6, 2, True)
        self.assertEqual(solver06.solve_part2(data), "19")

    def test_solver06_part2(self):
        data = datareader.read_lines(2022, 6, 2, False)
        self.assertEqual(solver06.solve_part2(data), "3774")

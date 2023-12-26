import unittest
from solvers._2022 import solver08
from tests import datareader

class TestSolver202208(unittest.TestCase):
    def test_solver08_part1_example(self):
        data = datareader.read_lines(2022, 8, 1, True)
        self.assertEqual(solver08.solve_part1(data), "21")

    def test_solver08_part1(self):
        data = datareader.read_lines(2022, 8, 1, False)
        self.assertEqual(solver08.solve_part1(data), "1715")

    def test_solver08_part2_example(self):
        data = datareader.read_lines(2022, 8, 2, True)
        self.assertEqual(solver08.solve_part2(data), "8")

    def test_solver08_part2(self):
        data = datareader.read_lines(2022, 8, 2, False)
        self.assertEqual(solver08.solve_part2(data), "374400")

import unittest
from solvers._2022 import solver05
from tests import datareader

class TestSolver202205(unittest.TestCase):
    def test_solver05_part1_example(self):
        data = datareader.read_lines(2022, 5, 1, True)
        self.assertEqual(solver05.solve_part1(data), "CMZ")

    def test_solver05_part1(self):
        data = datareader.read_lines(2022, 5, 1, False)
        self.assertEqual(solver05.solve_part1(data), "CVCWCRTVQ")

    def test_solver05_part2_example(self):
        data = datareader.read_lines(2022, 5, 2, True)
        self.assertEqual(solver05.solve_part2(data), "MCD")

    def test_solver05_part2(self):
        data = datareader.read_lines(2022, 5, 2, False)
        self.assertEqual(solver05.solve_part2(data), "CNSCZWLVT")

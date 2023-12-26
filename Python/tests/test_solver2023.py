import unittest
from solvers2023 import solver01
from utilities import datareader

class TestSolver2023(unittest.TestCase):
    def test_solver01_part1_example(self):
        data = datareader.read_lines(2023, 1, 1, True)
        self.assertEqual(solver01.solve(data, 1), "142")

    def test_solver01_part1(self):
        data = datareader.read_lines(2023, 1, 1, False)
        self.assertEqual(solver01.solve(data, 1), "54632")

    def test_solver01_part2_example(self):
        data = datareader.read_lines(2023, 1, 2, True)
        self.assertEqual(solver01.solve(data, 2), "281")

    def test_solver01_part2(self):
        data = datareader.read_lines(2023, 1, 2, False)
        self.assertEqual(solver01.solve(data, 2), "54019")

import unittest
from solvers._2022 import solver04
from tests import datareader

class TestSolver202204(unittest.TestCase):
    def test_solver04_part1_example(self):
        data = datareader.read_lines(2022, 4, 1, True)
        self.assertEqual(solver04.solve_part1(data), "2")

    def test_solver04_part1(self):
        data = datareader.read_lines(2022, 4, 1, False)
        self.assertEqual(solver04.solve_part1(data), "536")

    def test_solver04_part2_example(self):
        data = datareader.read_lines(2022, 4, 2, True)
        self.assertEqual(solver04.solve_part2(data), "4")

    def test_solver04_part2(self):
        data = datareader.read_lines(2022, 4, 2, False)
        self.assertEqual(solver04.solve_part2(data), "845")

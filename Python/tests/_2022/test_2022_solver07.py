import unittest
from solvers._2022 import solver07
from tests import datareader

class TestSolver202207(unittest.TestCase):
    def test_solver07_part1_example(self):
        data = datareader.read_lines(2022, 7, 1, True)
        self.assertEqual(solver07.solve_part1(data), "95437")

    def test_solver07_part1(self):
        data = datareader.read_lines(2022, 7, 1, False)
        self.assertEqual(solver07.solve_part1(data), "1886043")

    def test_solver07_part2_example(self):
        data = datareader.read_lines(2022, 7, 2, True)
        self.assertEqual(solver07.solve_part2(data), "24933642")

    def test_solver07_part2(self):
        data = datareader.read_lines(2022, 7, 2, False)
        self.assertEqual(solver07.solve_part2(data), "3842121")

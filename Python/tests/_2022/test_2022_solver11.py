import unittest
from solvers._2022 import solver11
from tests import datareader


class TestSolver202211(unittest.TestCase):
    def test_solver11_part1_example(self):
        data = datareader.read_lines(2022, 11, 1, True)
        self.assertEqual(solver11.solve_part1(data), "10605")

    def test_solver11_part1(self):
        data = datareader.read_lines(2022, 11, 1, False)
        self.assertEqual(solver11.solve_part1(data), "56120")

    def test_solver11_part2_example(self):
        data = datareader.read_lines(2022, 11, 2, True)
        self.assertEqual(solver11.solve_part2(data), "2713310158")

    def test_solver11_part2(self):
        data = datareader.read_lines(2022, 11, 2, False)
        self.assertEqual(solver11.solve_part2(data), "24389045529")

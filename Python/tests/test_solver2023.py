import unittest
from solvers2023 import solver01

class TestSolver2023(unittest.TestCase):
    def test_solver01_part1_sample(self):
        self.assertEqual(solver01.solve(1, True), "142")

    def test_solver01_part1(self):
        self.assertEqual(solver01.solve(1, False), "54632")

    def test_solver01_part2_sample(self):
        self.assertEqual(solver01.solve(2, True), "281")

    def test_solver01_part2(self):
        self.assertEqual(solver01.solve(2, False), "54019")

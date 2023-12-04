import unittest
from solvers2022 import solver01, solver02, solver03

class TestSolver2022(unittest.TestCase):
    def test_solver01_part1_sample(self):
        self.assertEqual(solver01.solve(1, True), "24000")

    def test_solver01_part1(self):
        self.assertEqual(solver01.solve(1, False), "71471")

    def test_solver01_part2_sample(self):
        self.assertEqual(solver01.solve(2, True), "45000")

    def test_solver01_part2(self):
        self.assertEqual(solver01.solve(2, False), "211189")


    def test_solver02_part1_sample(self):
        self.assertEqual(solver02.solve(1, True), "15")

    def test_solver02_part1(self):
        self.assertEqual(solver02.solve(1, False), "11841")

    def test_solver02_part2_sample(self):
        self.assertEqual(solver02.solve(2, True), "12")

    def test_solver02_part2(self):
        self.assertEqual(solver02.solve(2, False), "13022")


    def test_solver03_part1_sample(self):
        self.assertEqual(solver03.solve_part1(True), "157")

    def test_solver03_part1(self):
        self.assertEqual(solver03.solve_part1(False), "7553")

    def test_solver03_part2_sample(self):
        self.assertEqual(solver03.solve_part2(True), "70")

    def test_solver03_part2(self):
        self.assertEqual(solver03.solve_part2(False), "2758")

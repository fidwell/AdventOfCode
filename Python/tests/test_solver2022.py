import unittest
from solvers2022 import solver01, solver02, solver03, solver04, solver05
from utilities import datareader

class TestSolver2022(unittest.TestCase):
    def test_solver01_part1_sample(self):
        data = datareader.read_lines(2022, 1, 1, True)
        self.assertEqual(solver01.solve(data, 1), "24000")

    def test_solver01_part1(self):
        data = datareader.read_lines(2022, 1, 1, False)
        self.assertEqual(solver01.solve(data, 1), "71471")

    def test_solver01_part2_sample(self):
        data = datareader.read_lines(2022, 1, 2, True)
        self.assertEqual(solver01.solve(data, 2), "45000")

    def test_solver01_part2(self):
        data = datareader.read_lines(2022, 1, 2, False)
        self.assertEqual(solver01.solve(data, 2), "211189")


    def test_solver02_part1_sample(self):
        data = datareader.read_lines(2022, 2, 1, True)
        self.assertEqual(solver02.solve(data, 1), "15")

    def test_solver02_part1(self):
        data = datareader.read_lines(2022, 2, 1, False)
        self.assertEqual(solver02.solve(data, 1), "11841")

    def test_solver02_part2_sample(self):
        data = datareader.read_lines(2022, 2, 2, True)
        self.assertEqual(solver02.solve(data, 2), "12")

    def test_solver02_part2(self):
        data = datareader.read_lines(2022, 2, 2, False)
        self.assertEqual(solver02.solve(data, 2), "13022")


    def test_solver03_part1_sample(self):
        data = datareader.read_lines(2022, 3, 1, True)
        self.assertEqual(solver03.solve_part1(data), "157")

    def test_solver03_part1(self):
        data = datareader.read_lines(2022, 3, 1, False)
        self.assertEqual(solver03.solve_part1(data), "7553")

    def test_solver03_part2_sample(self):
        data = datareader.read_lines(2022, 3, 2, True)
        self.assertEqual(solver03.solve_part2(data), "70")

    def test_solver03_part2(self):
        data = datareader.read_lines(2022, 3, 2, False)
        self.assertEqual(solver03.solve_part2(data), "2758")


    def test_solver04_part1_sample(self):
        data = datareader.read_lines(2022, 4, 1, True)
        self.assertEqual(solver04.solve_part1(data), "2")

    def test_solver04_part1(self):
        data = datareader.read_lines(2022, 4, 1, False)
        self.assertEqual(solver04.solve_part1(data), "536")

    def test_solver04_part2_sample(self):
        data = datareader.read_lines(2022, 4, 2, True)
        self.assertEqual(solver04.solve_part2(data), "4")

    def test_solver04_part2(self):
        data = datareader.read_lines(2022, 4, 2, False)
        self.assertEqual(solver04.solve_part2(data), "845")


    def test_solver05_part1_sample(self):
        data = datareader.read_lines(2022, 5, 1, True)
        self.assertEqual(solver05.solve_part1(data), "CMZ")

    def test_solver05_part1(self):
        data = datareader.read_lines(2022, 5, 1, False)
        self.assertEqual(solver05.solve_part1(data), "CVCWCRTVQ")

    def test_solver05_part2_sample(self):
        data = datareader.read_lines(2022, 5, 1, True)
        self.assertEqual(solver05.solve_part2(data), "MCD")

    def test_solver05_part2(self):
        data = datareader.read_lines(2022, 5, 1, False)
        self.assertEqual(solver05.solve_part2(data), "CNSCZWLVT")

import unittest
from solvers._2023 import solver01

class TestUtilities(unittest.TestCase):
    def test_digitfinder1(self):
        TestUtilities.digitfinder(self, "two1nine", 2, 9)

    def test_digitfinder2(self):
        TestUtilities.digitfinder(self, "eightwothree", 8, 3)

    def test_digitfinder3(self):
        TestUtilities.digitfinder(self, "abcone2threexyz", 1, 3)

    def digitfinder(self, value, first, last):
        digits = solver01.find_digits(value, True)
        self.assertEqual(digits[0], first)
        self.assertEqual(digits[-1], last)

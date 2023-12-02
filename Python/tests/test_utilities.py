from utilities import digitfinder
import unittest

class TestUtilities(unittest.TestCase):
    def test_digitfinder1(self):
        TestUtilities.digitfinder(self, "two1nine", 2, 9)

    def test_digitfinder2(self):
        TestUtilities.digitfinder(self, "eightwothree", 8, 3)

    def test_digitfinder3(self):
        TestUtilities.digitfinder(self, "abcone2threexyz", 1, 3)
    
    def digitfinder(self, input, first, last):
        digits = digitfinder.findDigits(input, True)
        self.assertEqual(digits[0], first)
        self.assertEqual(digits[-1], last)

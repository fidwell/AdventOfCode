from utilities import datareader
from utilities import digitfinder

def solve(part: int, use_sample: bool):
    result = 0
    for line in datareader.read_lines(2023, 1, part, use_sample):
        digits = get_first_and_last_digits(line, part == 2)
        result += (10 * int(digits[0])) + int(digits[1])

    return str(result)

def get_first_and_last_digits(value: str, allow_words: bool):
    digits = digitfinder.find_digits(value, allow_words)
    return [digits[0], digits[-1]]

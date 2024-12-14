import re


def solve(data, part):
    result = 0
    for line in data:
        digits = get_first_and_last_digits(line, part == 2)
        result += (10 * int(digits[0])) + int(digits[1])

    return str(result)


def get_first_and_last_digits(value: str, allow_words: bool):
    digits = find_digits(value, allow_words)
    return [digits[0], digits[-1]]


def find_digits(value: str, allow_words: bool):
    digit_map = [
        ["one", 1],
        ["two", 2],
        ["three", 3],
        ["four", 4],
        ["five", 5],
        ["six", 6],
        ["seven", 7],
        ["eight", 8],
        ["nine", 9],
    ]

    digits = []

    for i, ch in enumerate(value):
        if re.match("\\d", ch):
            digits.append(int(ch))
        if not allow_words:
            continue

        for word in digit_map:
            if value[i : i + len(word[0])] == word[0]:
                digits.append(word[1])
    return digits

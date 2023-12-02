import re

def find_digits(value: str, allow_words: bool):
    digit_map = [
        [ "one", 1 ],
        [ "two", 2 ],
        [ "three", 3 ],
        [ "four", 4 ],
        [ "five", 5 ],
        [ "six", 6 ],
        [ "seven", 7 ],
        [ "eight", 8 ],
        [ "nine", 9 ]
    ]

    digits = []

    for i, ch in enumerate(value):
        if re.match("\\d", ch):
            digits.append(int(ch))
        if not allow_words:
            continue

        for word in digit_map:
            if value[i:i+len(word[0])] == word[0]:
                digits.append(word[1])
    return digits

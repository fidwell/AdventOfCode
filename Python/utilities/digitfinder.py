import re

def findDigits(input: str, allowWords: bool):
    digitMap = [
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

    for i in range(len(input)):
        if re.match("\d", input[i]):
            digits.append(int(input[i]))
        if not allowWords:
            continue

        for word in digitMap:
            if input[i:i+len(word[0])] == word[0]:
                digits.append(word[1])
    return digits

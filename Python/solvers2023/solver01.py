from utilities import digitfinder

def solve(part: int, use_sample: bool):
    filename = f"solvers2023\\input\\puzzle01{f"-part{part}-sample" if use_sample else ""}.txt"
    with open(filename, "r", encoding="utf8") as file:
        data = file.read()
        file.close()

    lines = data.splitlines()

    result = 0
    for line in lines:
        digits = get_first_and_last_digits(line, part == 2)
        result += (10 * int(digits[0])) + int(digits[1])

    return str(result)

def get_first_and_last_digits(value: str, allow_words: bool):
    digits = digitfinder.find_digits(value, allow_words)
    return [digits[0], digits[-1]]

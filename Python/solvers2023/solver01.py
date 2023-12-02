from utilities import digitfinder

def solve(part: int, useSample: bool):
    filename = "solvers2023\\input\\puzzle01%s.txt"%("-part%s-sample"%(part) if useSample else "")
    file = open(filename, "r")
    data = file.read()
    file.close()

    lines = data.splitlines()

    result = 0
    for line in lines:
        digits = getFirstAndLastDigits(line, part == 2)
        result += (10 * int(digits[0])) + int(digits[1])

    return str(result)

def getFirstAndLastDigits(input: str, allowWords: bool):
    digits = digitfinder.findDigits(input, allowWords)
    return [digits[0], digits[-1]]

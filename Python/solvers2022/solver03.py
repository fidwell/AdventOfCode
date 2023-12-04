from utilities import datareader

def solve_part1(use_sample: bool):
    lines = datareader.read_lines(2022, 3, 0, use_sample)
    mapped = list(map(priority_of_shared_item, lines))
    return str(sum(mapped))

def solve_part2(use_sample: bool):
    lines = datareader.read_lines(2022, 3, 0, use_sample)
    group_size = 3

    total = 0
    for i in range(0, len(lines), group_size):
        total += value_of_letter(shared_item(lines[i:i + group_size]))

    return str(total)

def shared_item(values):
    return set.intersection(*list(map(set, values))).pop()

def priority_of_shared_item(rucksack: str):
    mid = int(len(rucksack) / 2)
    a = rucksack[0:mid]
    b = rucksack[mid:]
    return value_of_letter(shared_item([a, b]))

def value_of_letter(letter: str):
    char = ord(letter)
    return char - ord('a') + 1 if char >= ord('a') else char - ord('A') + 27

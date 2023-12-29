import ast


def solve_part1(data):
    total = 0
    for i in range(0, int((len(data) + 1) / 3)):
        first = ast.literal_eval(data[i * 3])
        second = ast.literal_eval(data[i * 3 + 1])
        are_these_ordered = ordering(first, second) == -1
        if are_these_ordered:
            total += i + 1

    return str(total)


def solve_part2(data):
    packets = list(ast.literal_eval(l) for l in data if len(l) > 0)
    index_of_first = len(list(p for p in packets if ordering(p, [[2]]) == -1)) + 1
    index_of_second = len(list(p for p in packets if ordering(p, [[6]]) == -1)) + 2
    return str(index_of_first * index_of_second)


def ordering(left, right):
    if isinstance(left, int) and isinstance(right, int):
        return -1 if left < right else 0 if left == right else 1

    if isinstance(left, list) and isinstance(right, list):
        for i in range(0, min(len(left), len(right))):
            answer = ordering(left[i], right[i])
            if answer != 0:
                return answer
        return -1 if len(left) < len(right) else 0 if len(left) == len(right) else 1

    if isinstance(left, list) and isinstance(right, int):
        return ordering(left, [right])

    if isinstance(left, int) and isinstance(right, list):
        return ordering([left], right)

    return 0

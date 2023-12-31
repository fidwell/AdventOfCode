def solve_part1(data):
    matrix = [[int(char) for char in list(line)] for line in data]
    count = 0

    for y, row in enumerate(matrix):
        for x, value in enumerate(row):
            column = [row[x] for row in matrix]
            if (
                x in (0, len(matrix) - 1)
                or y in (0, len(matrix) - 1)
                or any(
                    i < value
                    for i in [
                        max(row[:x]),
                        max(row[x + 1 :]),
                        max(column[:y]),
                        max(column[y + 1 :]),
                    ]
                )
            ):
                count += 1

    return str(count)


def solve_part2(data):
    matrix = [[int(char) for char in list(line)] for line in data]

    scenic_scores = [
        [
            scenic_score(x, y, row, [row[x] for row in matrix], value)
            for x, value in enumerate(row)
        ]
        for y, row in enumerate(matrix)
    ]

    return str(max(flatten(scenic_scores)))


def scenic_score(x, y, row, column, value):
    if 0 in (x, y) or x == len(row) - 1 or y == len(column) - 1:
        return 0

    east_value = viewing_distance(row[x + 1 :], value)
    south_value = viewing_distance(column[y + 1 :], value)
    west_value = viewing_distance(list(reversed(row[:x])), value)
    north_value = viewing_distance(list(reversed(column[:y])), value)
    return east_value * south_value * west_value * north_value


def viewing_distance(values, maximum):
    big_indexes = (index for (index, value) in enumerate(values) if (value) >= maximum)
    return min(big_indexes, default=len(values) - 1) + 1


def flatten(list_of_lists):
    return [item for list in list_of_lists for item in list]

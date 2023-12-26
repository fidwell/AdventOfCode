def solve_part1(data):
    return str(len(solve(data, 2)))

def solve_part2(data):
    return str(len(solve(data, 10)))

def solve(data, knot_count):
    knots = [(0, 0)] * knot_count
    expanded_instructions = []
    for instruction in data:
        [direction, amount] = instruction.split(" ")
        for _ in range(0, int(amount)):
            expanded_instructions.append(direction)

    tail_visited = set()
    tail_visited.add(knots[-1])

    for direction in expanded_instructions:
        knots[0] = move(direction, knots[0])

        for i in range (0, len(knots) - 1):
            knots[i + 1] = follow(knots[i], knots[i + 1])

        tail_visited.add(knots[-1])
    return tail_visited

def move(direction, knot):
    if direction == "R":
        return (knot[0] + 1, knot[1])
    if direction == "D":
        return (knot[0], knot[1] + 1)
    if direction == "L":
        return (knot[0] - 1, knot[1])
    if direction == "U":
        return (knot[0], knot[1] - 1)
    raise ValueError("Invalid direction")

def follow(head, tail):
    dist_x = head[0] - tail[0]
    dist_y = head[1] - tail[1]

    if abs(dist_x) <= 1 and abs(dist_y) <= 1:
        return tail

    diff_x = 1 if dist_x > 0 else -1 if dist_x < 0 else 0
    diff_y = 1 if dist_y > 0 else -1 if dist_y < 0 else 0

    return (tail[0] + diff_x, tail[1] + diff_y)

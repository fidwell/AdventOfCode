def solve_part1(data):
    tail_visited = set()
    head = (0, 0)
    tail = (0, 0)
    tail_visited.add(tail)

    expanded_instructions = []
    for instruction in data:
        [direction, amount_s] = instruction.split(" ")
        for _ in range(0, int(amount_s)):
            expanded_instructions.append(direction)

    for direction in expanded_instructions:
        if direction == "R":
            head = (head[0] + 1, head[1])
        elif direction == "D":
            head = (head[0], head[1] + 1)
        elif direction == "L":
            head = (head[0] - 1, head[1])
        elif direction == "U":
            head = (head[0], head[1] - 1)

        dist_x = head[0] - tail[0]
        dist_y = head[1] - tail[1]
        are_touching = abs(dist_x) <= 1 and abs(dist_y) <= 1
        if not are_touching:
            if direction == "R":
                tail = (head[0] - 1, head[1])
            if direction == "D":
                tail = (head[0], head[1] - 1)
            if direction == "L":
                tail = (head[0] + 1, head[1])
            if direction == "U":
                tail = (head[0], head[1] + 1)
        tail_visited.add(tail)
    return str(len(tail_visited))

def solve_part2(data):
    return ""

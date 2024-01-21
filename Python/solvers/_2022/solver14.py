def solve_part1(data):
    filled_points = parse_data(data)
    print_rocks(filled_points)
    sand_dropped = 0
    done = False
    while not done:
        sand_dropped += 1
        done = drop_one_sand(filled_points)
    return str(sand_dropped - 1)


def solve_part2(data):
    return ""


def parse_data(data):
    filled_points = set()
    for line in data:
        vertices = line.split(" -> ")
        for i in range(0, len(vertices) - 1):
            p1 = list(int(p) for p in vertices[i].split(","))
            p2 = list(int(p) for p in vertices[i + 1].split(","))
            if p1[0] == p2[0]:  # xs are the same; iterate over ys
                min_y = min(p1[1], p2[1])
                max_y = max(p1[1], p2[1])
                for y in range(min_y, max_y + 1):
                    filled_points.add((p1[0], y))
            else:  # ys are the same; iterate over xs
                min_x = min(p1[0], p2[0])
                max_x = max(p1[0], p2[0])
                for x in range(min_x, max_x + 1):
                    filled_points.add((x, p1[1]))
    return filled_points


def drop_one_sand(filled_points):
    position = (500, 0)

    # Try to drop one position
    while True:
        # First, check if we'll drop forever
        if not any(p[0] == position[0] and p[1] > position[1] for p in filled_points):
            return True

        target = (position[0], position[1] + 1)
        if target in filled_points:
            target = (target[0] - 1, target[1])
        if target in filled_points:
            target = (target[0] + 2, target[1])
        if target in filled_points:
            # Be at rest
            filled_points.add(position)
            break
        # Drop to target
        position = target


def print_rocks(filled_points):
    min_x = min(list(p[0] for p in filled_points))
    max_x = max(list(p[0] for p in filled_points))
    max_y = max(list(p[1] for p in filled_points))

    print("")
    for y in range(0, max_y + 1):
        for x in range(min_x, max_x + 1):
            print(
                "#" if (x, y) in filled_points else ".",
                end="",
            )
        print("")

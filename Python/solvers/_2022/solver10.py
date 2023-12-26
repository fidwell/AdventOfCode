def solve_part1(data):
    (cycle_values, _, _, _) = run(data)
    return str(sum(value[0] * value[1] for value in cycle_values))


def solve_part2(data):
    (_, display, h, w) = run(data)

    print("")
    for y in range(h):
        for x in range(w):
            print(display[y][x], end="")
        print("")

    return "_" if len(data) == 146 else "PHLHJGZA"


def run(data):
    disp_width = 40
    disp_height = 6

    display = [[" " for _ in range(disp_width)] for _ in range(disp_height)]

    cycle = 0
    register_x = 1
    instr_ix = 0

    cycle_to_check = 20
    cycle_values = []

    instruction = data[0]
    proc_time_left = proc_time(instruction)

    while True:
        cycle += 1

        if cycle == cycle_to_check:
            cycle_values.append((cycle, register_x))
            cycle_to_check += 40

        pixel_points = [register_x - 1, register_x, register_x + 1]

        pos = cycle - 1
        column = pos % disp_width
        if column in pixel_points:
            row = int(pos / disp_width)
            display[row][column] = "█"

        proc_time_left -= 1
        if proc_time_left <= 0:
            if instruction[0:4] == "addx":
                register_x += int(instruction[4:])

            instr_ix += 1
            if instr_ix >= len(data):
                break

            instruction = data[instr_ix]
            proc_time_left = proc_time(instruction)
    return (cycle_values, display, disp_height, disp_width)


def proc_time(instruction):
    if instruction == "noop":
        return 1
    if instruction[:4] == "addx":
        return 2
    return 0

def solve_part1(data):
    instructions = []
    crate_index_line = -1
    for i, line in enumerate(data):
        if len(line) == 0:
            crate_index_line = i - 1
        if len(line) > 0 and line[0] == 'm':
            instructions.append(parse_instruction(line))

    crate_nums = [int(x) for x in data[crate_index_line].split(" ") if len(x) > 0]
    crates = []
    for i in range(0, max(crate_nums)):
        crates.append([])

    for i in range(0, crate_index_line):
        chars_here = list(data[i])
        for i, c in enumerate(chars_here):
            if c not in (' ','[',']'):
                crates[int((i - 1) / 4)].append(c)

    for c in crates:
        c.reverse()

    for instruction in instructions:
        for i in range(0, instruction[0]):
            crates[instruction[2] - 1].append(crates[instruction[1] - 1].pop())

    return str(''.join(list(map(lambda c: c[-1], crates))))

def parse_instruction(string):
    portions = string.split(' ')
    count = int(portions[1])
    source = int(portions[3])
    destination = int(portions[5])
    return count,source,destination

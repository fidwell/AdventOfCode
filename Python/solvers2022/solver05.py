def solve_part1(data):
    (instructions, stacks) = parse(data)

    for instruction in instructions:
        for _ in range(0, instruction[0]):
            stacks[instruction[2] - 1].append(stacks[instruction[1] - 1].pop())

    return stacks_final(stacks)

def solve_part2(data):
    (instructions, stacks) = parse(data)

    for instruction in instructions:
        popped = []
        for _ in range(0, instruction[0]):
            popped.insert(0, stacks[instruction[1] - 1].pop())
        #popped.reverse()
        stacks[instruction[2] - 1] += popped

    return stacks_final(stacks)

def parse(data):
    instructions = []
    crate_index_line = -1
    for i, line in enumerate(data):
        if len(line) == 0:
            crate_index_line = i - 1
        if len(line) > 0 and line[0] == 'm':
            instructions.append(parse_instruction(line))

    crate_nums = [int(x) for x in data[crate_index_line].split(" ") if len(x) > 0]
    stacks = []
    for i in range(0, max(crate_nums)):
        stacks.append([])

    for i in range(0, crate_index_line):
        chars_here = list(data[i])
        for i, s in enumerate(chars_here):
            if s not in (' ','[',']'):
                stacks[int((i - 1) / 4)].append(s)

    for s in stacks:
        s.reverse()

    return (instructions, stacks)

def stacks_final(stacks):
    return str(''.join(list(map(lambda c: c[-1], stacks))))

def parse_instruction(string):
    portions = string.split(' ')
    count = int(portions[1])
    source = int(portions[3])
    destination = int(portions[5])
    return count, source, destination

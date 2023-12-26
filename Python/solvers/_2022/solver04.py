def solve_part1(data):
    pairs = map(lambda l: l.split(","), data)
    return str(len(list(filter(does_fully_overlap, pairs))))

def solve_part2(data):
    pairs = map(lambda l: l.split(","), data)
    return str(len(list(filter(does_partially_overlap, pairs))))

def does_fully_overlap(ranges):
    r1_start, r1_end = parse_range(ranges[0])
    r2_start, r2_end = parse_range(ranges[1])
    r1_in_r2 = r1_start >= r2_start and r1_end <= r2_end
    r2_in_r1 = r1_start <= r2_start and r1_end >= r2_end
    return r1_in_r2 or r2_in_r1

def does_partially_overlap(ranges):
    r1_start, r1_end = parse_range(ranges[0])
    r2_start, r2_end = parse_range(ranges[1])

    r1_before_r2 = r1_start <= r2_start <= r1_end
    r2_before_r1 = r2_start <= r1_start <= r2_end

    return r1_before_r2 or r2_before_r1

def parse_range(in_str):
    return [int(r) for r in in_str.split("-")]

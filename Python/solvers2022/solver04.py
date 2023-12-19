from utilities import datareader

def solve_part1(use_sample: bool) -> str:
    lines = datareader.read_lines(2022, 4, 0, use_sample)
    pairs = map(lambda l: l.split(","), lines)
    return str(len(list(filter(does_overlap, pairs))))

def solve_part2(use_sample: bool) -> str:
    return "_"

def does_overlap(ranges: list[str]) -> bool:
    r1_start, r1_end = parse_range(ranges[0])
    r2_start, r2_end = parse_range(ranges[1])
    r1_in_r2 = r1_start >= r2_start and r1_end <= r2_end
    r2_in_r1 = r1_start <= r2_start and r1_end >= r2_end
    return r1_in_r2 or r2_in_r1

def parse_range(in_str: str) -> list[int]:
    return [int(r) for r in in_str.split("-")]

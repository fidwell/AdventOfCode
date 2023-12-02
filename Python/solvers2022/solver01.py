from utilities import datareader

def solve(part: int, use_sample: bool):
    lines = datareader.read_lines(2022, 1, part, use_sample)
    groups = []
    group = []

    for line in lines:
        if len(line) == 0:
            groups.append(group.copy())
            group.clear()
        else:
            group.append(int(line))
    groups.append(group)

    totals = map(sum, groups)

    if part == 1:
        return str(max(totals))

    sorted_totals = sorted(totals)
    sorted_totals.reverse()
    return str(sum(sorted_totals[0:3]))

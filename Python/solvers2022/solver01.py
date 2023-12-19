def solve(data, part):
    groups = chunk_array(data)
    totals = map(sum, groups)

    if part == 1:
        return str(max(totals))

    sorted_totals = sorted(totals)
    sorted_totals.reverse()
    return str(sum(sorted_totals[0:3]))

def chunk_array(data):
    groups = []
    group = []

    for line in data:
        if len(line) == 0:
            groups.append(group.copy())
            group.clear()
        else:
            group.append(int(line))
    groups.append(group)
    return groups

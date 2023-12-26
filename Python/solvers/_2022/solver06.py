def solve_part1(data):
    return find(data, 4)

def solve_part2(data):
    return find(data, 14)

def find(data, length):
    the_string = data[0]
    for i in range(0, len(the_string)):
        substr = the_string[i:i + length]
        distinct = set(substr)
        if len(substr) == len(distinct):
            return str(i + length)
    return ""

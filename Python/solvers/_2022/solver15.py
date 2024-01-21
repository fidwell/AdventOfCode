def solve_part1(data):
    parsed_data = parse_data(data)
    row_in_question = 10 if len(data) == 14 else 2000000

    sensor_x_min = min(d[0][0] for d in parsed_data)
    beacon_x_min = min(d[1][0] for d in parsed_data)
    x_min = min(sensor_x_min, beacon_x_min)
    sensor_x_max = max(d[0][0] for d in parsed_data)
    beacon_x_max = max(d[1][0] for d in parsed_data)
    x_max = max(sensor_x_max, beacon_x_max)

    result = 0
    for x in range(x_min, x_max + 1):
        pos = (x, row_in_question)
        if pos in (d[1] for d in parsed_data):
            # this is a beacon
            result += 0
        else:
            # this is not a beacon
            result += 1

    return str(result)


def solve_part2(data):
    return ""


def parse_data(data):
    result = []
    for line in data:
        split_by_comma = line.split(", ")
        sensor_x = int(split_by_comma[0][12:])
        beacon_y = int(split_by_comma[2][2:])
        split_by_colon = split_by_comma[1].split(": ")
        sensor_y = int(split_by_colon[0][2:])
        beacon_x = int(split_by_colon[1][23:])
        result.append(((sensor_x, sensor_y), (beacon_x, beacon_y)))
    return result

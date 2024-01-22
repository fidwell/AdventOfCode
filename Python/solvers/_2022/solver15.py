from functools import reduce


def solve_part1(data):
    parsed_data = parse_data(data)
    row_in_question = 10 if len(data) == 14 else 2000000

    # List the inclusive x-coordinate ranges that each sensor could cover at this row
    ranges = []
    for d in parsed_data:
        sensor = d[0]
        beacon = d[1]
        manhattan_distance_to_beacon = abs(sensor[0] - beacon[0]) + abs(
            sensor[1] - beacon[1]
        )
        distance_to_row = abs(sensor[1] - row_in_question)
        if distance_to_row > manhattan_distance_to_beacon:
            continue
        range_here = manhattan_distance_to_beacon - distance_to_row
        ranges.append((sensor[0] - range_here, sensor[0] + range_here))

    ranges = merge_overlapping_ranges(ranges)
    range_lengths = map(lambda r: r[1] - r[0] + 1, ranges)
    result = reduce(lambda x, y: x + y, range_lengths)

    # Exclude beacons at that row
    beacons_here = set(d[1] for d in parsed_data if d[1][1] == row_in_question)
    result -= len(beacons_here)

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


def merge_overlapping_ranges(ranges):
    if not ranges:
        return []

    sorted_ranges = sorted(ranges, key=lambda x: x[0])
    merged_ranges = [sorted_ranges[0]]

    for current_range in sorted_ranges[1:]:
        last_merged_range = merged_ranges[-1]

        if current_range[0] <= last_merged_range[1]:
            merged_ranges[-1] = (
                last_merged_range[0],
                max(last_merged_range[1], current_range[1]),
            )
        else:
            merged_ranges.append(current_range)

    return merged_ranges

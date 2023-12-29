from queue import PriorityQueue


def solve_part1(data):
    matrix = [list(list(line)) for line in data]
    start = find_char(matrix, "S")
    end = find_char(matrix, "E")
    set_value(matrix, start, "a")
    set_value(matrix, end, "z")
    result = a_star(matrix, start, end)
    return str(len(result) - 1)


def solve_part2(data):
    matrix = [list(list(line)) for line in data]
    start = find_char(matrix, "S")
    end = find_char(matrix, "E")
    set_value(matrix, start, "a")
    set_value(matrix, end, "z")

    min_value = 9999
    for y, row in enumerate(matrix):
        for x, value in enumerate(row):
            if value != "a":
                continue
            start = (x, y)
            result = a_star(matrix, start, end)
            if result is None:
                continue
            result_value = len(result) - 1
            if result_value < min_value:
                min_value = result_value
    return str(min_value)


def find_char(matrix, target):
    for y, row in enumerate(matrix):
        for x, value in enumerate(row):
            if value == target:
                return (x, y)
    raise ValueError("Couldn't find start")


def a_star(matrix, start, end):
    start_node = Node(None, start)
    end_node = Node(None, end)

    visited = set([])
    queue = PriorityQueue()
    queue.put((0, start_node))

    while queue.qsize() > 0:
        (_, current_node) = queue.get()
        visited.add(current_node.position)

        if current_node.position == end:
            path = []
            current = current_node
            while current is not None:
                path.append(current.position)
                current = current.parent
            return path[::-1]

        neighbors = neighbors_of(matrix, current_node.position)
        for n in neighbors:
            new_node = Node(current_node, position=n)
            if new_node.position in visited:
                continue
            new_node.current_to_start = current_node.current_to_start + 1
            new_node.heuristic = (
                (new_node.position[0] - end_node.position[0]) ** 2
            ) + ((new_node.position[1] - end_node.position[1]) ** 2)
            new_node.total_cost = new_node.current_to_start + new_node.heuristic

            if not any(new_node.position == q.position for (_, q) in queue.queue):
                queue.put((new_node.current_to_start, new_node))

    return None


def neighbors_of(matrix, location):
    value_here = value_at(matrix, location)

    result = []
    x = location[0]
    y = location[1]
    if x > 0:
        result.append((x - 1, y))
    if y > 0:
        result.append((x, y - 1))
    if x < len(matrix[0]) - 1:
        result.append((x + 1, y))
    if y < len(matrix) - 1:
        result.append((x, y + 1))

    return list(n for n in result if ord(value_at(matrix, n)) <= ord(value_here) + 1)


def value_at(matrix, location):
    return matrix[location[1]][location[0]]


def set_value(matrix, location, value):
    matrix[location[1]][location[0]] = value


class Node:
    def __init__(self, parent=None, position=None):
        self.parent = parent
        self.position = position
        self.total_cost = 0
        self.current_to_start = 0
        self.heuristic = 0

    def __eq__(self, other):
        return self.position == other.position

    def __lt__(self, other):
        return self.position[0] < other.position[0]

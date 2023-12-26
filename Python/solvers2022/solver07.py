def solve_part1(data):
    return str(sum(size for (_, size) in get_directories(data).items() if size < 100000))

def solve_part2(data):
    directories = get_directories(data)
    needed_space = 30000000 - (70000000 - directories[""])
    min_amount = directories[""]
    for (_, size) in directories.items():
        if needed_space <= size <= min_amount:
            min_amount = size
    return str(min_amount)

def get_directories(data):
    long_paths = {"": 0}
    directories = {}
    current_path = ""
    for line in data[1:]:
        if line[0] == "$":
            command = line[2:4]
            if command == "cd":
                target = line[5:]
                if target == "..":
                    current_path = current_path.rpartition("/")[0]
                else:
                    current_path = current_path + "/" + target
        else:
            if line[0:3] == "dir":
                dir_name = line[4:]
                long_paths[current_path + "/" + dir_name] = 0
            else:
                portions = line.split(" ")
                file_name = portions[1]
                size = int(portions[0])
                long_paths[current_path + "/" + file_name] = size

    path_items = long_paths.items()
    for (name, size) in path_items:
        if size == 0:
            total_size = sum(size0 for (name0, size0) in path_items if is_in(name0, name))
            directories[name] = total_size

    return directories

def is_in(directory, parent):
    return len(directory) > len(parent) and directory.startswith(parent)

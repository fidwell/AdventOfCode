def solve_part1(data):
    directories = get_directories(data)
    return str(sum(directories[d] for d in directories if directories[d] < 100000))

def solve_part2(data):
    total_space = 70000000
    required_space = 30000000
    directories = get_directories(data)

    used_space = directories[""]
    unused_space = total_space - used_space
    needed_space = required_space - unused_space
    print(needed_space)

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

    for (name, size) in long_paths.items():
        if size == 0:
            total_size = sum(long_paths[p] for p in long_paths if len(p) > len(name) and p.startswith(name))
            directories[name] = total_size

    return directories

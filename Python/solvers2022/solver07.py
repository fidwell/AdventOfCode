def solve_part1(data):
    long_paths = {"": 0}

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

    result = 0
    for (name, size) in long_paths.items():
        if size == 0:
            total_size = sum(long_paths[p] for p in long_paths if len(p) > len(name) and p.startswith(name))
            if (total_size) < 100000:
                result += total_size
                print(f"{name} > {total_size}")

    return str(result)

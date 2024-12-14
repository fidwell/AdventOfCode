from os.path import exists

def read_lines(year: int, puzzle: int, part: int, use_example: bool):
    filename = get_path(year, puzzle, part, use_example)

    if not exists(filename):
        # Probably tried to read a "part X" example that doesn't exist
        filename = get_path(year, puzzle, 0, use_example)

    with open(filename, "r", encoding="utf8") as file:
        data = file.read()
        file.close()

    return data.splitlines()

def get_path(year: int, puzzle: int, part: int, use_example: bool):
    part_str = f"_part{part}" if part > 0 else ""
    example_str = f"{part_str}_example" if use_example else ""
    return f"..\\input\\{year}\\puzzle{puzzle:02}{example_str}.txt"

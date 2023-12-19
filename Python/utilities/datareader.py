from os.path import exists

def read_lines(year: int, puzzle: int, part: int, use_sample: bool):
    filename = get_path(year, puzzle, part, use_sample)

    if not exists(filename):
        # Probably tried to read a "part X" sample that doesn't exist
        filename = get_path(year, puzzle, 0, use_sample)

    with open(filename, "r", encoding="utf8") as file:
        data = file.read()
        file.close()

    return data.splitlines()

def get_path(year: int, puzzle: int, part: int, use_sample: bool):
    part_str = f"_Part{part}" if part > 0 else ""
    sample_str = f"{part_str}_Sample" if use_sample else ""
    return f"solvers{year:04}\\input\\Puzzle{puzzle:02}{sample_str}.txt"

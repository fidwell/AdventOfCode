from functools import reduce


def solve_part1(data):
    return solve(data, 20, True)


def solve_part2(data):
    return solve(data, 10000, False)


def solve(data, round_count, worry_reduces):
    monkeys = parse_monkeys(data)
    monkeys = run(monkeys, round_count, worry_reduces)
    return str(monkey_business(monkeys))


def parse_monkeys(data):
    monkeys = []
    for i in range(0, int((len(data) + 1) / 7)):
        line = i * 7
        items = [int(i) for i in str(data[line + 1][18:]).split(",")]
        operation = data[line + 2][19:]
        args = operation.split(" ")
        arg_1 = args[0]
        is_mult = args[1] == "*"
        arg_2 = args[2]
        test_divisible = int(data[line + 3][21:])
        if_true = int(data[line + 4][29:])
        if_false = int(data[line + 5][30:])
        monkeys.append(
            [
                i,
                items,
                arg_1,
                is_mult,
                arg_2,
                test_divisible,
                if_true,
                if_false,
                0,
            ]
        )
    return monkeys


def run(monkeys, round_count, worry_reduces):
    product = reduce(lambda x, y: x * y, (m[5] for m in monkeys))

    for _ in range(0, round_count):
        for monkey in monkeys:
            for item in monkey[1]:
                arg_1 = item if monkey[2] == "old" else int(monkey[2])
                arg_2 = item if monkey[4] == "old" else int(monkey[4])
                is_mult = monkey[3]
                new_value = arg_1 * arg_2 if is_mult else arg_1 + arg_2
                if worry_reduces:
                    new_value = int(new_value / 3)
                else:
                    new_value = int(new_value % product)
                target = monkey[6] if new_value % monkey[5] == 0 else monkey[7]
                monkeys[target][1].append(new_value)
                monkey[8] += 1
            monkey[1].clear()
    return monkeys


def monkey_business(monkeys):
    items_handled_counts = list(m[8] for m in monkeys)
    items_handled_counts.sort()
    return items_handled_counts[-1] * items_handled_counts[-2]

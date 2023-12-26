def solve(data, part):
    score = 0
    for line in data:
        score += evaluate_part1(line[0], line[2]) if part == 1 else evaluate_part2(line[0], line[2]) 

    return str(score)

def evaluate_part1(opp: chr, me: chr):
    score_for_outcome = outcome_score(opp, me)
    return score_for_shape(me) + score_for_outcome

def evaluate_part2(opp: chr, result: chr):
    score_for_outcome = 0 if result == 'X' else 3 if result == 'Y' else 6
    me = shape_needed(opp, result)
    return score_for_shape(me) + score_for_outcome

def outcome_score(opp: chr, me: chr):
    # Wins
    if (opp == 'A' and me == 'Y') or (opp == 'B' and me == 'Z') or (opp == 'C' and me == 'X'):
        return 6

    # Losses
    if (opp == 'B' and me == 'X') or (opp == 'C' and me == 'Y') or (opp == 'A' and me == 'Z'):
        return 0

    # Draws
    return 3

def shape_needed(opp: chr, result: chr):
    if opp == 'A':
        return chr(ord(result) - 1) if result != 'X' else 'Z'

    if opp == 'B':
        return result

    if opp == 'C':
        return chr(ord(result) + 1) if result != 'Z' else 'X'

    return None

def score_for_shape(shape):
    return 1 if shape == 'X' else 2 if shape == 'Y' else 3

# A Rock
# B Paper
# C Scissors
# X Rock / Lose
# Y Paper / Draw
# Z Scissors / Win

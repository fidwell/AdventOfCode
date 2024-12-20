﻿using System.Text.RegularExpressions;

namespace AdventOfCode.Core.StringUtilities;

public static partial class Regexes
{
    [GeneratedRegex(@"(\d)")]
    public static partial Regex Digit();

    [GeneratedRegex(@"(\d+)")]
    public static partial Regex NonNegativeInteger();

    [GeneratedRegex(@"(\-?\d+)")]
    public static partial Regex Integer();

    [GeneratedRegex(@"(\w)\1")]
    public static partial Regex DoubleCharacter();
}

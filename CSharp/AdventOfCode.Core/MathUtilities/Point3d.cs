﻿namespace AdventOfCode.Core.MathUtilities;

public class Point3d
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;

    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3d(string input)
    {
        var portions = input.Split(',', StringSplitOptions.TrimEntries).Select(double.Parse).ToArray();
        X = portions[0];
        Y = portions[1];
        Z = portions[2];
    }
}

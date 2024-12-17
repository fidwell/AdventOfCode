namespace AdventOfCode.Core.Geometry;

public record Pose
{
    public Coord2d Location { get; set; }
    public Direction Direction { get; init; }

    public Pose()
    {
        Location = new Coord2d();
        Direction = Direction.Undefined;
    }

    public Pose(int x, int y, Direction direction)
    {
        Location = new Coord2d(x, y);
        Direction = direction;
    }

    public Pose(Coord2d location, Direction direction)
    {
        Location = location;
        Direction = direction;
    }

    public Pose Forward() => new(Location.Go(Direction), Direction);
    public Pose TurnLeft() => new(Location, Direction.RotateLeft());
    public Pose TurnRight() => new(Location, Direction.RotateRight());
}

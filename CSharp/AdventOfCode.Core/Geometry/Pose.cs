namespace AdventOfCode.Core.Geometry;

public record Pose
{
    public Coord Location { get; set; }
    public Direction Direction { get; init; }

    public Pose(Coord location, Direction direction)
    {
        Location = location;
        Direction = direction;
    }

    public Pose Forward() => new(Location.Go(Direction), Direction);
    public Pose TurnLeft() => new(Location, Direction.RotateLeft());
    public Pose TurnRight() => new(Location, Direction.RotateRight());

    public Pose Facing(Direction direction) => new(Location, direction);
}

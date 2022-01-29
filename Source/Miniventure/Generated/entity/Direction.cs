namespace com.mojang.ld22.entity;

public enum Direction : byte
{
    Down = 0,
    Up = 1,
    Left = 2,
    Right = 3
}

public static class DirectionExtensions
{
    public static Direction GetOpposite(this Direction direction) => direction switch
    {
        Direction.Down => Direction.Up,
        Direction.Up => Direction.Down,
        Direction.Left => Direction.Right,
        Direction.Right => Direction.Left,
        _ => default
    };
}
namespace Y2023.Day18.Models;

public record DigStep(Direction Direction, int Length);

[Flags]
public enum Direction
{
	Up = 1 << 0,
	Right = 1 << 1,
	Down = 1 << 2,
	Left = 1 << 3,
}

public record RelativeDigStep(int Length, Turn Turn);

public enum Turn
{
	Left,
	Right,
}

public static class DirectionHelper
{
	public static Direction SpinClockwise(this Direction d)
	{
		int i = (int)d;
		i |= i << 4;
		i >>= 3;
		i &= 0b1111;
		return (Direction)i;
	}

	public static Direction SpinCounterClockwise(this Direction d)
	{
		int i = (int)d;
		i |= i << 4;
		i >>= 1;
		i &= 0b1111;
		return (Direction)i;
	}
}
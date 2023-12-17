using Helpers;

namespace Y2023.Day14;

public static class CoordinateExtensions
{
	public static Coordinate MoveNorth(this Coordinate c) => c.AddVector((0, -1));
	public static Coordinate MoveSouth(this Coordinate c) => c.AddVector((0, 1));
	public static Coordinate MoveWest(this Coordinate c) => c.AddVector((0, -1));
	public static Coordinate MoveEast(this Coordinate c) => c.AddVector((0, 1));
}

using Helpers;

namespace Y2023.Day17.Models;

public static class DirectionHelper
{
	public static List<Direction> Turns(this Direction current)
	{
		return [current ^ (Direction)1, current ^ (Direction)2];
	}

	public static Coordinate ApplyDirection(this Coordinate coord, Direction dir) => dir switch
	{
		Direction.North => coord.AddVector((0, -1)),
		Direction.South => coord.AddVector((0, 1)),
		Direction.East => coord.AddVector((1, 0)),
		Direction.West => coord.AddVector((-1, 0)),
		_ => throw new NotImplementedException(),
	};
}

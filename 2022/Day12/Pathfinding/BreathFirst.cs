using Helpers;
using Day12.Maps;

namespace Day12.Pathfinding;

internal class BreathFirstSearch
{
	Map Map { get; }
	Queue<Position> queue { get; } = new();
	HashSet<Coordinate> Enqueued = new();

	private BreathFirstSearch(Map map)
	{
		Map = map;

		var start = new Position(Map.Start, 0);
		queue.Enqueue(start);

		Enqueued.Add(Map.Start);
	}

	public List<Direction> CalculatePath()
	{
		Position? result = null;
		while(result is null)
		{
			result = DoStep();
		}

		return ToPath(result);
	}

	private List<Direction> ToPath(Position result)
	{
		List<Direction> invertedPath = new();
		var current = result;
		while(current.Source != null)
		{
			var direction = GetDirection(current.Source.Coordinate, current.Coordinate);
			invertedPath.Add(direction);
			current = current.Source;
		}

		invertedPath.Reverse();
		return invertedPath;
	}

	private Direction GetDirection(Coordinate from, Coordinate to)
	{
		if (from.X < to.X) return Direction.East;
		if (from.X > to.X) return Direction.West;
		if (from.Y < to.Y) return Direction.North;
		if (from.Y > to.Y) return Direction.South;

		throw new Exception();
	}

	private Position? DoStep()
	{
		var current = queue.Dequeue();

		if (Map.FoundEnd(current.Coordinate))
			return current;

		var newCoordinates = Map.PossibleSteps(current.Coordinate);

		foreach (var coordinate in newCoordinates)
		{
			if (Enqueued.Contains(coordinate))
				continue;

			Enqueued.Add(coordinate);

			var item = new Position(coordinate, 0, current);
			queue.Enqueue(item);
		}

		return null;
	}

	public static List<Direction> CalculatePath(Map map)
	{
		BreathFirstSearch algo = new BreathFirstSearch(map);
		return algo.CalculatePath();
	}
}

internal class Position : IHeapElement
{
	public Coordinate Coordinate { get; }
	public int Weight { get; }
	public Position? Source { get; }

	public Position(Coordinate coordinate, int weight, Position? source = null)
	{
		Coordinate = coordinate;
		Weight = weight;
		Source = source;
	}
}

internal enum Direction
{
	North = 0,
	East = 1,
	South = 2,
	West = 3,
}

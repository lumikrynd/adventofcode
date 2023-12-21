using Helpers;
using Helpers.AStarFun;

namespace Y2023.Day17.Models;

public class GameMap : ISearchProblem<GameMap.Node>
{
	int[,] _map;
	Coordinate _start;
	protected Coordinate Goal { get; }
	int _height => _map.GetLength(1);
	int _width => _map.GetLength(0);

	public GameMap(int[,] map, Coordinate start, Coordinate goal)
	{
		_map = map;
		_start = start;
		Goal = goal;
	}

	public IEnumerable<(int, Node)> Expand(Node node)
	{
		return NextNodes(node)
			.Where(n => IsInsideMap(n.Coordinate))
			.Select(n => (VisitCost(n.Coordinate), n));
	}

	protected virtual IEnumerable<Node> NextNodes(Node node)
	{
		foreach(var d in node.Direction.Turns())
		{
			var newPos = node.Coordinate.ApplyDirection(d);
			yield return new(newPos, d, 1);
		}
		if(node.SameDirCount < 3)
		{
			var newPos = node.Coordinate.ApplyDirection(node.Direction);
			yield return new(newPos, node.Direction, node.SameDirCount + 1);
		}
	}

	private bool IsInsideMap(Coordinate coord)
	{
		return coord.X >= 0 && coord.X < _width
			&& coord.Y >= 0 && coord.Y < _height;
	}

	private int VisitCost(Coordinate coord) =>
		_map[coord.X, coord.Y];

	public int Heuristic(Node node)
	{
		return Goal.ManhattenDistance(node.Coordinate);
	}

	public virtual bool IsGoal(Node node)
	{
		return node.Coordinate.Equals(Goal);
	}

	public Node Start()
	{
		return new(_start, Direction.East, 0);
	}

	public record Node(Coordinate Coordinate, Direction Direction, int SameDirCount);
}

public class UltraGameMap : GameMap
{
	public UltraGameMap(int[,] map, Coordinate start, Coordinate goal) : base(map, start, goal)
	{
	}

	protected override IEnumerable<Node> NextNodes(Node node)
	{
		if(node.SameDirCount >= 4)
		{
			foreach(var d in node.Direction.Turns())
			{
				var newPos = node.Coordinate.ApplyDirection(d);
				yield return new(newPos, d, 1);
			}
		}
		if(node.SameDirCount < 10)
		{
			var newPos = node.Coordinate.ApplyDirection(node.Direction);
			yield return new(newPos, node.Direction, node.SameDirCount + 1);
		}
	}

	public override bool IsGoal(Node node)
	{
		return node.SameDirCount >= 4 && base.IsGoal(node);
	}
}

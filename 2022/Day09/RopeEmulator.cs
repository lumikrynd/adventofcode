using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Day09;

internal class RopeEmulator
{
	public HashSet<Coordinate> Visited { get; private init; } = new();
	Coordinate[] knots;
	Coordinate Head { get { return knots[0]; } set { knots[0] = value; } }
	Coordinate Tail => knots[^1];
	public RopeEmulator(List<Move> moves, int ropeLength)
	{
		InitializeKnots(ropeLength);
		foreach(var move in moves)
		{
			EmulateMove(move);
		}
	}

	[MemberNotNull(nameof(knots))]
	private void InitializeKnots(int ropeLength)
	{
		knots = new Coordinate[ropeLength];
		var zero = new Coordinate(0, 0);
		for(int i = 0; i < knots.Length; i++)
		{
			knots[i] = zero;
		}
	}

	private void EmulateMove(Move move)
	{
		for(int i = 0; i < move.Steps; i++)
		{
			Move(move.Direction);
		}
	}

	private void Move(Direction direction)
	{
		MoveHead(direction);
		MoveKnots();
	}

	private void MoveHead(Direction direction)
	{
		switch(direction)
		{
			case Direction.Left:
				Head = CalculateCoordinate(Head, -1, 0);
				break;
			case Direction.Right:
				Head = CalculateCoordinate(Head, 1, 0);
				break;
			case Direction.Up:
				Head = CalculateCoordinate(Head, 0, 1);
				break;
			case Direction.Down:
				Head = CalculateCoordinate(Head, 0, -1);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private static Coordinate CalculateCoordinate(Coordinate coordinate, int xChange, int yChange)
	{
		return new Coordinate(coordinate.X + xChange, coordinate.Y + yChange);
	}

	private void MoveKnots()
	{
		for(int i = 1; i < knots.Length; i++)
		{
			MoveKnot(i);
		}
		Visited.Add(Tail);
	}

	private void MoveKnot(int index)
	{
		var follow = knots[index - 1];
		var move = knots[index];
		var knot = CalculateNewKnot(move, follow);
		knots[index] = knot;
	}

	private Coordinate CalculateNewKnot(Coordinate Current, Coordinate Follow)
	{
		if(Current.ChebyshewDistance(Follow) <= 1)
			return Current;
		int x = CalculateOneStepTowards(Current.X, Follow.X);
		int y = CalculateOneStepTowards(Current.Y, Follow.Y);
		return new(x, y);
	}

	private int CalculateOneStepTowards(int current, int goal)
	{
		if(current == goal)
			return current;
		var diff = goal - current;
		var singleStep = diff / Math.Abs(diff);
		return current + singleStep;
	}
}

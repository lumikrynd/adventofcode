using System;

namespace Day08;

public class TreeCordinate
{
	private (int x, int y) inner;
	public int X => inner.x;
	public int Y => inner.y;

	public TreeCordinate(int x, int y)
	{
		inner = (x, y);
	}

	public override bool Equals(object? obj)
	{
		if(obj is not TreeCordinate other)
			return false;

		return other.X == X && other.Y == Y;
	}

	public override int GetHashCode() => HashCode.Combine(X, Y);
	public override string ToString() => $"({X}, {Y})";
}

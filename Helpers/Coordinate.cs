namespace Helpers;

public class Coordinate
{
	public int X { get; }
	public int Y { get; }

	public Coordinate(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int ManhattenDistance(Coordinate other)
	{
		return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
	}

	public int ChebyshewDistance(Coordinate other)
	{
		return Math.Max(Math.Abs(X - other.X), Math.Abs(Y - other.Y));
	}

	/// <summary>
	/// Get vector to
	/// </summary>
	public (int x, int y) GetVectorTo(Coordinate To)
	{
		return (To.X - X, To.Y - Y);
	}

	public Coordinate Clone()
	{
		return new Coordinate(X, Y);
	}
}

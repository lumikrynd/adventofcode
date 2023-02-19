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

	/// <summary>
	/// |X1 - X2| + |Y1 - Y2|
	/// </summary>
	public int ManhattenDistance(Coordinate other)
	{
		return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
	}

	/// <summary>
	/// Max( |X1 - X2|, |Y1 - Y2| )
	/// </summary>
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

	public override string ToString()
	{
		return $"({X}, {Y})";
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(X, Y);
	}

	public override bool Equals(object? obj)
	{
		if(obj is Coordinate coord)
			return Equals(coord);
		return false;
	}

	public bool Equals(Coordinate other)
	{
		return X == other.X && Y == other.Y;
	}
}

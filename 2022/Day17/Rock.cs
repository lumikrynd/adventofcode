namespace Day17;

public class Rock
{
	public int[] Shape { get; private set; }

	public int Altitude { get; private set; }

	public Rock(int[] shape, int height)
	{
		Shape = shape;
		Altitude = height;
	}

	public int this[int i]
	{
		get { return Shape[i]; }
		set { Shape[i] = value; }
	}

	public int Length => Shape.Length;

	public bool MoveLeft()
	{
		if (Shape.Any(x => x % 2 != 0))
			return false;
		
		for(int i = 0; i < Shape.Length; i++)
		{
			Shape[i] >>= 1;
		}

		return true;
	}

	/// <summary>
	/// If field is 7 wide, highestPossible should be 0b1000000
	/// </summary>
	public bool MoveRight()
	{
		if (Shape.Any(x => (x & (1 << 6)) != 0))
			return false;

		for(int i = 0; i < Shape.Length; i++)
		{
			Shape[i] <<= 1;
		}

		return true;
	}

	public bool MoveDown()
	{
		Altitude--;
		return true;
	}

	/// <summary>
	/// Only really used to go back up if collision happens.
	/// </summary>
	public bool MoveUp()
	{
		Altitude++;
		return true;
	}
}

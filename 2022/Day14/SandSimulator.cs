using Day14.Model;
using Helpers;

namespace Day14;

internal class SandSimulator
{
	readonly CaveMap cave;
	public int SandFilled { get; private set; }
	public SandSimulator(CaveMap cave)
	{
		this.cave = cave;
	}

	public void FillSandFrom(Coordinate coordinate)
	{
		Traverse(coordinate);
	}

	/// <summary>
	/// Basically a greedy path finding to the bottom.
	/// Fills out every path in the cave which didn't lead to the bottom.
	/// </summary>
	/// <param name="coordinate"></param>
	/// <returns>True if it found the bottom</returns>
	private bool Traverse(Coordinate coordinate)
	{
		if(cave.IsFilledOutAt(coordinate))
			return false;

		var reachedBottom = ReachedBottom(coordinate);
		reachedBottom = reachedBottom || Traverse(coordinate.AddVector((0, 1)));
		reachedBottom = reachedBottom || Traverse(coordinate.AddVector((-1, 1)));
		reachedBottom = reachedBottom || Traverse(coordinate.AddVector((1, 1)));

		if(reachedBottom is false)
		{
			cave.AddSand(coordinate);
			SandFilled++;
		}

		return reachedBottom;
	}

	private bool ReachedBottom(Coordinate coordinate)
	{
		return coordinate.Y >= cave.Bottom;
	}
}

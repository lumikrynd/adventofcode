using Helpers;

namespace Day12.Maps;

internal class Map
{
	public Map(int[,] fields, Coordinate start, Coordinate goal)
	{
		Fields = fields;
		Start = start;
		Goal = goal;	
	}

	public int[,] Fields { get; }
	public Coordinate Start { get; }
	public Coordinate Goal { get; }

	internal List<Coordinate> PossibleSteps(Coordinate coordinate)
	{
		List<Coordinate> steps = new();

		int x = coordinate.X;
		int y = coordinate.Y;

		var X = Fields.GetLength(0) - 1;
		var Y = Fields.GetLength(1) - 1;

		if (x > 0) steps.Add(new(x - 1, y));
		if (x < X) steps.Add(new(x + 1, y));
		if (y > 0) steps.Add(new(x, y - 1));
		if (y < Y) steps.Add(new(x, y + 1));

		var maxHeigh = Fields[x, y] + 1;

		var result = steps.Where(c => Fields[c.X, c.Y] <= maxHeigh).ToList();
		return result;
	}

	internal bool FoundEnd(Coordinate coordinate) => coordinate.Equals(Goal);
}

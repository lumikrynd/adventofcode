using Helpers;

namespace Day12.Maps;

internal class Map
{
	public Map(int[,] fields, Coordinate s, Coordinate e)
	{
		Fields = fields;
		S = s;
		E = e;	
	}

	public int[,] Fields { get; }
	public Coordinate S { get; }
	public Coordinate E { get; }

	internal List<Coordinate> PossibleSteps(Coordinate current)
	{
		List<Coordinate> steps = new();

		int x = current.X;
		int y = current.Y;

		var X = Fields.GetLength(0) - 1;
		var Y = Fields.GetLength(1) - 1;

		if (x > 0) steps.Add(new(x - 1, y));
		if (x < X) steps.Add(new(x + 1, y));
		if (y > 0) steps.Add(new(x, y - 1));
		if (y < Y) steps.Add(new(x, y + 1));

		var result = steps.Where(c => StepPossible(current, c)).ToList();
		return result;
	}

	public int GetHeightAt(Coordinate coordinate) => Fields[coordinate.X, coordinate.Y];
	public Func<Coordinate, Coordinate, bool> StepPossible { get; set; } = (_, _) => throw new NotImplementedException();

	public Func<Coordinate, bool> FoundEnd { get; set; } = _ => throw new NotImplementedException();
	public Coordinate Start { get; set; } = new(0, 0);
}

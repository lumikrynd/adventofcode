using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12.Maps;

internal class Map
{
	public Map(int[,] fields, (int x, int y) start, (int x, int y) goal)
	{
		Fields = fields;
		Start = start;
		Goal = goal;	
	}

	public int[,] Fields { get; }
	public (int x, int y) Start { get; }
	public (int x, int y) Goal { get; }

	internal List<(int x, int y)> PossibleSteps((int, int) coordinate)
	{
		List<(int x, int y)> steps = new();

		(int x, int y) = coordinate;

		var X = Fields.GetLength(0) - 1;
		var Y = Fields.GetLength(1) - 1;

		if (x > 0) steps.Add((x - 1, y));
		if (x < X) steps.Add((x + 1, y));
		if (y > 0) steps.Add((x, y - 1));
		if (y < Y) steps.Add((x, y + 1));

		var maxHeigh = Fields[x, y] + 1;

		var result = steps.Where(c => Fields[c.x, c.y] <= maxHeigh).ToList();
		return result;
	}

	internal bool FoundEnd((int x, int y) coordinate) => coordinate == Goal;
}

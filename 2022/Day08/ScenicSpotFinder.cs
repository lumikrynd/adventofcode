using System;
using Helpers;

namespace Day08;

internal class ScenicSpotFinder
{
	public static (int, Coordinate) FindScenicSpotValue(TreeGroove groove)
	{
		var instance = new ScenicSpotFinder(groove);
		return instance.FindScenicSpotValue();
	}

	int bestViewScore = 0;
	Coordinate bestSpot = new(0, 0);
	TreeGroove Groove { get; }

	private ScenicSpotFinder(TreeGroove groove)
	{
		Groove = groove;
	}

	public (int, Coordinate) FindScenicSpotValue()
	{
		for(int x = 0; x < Groove.Width; x++)
		{
			for(int y = 0; y < Groove.Height; y++)
			{
				CheckTreeHausePotential(new(x, y));
			}
		}

		return (bestViewScore, bestSpot);
	}

	private void CheckTreeHausePotential(Coordinate coord)
	{
		int treeHouseHeight = GetTreeHeight(coord);

		int potential = 1;
		potential *= DistanceToTreeOfMinimumHeight(coord, x => x + 1, Unchanged, treeHouseHeight);
		potential *= DistanceToTreeOfMinimumHeight(coord, x => x - 1, Unchanged, treeHouseHeight);
		potential *= DistanceToTreeOfMinimumHeight(coord, Unchanged, y => y + 1, treeHouseHeight);
		potential *= DistanceToTreeOfMinimumHeight(coord, Unchanged, y => y - 1, treeHouseHeight);

		if(potential > bestViewScore)
		{
			bestViewScore = potential;
			bestSpot = coord;
		}
	}

	private static int Unchanged(int i) => i;

	private int DistanceToTreeOfMinimumHeight(Coordinate start, Func<int, int> xChange, Func<int, int> yChange, int TreeHouseHeight)
	{
		int counter = 0;
		int x = start.X, y = start.Y;
		do
		{
			x = xChange(x);
			y = yChange(y);

			if(!ContainsCoordinate(x, y))
				break;
			counter++;
		}
		while(GetTreeHeight(x, y) < TreeHouseHeight);

		return counter;
	}

	private bool ContainsCoordinate(int x, int y) => Groove.ContainsCoordinate(x, y);
	private int GetTreeHeight(int x, int y) => Groove.GetTreeHeight(x, y);
	private int GetTreeHeight(Coordinate coord) => Groove.GetTreeHeight(coord);
}
